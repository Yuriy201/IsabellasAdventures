using UnityEngine;
using Zenject;
using System;
using Player;

namespace Enemy.Wolf
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(Animator))]
    public class Wolf : Enemy
    {
        public event Action AttackAnimationCallback;

        [field: SerializeField] public Rigidbody2D Rigidbody2D { get; private set; }
        [field: SerializeField] public Animator Animator { get; private set; }
        public PlayerController Target { get; set; }
        public PlayerController TouchingTarget { get; private set; }

        [field: SerializeField] public Transform LeftExetremPoint { get; private set; }
        [field: SerializeField] public Transform RightExetremPoint { get; private set; }

        [field: SerializeField] public int Level { get; private set; } = 1;
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public int Damage { get; private set; }

        public DistanceTrigger DistanceTrigger;

        public float AttackCd = 2f;
        public float AttackRadius;
        public Vector2 AttackArea;
        public Vector2 AttackAreaOffset;
        public LayerMask AttackLayers;

        public float FollowSpeedMult = 1.2f;
        public float DistanceToUnAgro = 100f;

        [HideInInspector] public int WalkHash = Animator.StringToHash("Walk");
        [HideInInspector] public int BiteHash = Animator.StringToHash("Bite");
        [HideInInspector] public int IdleHash = Animator.StringToHash("Idle");

        private PlayerStats _playerStats;
        private int rsLayer;

        private enum WolfState
        {
            Patrol,
            Follow,
            Attack
        }

        private WolfState CurrentState = WolfState.Patrol;

        // Patrol
        private float walkTimer;
        private bool walking;
        private float stayTimer;
        private bool staying;

        // Follow
        private float speedMult = 1.3f;
        private float distanceCheckCdTimer = 0.5f;

        // Attack
        private float attackCdTimer;

        [Inject]
        private void Inject(PlayerStats stats)
        {
            _playerStats = stats;
        }

        private void OnValidate()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
        }

        private void Awake()
        {
            DistanceTrigger.OnPlayerChanged += (player) => Target = player;
            rsLayer = LayerMask.NameToLayer("RS");
        }

        private void Start()
        {
            EnterPatrol();
        }

        private void FixedUpdate()
        {
            if (Target != null)
            {
                if (DistanceTrigger.CurrentDistance <= AttackRadius)
                {
                    if (CurrentState != WolfState.Attack)
                    {
                        EnterAttack();
                        CurrentState = WolfState.Attack;
                    }
                    OperateAttack();
                }
                else
                {
                    if (CurrentState != WolfState.Follow)
                    {
                        EnterFollow();
                        CurrentState = WolfState.Follow;
                    }
                    OperateFollow();
                }
            }
            else
            {
                if (CurrentState != WolfState.Patrol)
                {
                    EnterPatrol();
                    CurrentState = WolfState.Patrol;
                }
                OperatePatrol();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out PlayerController player)) Target = player;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out PlayerController player)) Target = null;
        }

        private void InvokeAttackAnimationCallback() => AttackAnimationCallback?.Invoke();

        void AddExp()
        {
            _playerStats.AddExperience(this);
            AchievementManager.Instance?.RegisterWolfKill();
        }

        private void OnEnable()
        {
            Died += AddExp;
        }

        private void OnDisable()
        {
            Died -= AddExp;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, Mathf.Sqrt(DistanceToUnAgro));

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, Mathf.Sqrt(AttackRadius));

            Gizmos.color = Color.yellow;
            Gizmos.DrawCube(Rigidbody2D.position + Vector2.Scale(AttackAreaOffset, transform.right), AttackArea);
        }

        // Patrol
        public void EnterPatrol()
        {
            walkTimer = UnityEngine.Random.Range(12f, 14f);
            walking = true;
            staying = false;
            Animator.SetFloat("WalkMult", 1);
            Physics2D.IgnoreLayerCollision(gameObject.layer, rsLayer, true);
            Debug.Log("<color=green>Enter in Patrol</color>");
        }

        public void OperatePatrol()
        {
            if (Target != null) return;

            if (!staying && walkTimer > 0f)
            {
                if (walking)
                {
                    Animator.Play(WalkHash);
                    walking = false;
                }

                walkTimer -= Time.deltaTime;

                if (transform.position.x < LeftExetremPoint.position.x)
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                else if (transform.position.x > RightExetremPoint.position.x)
                    transform.rotation = Quaternion.Euler(0, 180, 0);

                Rigidbody2D.velocity = new Vector2(Speed * transform.right.x, Rigidbody2D.velocity.y);

                if (walkTimer <= 0f)
                {
                    walking = false;
                    staying = true;
                    stayTimer = UnityEngine.Random.Range(1f, 2f);
                }

                return;
            }

            if (!walking)
            {
                if (stayTimer > 0f)
                {
                    if (staying)
                    {
                        Animator.Play(IdleHash);
                        staying = false;
                    }

                    stayTimer -= Time.deltaTime;
                    Rigidbody2D.velocity = new Vector2(0, Rigidbody2D.velocity.y);
                }
                else
                {
                    staying = false;
                    walking = true;
                    walkTimer = UnityEngine.Random.Range(2f, 4f);
                }
            }
        }

        // Follow
        public void EnterFollow()
        {
            Animator.Play(WalkHash);
            Animator.SetFloat("WalkMult", speedMult);
            Physics2D.IgnoreLayerCollision(gameObject.layer, rsLayer, true);
            Debug.Log("<color=yellow>Enter in Follow</color>");
        }

        public void OperateFollow()
        {
            if (Target == null) return;

            distanceCheckCdTimer -= Time.deltaTime;

            if (distanceCheckCdTimer <= 0f && DistanceTrigger.ClosestPlayer == null)
            {
                distanceCheckCdTimer = 0.5f;
                float distance = (Target.transform.position - transform.position).sqrMagnitude;

                if (distance >= DistanceToUnAgro)
                {
                    Target = null;
                }
            }

            if (transform.position.x < Target.transform.position.x)
                transform.rotation = Quaternion.Euler(0, 0, 0);
            else if (transform.position.x > Target.transform.position.x)
                transform.rotation = Quaternion.Euler(0, 180, 0);

            Rigidbody2D.velocity = new Vector2(Speed * transform.right.x * speedMult, Rigidbody2D.velocity.y);
        }

        // Attack
        public void EnterAttack()
        {
            Debug.Log("<color=green>Enter in Attack</color>");
            AttackAnimationCallback += Attack;
            Rigidbody2D.velocity = Vector2.zero;
            Physics2D.IgnoreLayerCollision(gameObject.layer, rsLayer, true);
            attackCdTimer = -1f;
        }

        public void OperateAttack()
        {
            attackCdTimer -= Time.deltaTime;
            Rigidbody2D.velocity = Vector2.zero;

            if (attackCdTimer <= 0f)
            {
                if (DistanceTrigger.ClosestPlayer == null || DistanceTrigger.CurrentDistance > AttackRadius)
                {
                    EnterFollow();
                    CurrentState = WolfState.Follow;
                }
                else
                {
                    if (transform.position.x < Target.transform.position.x)
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                    else
                        transform.rotation = Quaternion.Euler(0, 180, 0);

                    Animator.Play(BiteHash, -1, 0f);
                    attackCdTimer = AttackCd;
                }
            }
        }

        private void Attack()
        {
            var hits = Physics2D.BoxCastAll(Rigidbody2D.position + Vector2.Scale(AttackAreaOffset, transform.right), AttackArea, 0, transform.right, 0, AttackLayers);

            foreach (var player in hits)
            {
                if (player.transform != null)
                    player.transform.GetComponent<PlayerController>().Stats.RemoveHealth(Damage);
            }
        }

        public int GetDamageValue() => Damage;
    }
}
