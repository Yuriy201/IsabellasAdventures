using UnityEngine;
using Player;
using FSM;
using Zenject;
using System;

namespace Enemy.Bird
{
    public class Crow : Enemy
    {
        public PlayerController TouchingTarget { get; set; }
        public PlayerController Target { get; private set; }
        public Animator Animator { get; private set; }

        public event Action? AttackAnimationCallback;

        [field: SerializeField] public float FollowSpeed { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float StayTime { get; private set; }
        [field: SerializeField] public int Damage { get; private set; }
        [field: SerializeField] public float DamageCooldown { get; private set; }

        [SerializeField] private Transform[] _points;
        public float _attackRadius;
        public float _attackRange;
        
        [HideInInspector] public int FlyHash = Animator.StringToHash("Fly");
        [HideInInspector] public int AttackHash = Animator.StringToHash("Attack");

        [SerializeField, HideInInspector]
        private SpriteRenderer _spriteRenderer;
        private StateMachine<CrowState> _stateMachine;
        private PlayerStats _playerStats;
        private int _currentPoint;

        public DistanceTrigger _distanceTrigger;
        
        [Inject] 
        private void Inject(PlayerStats stats) => 
            _playerStats = stats;

        public void ChangePoint()
        {
            _currentPoint++;

            if (_currentPoint >= _points.Length)
                _currentPoint = 0;
        }

        public Transform GetCurrentPoint() => 
            _points[_currentPoint];

        public void Rotate(Vector3 target) =>
            _spriteRenderer.flipX = transform.position.x < target.x;

        public void Move(Vector3 target, float speed) => 
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        private void OnValidate()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            Animator = GetComponent<Animator>();
        }

        private void Start()
        {            
            _stateMachine = new StateMachine<CrowState>();
            _stateMachine.AddState(new PatrolCrowState(_stateMachine, this));
            _stateMachine.AddState(new FollowCrowState(_stateMachine, this));
            _stateMachine.AddState(new AttackCrowState(_stateMachine, this));

            _distanceTrigger.OnPlayerChanged += (player) => Target = player;
            _distanceTrigger.OnPlayerExit += () => Target = null;
        }

        private void OnEnable() => 
            Died += AddExp;

        private void OnDisable() => 
            Died -= AddExp;

        private void Update() => 
            _stateMachine.UseActiveState();

        //private void OnTriggerEnter2D(Collider2D collision)
        //{
        //    if (_distanceTrigger.ClosestPlayer != null)
        //    {
        //        Target = _distanceTrigger.ClosestPlayer;
        //    }

        //    //if (collision.TryGetComponent(out PlayerController player))
        //    //    Target = player;
        //}

        //private void OnTriggerStay2D(Collider2D collision)
        //{
        //    if (_distanceTrigger.ClosestPlayer != null)
        //    {
        //        if (_distanceTrigger.CurrentDistance < _attackRadius)
        //            TouchingTarget = _distanceTrigger.ClosestPlayer;
        //        else 
        //            TouchingTarget = null;
        //    }
        //}

        //private void OnTriggerExit2D(Collider2D collision)
        //{
        //    if (collision.TryGetComponent(out PlayerController player))
        //        Target = null;
        //}

        private void InvokeAttackAnimationCallback() => AttackAnimationCallback?.Invoke();

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, Mathf.Sqrt(_attackRadius));

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, Mathf.Sqrt(_attackRange));
        }

        private void AddExp() =>
            _playerStats.AddExperience(this);
    }
}