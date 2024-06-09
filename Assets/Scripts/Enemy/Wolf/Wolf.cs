using UnityEngine;
using Zenject;
using System;
using Player;
using FSM;

namespace Enemy.Wolf
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class Wolf : Enemy
    {
        public event Action AttackAnimationCallback;

        public Rigidbody2D Rigidbody2D { get; private set; }
        public Animator Animator { get; private set; }
        public PlayerController Target { get; private set; }
        public PlayerController TouchingTarget { get; private set; }

        [field: SerializeField] public Transform LeftExetremPoint { get; private set; }
        [field: SerializeField] public Transform RightExetremPoint { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public int Damage { get; private set; }

        private StateMachine<WolfState> _stateMachine;
        private PlayerStats _playerStats;

        [Inject] private void Inject(PlayerStats stats)
        {
            _playerStats = stats;
        }

        private void Start()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();    

            _stateMachine = new StateMachine<WolfState>();
            _stateMachine.AddState(new PatrolWolfState(_stateMachine, this));
            _stateMachine.AddState(new FollowWolfState(_stateMachine, this));
            _stateMachine.AddState(new AttackWolfState(_stateMachine, this));
        }

        private void Update()
        {
            _stateMachine.UseActiveState();
            if (Input.GetKeyDown(KeyCode.I)) AddExp();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out PlayerController player)) Target = player;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out PlayerController player)) Target = null;
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.transform.TryGetComponent(out PlayerController player)) TouchingTarget = player;
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.transform.TryGetComponent(out PlayerController player)) TouchingTarget = null;
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
    }
}
