using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private float patrolRange;
    [SerializeField] private float _chargeSpeed;
    private MeleeAttack _meleeAttack;
    private Movement _movement;
    private PatrolState _patrolState;
    private MeleeBattle _meleeBattle;
    private State _currentState;

    private void Start()
    {
        _meleeAttack = GetComponent<MeleeAttack>();
        _movement = GetComponent<Movement>();
        _patrolState = new PatrolState(this.transform, _movement, patrolRange);
        _meleeBattle = new MeleeBattle(_movement, _meleeAttack, this.transform);
        _currentState = _patrolState;
    }

    private void Update()
    {
        _currentState.Update();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _meleeBattle.SetTarget(player.transform);
            _currentState = _meleeBattle;
        }
    }
}

