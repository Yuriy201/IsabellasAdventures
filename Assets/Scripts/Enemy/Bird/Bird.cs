using UnityEngine;
using Player;
using FSM;

namespace Enemy.Bird
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bird : Enemy
    {
        public PlayerController TouchingTarget { get; private set; }
        public PlayerController Target { get; private set; }
        public Rigidbody2D Rigidbody { get; private set; }
        public Transform CurrentPoint => _points[_currentPoint];

        [field: SerializeField] public float FollowSpeed { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public int Damage { get; private set; }

        [SerializeField] private Transform[] _points;
        [SerializeField] private float _attackRadius;

        private StateMachine<BirdState> _stateMachine;
        private int _currentPoint = 0;

        public void IncreasePointIndex()
        {
            _currentPoint++;

            if (_currentPoint >= _points.Length)
                _currentPoint = 0;
        }

        private void Start()
        {
            Rigidbody = GetComponent<Rigidbody2D>();

            _stateMachine = new StateMachine<BirdState>();
            _stateMachine.AddState(new PatrolBirdState(_stateMachine, this));
            _stateMachine.AddState(new FollowBirdState(_stateMachine, this));
            _stateMachine.AddState(new AttackBirdState(_stateMachine, this));
        }

        private void Update() => _stateMachine.UseActiveState();

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out PlayerController player))
                Target = player;
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out PlayerController player))
            {
                if (Vector2.Distance(player.transform.position, transform.position) < _attackRadius)
                    TouchingTarget = player;
                else 
                    TouchingTarget = null;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out PlayerController player))
                Target = null;
        }
    }
}