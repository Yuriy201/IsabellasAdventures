using UnityEngine;
using Zenject;
using System;
using Player;
using FSM;

namespace Enemy.Wolf
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(Animator))]
    public class Wolf : Enemy
    {
        public event Action AttackAnimationCallback;

        [field:SerializeField]
        public Rigidbody2D Rigidbody2D { get; private set; }

        [field: SerializeField]
        public Animator Animator { get; private set; }
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

        [HideInInspector]
        public int WalkHash = Animator.StringToHash("Walk");
        [HideInInspector]
        public int BiteHash = Animator.StringToHash("Bite");
        [HideInInspector]
        public int IdleHash = Animator.StringToHash("Idle");

        private StateMachine<WolfState> _stateMachine;
        private PlayerStats _playerStats;

        [Inject] private void Inject(PlayerStats stats)
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
            //Animator = GetComponent<Animator>();
            DistanceTrigger.OnPlayerChanged += (player) => Target = player;
        }

        private void Start()
        {
            _stateMachine = new StateMachine<WolfState>();
            _stateMachine.AddState(new PatrolWolfState(_stateMachine, this));
            _stateMachine.AddState(new FollowWolfState(_stateMachine, this));
            _stateMachine.AddState(new AttackWolfState(_stateMachine, this));

        }

        private void FixedUpdate()
        {
            _stateMachine.UseActiveState();
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

        void AddExp() => _playerStats.AddExperience(this);

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
    }
}
