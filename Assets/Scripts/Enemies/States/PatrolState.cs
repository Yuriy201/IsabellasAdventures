using UnityEngine;

public class PatrolState : State
{
    private Vector2 _patrolPoints;
    private Movement _movement;
    private Vector3 _currentPatrolPoint;
    private Transform _tr;

    public PatrolState(Transform pos, Movement movement, float patrolRange)
    {
        _movement = movement;
        _tr = pos;
        _patrolPoints = new Vector2(_tr.position.x - patrolRange, _tr.position.x + patrolRange);
        _currentPatrolPoint = new Vector3(_patrolPoints.x, 0, 0);
    }

    public override void Update()
    {
        if (_movement.PointReached(_currentPatrolPoint))
        {
            NewPatrol();
        }
        _movement.MoveToThePoint(_currentPatrolPoint);
    }

    private void NewPatrol()
    {
        if (_currentPatrolPoint.x == _patrolPoints.x)
        {
            _currentPatrolPoint.x = _patrolPoints.y;
        }
        else
        {
            _currentPatrolPoint.x = _patrolPoints.x;
        }
    }
}

