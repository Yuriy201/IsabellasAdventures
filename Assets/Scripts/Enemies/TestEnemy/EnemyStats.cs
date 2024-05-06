using UnityEngine;

public class EnemyStats: MonoBehaviour
{
    [SerializeField] internal Transform _firstPatrolPoint; 
    [SerializeField] internal Transform _secondPatrolPoint;
    [SerializeField] internal float _patrolRadius;
    [SerializeField] internal int _enemyHp = 90;
    [SerializeField] internal float _speed;
    [SerializeField] internal float _stopWalk;
    [SerializeField] internal int _damage;
}
