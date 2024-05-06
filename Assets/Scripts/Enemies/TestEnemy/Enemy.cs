using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]

public class Enemy : MonoBehaviour, IEnemyStates
{
    #region Components
    private Rigidbody2D _rb;
    private Animator _animator;
    private PlayerStats _playerStats;
    private EnemyStats _enemyStats;
    #endregion

    #region const
    private Transform _firstPatrolPoint; 
    private Transform _secondPatrolPoint;
    private int _direction = 1;
    private float _speed;
    private float _stopWalk;
    private int _damage;
    #endregion
    
    [Inject] private void Injcet (PlayerStats playerStats, EnemyStats enemyStats)
    {
        _playerStats = playerStats;
        _enemyStats = enemyStats;
    }
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _firstPatrolPoint = _enemyStats._firstPatrolPoint;
        _secondPatrolPoint = _enemyStats._secondPatrolPoint;
        _speed = _enemyStats._speed;
        _stopWalk = _enemyStats._stopWalk;
        _damage = _enemyStats._damage;
    }
    
    public void Walk()
    {
        if (transform.position.x <= _firstPatrolPoint.position.x)
        {
            _direction = 1;
            transform.localScale = new Vector2(-1f, transform.localScale.y);
        }
        else if (transform.position.x >= _secondPatrolPoint.position.x)
        {
            _direction = -1;
            transform.localScale = new Vector2(1f, transform.localScale.y);
        }
        
        _rb.velocity = new Vector2(_direction * _speed * Time.fixedDeltaTime, _rb.velocity.y);
    }
    
    public void Attack(GameObject player)
    {
        var direction = 1;
        
        if (Vector3.Distance(player.transform.position, transform.position) > _stopWalk)
        {
            if (transform.position.x >= player.transform.position.x)
            {
                direction = -1;
                transform.localScale = new Vector2(1f, transform.localScale.y);
            }
            else if (transform.position.x <= player.transform.position.x) 
            { 
                direction = 1; 
                transform.localScale = new Vector2(-1f, transform.localScale.y);
            }
        }
        else
        {
            direction = 0;
            
            if (_playerStats._playerHp > 0)
            {
                _animator.SetTrigger("Attack");
            }
        }
        
        print(_playerStats._playerHp);
        print(_enemyStats._enemyHp);
        _rb.velocity = new Vector2(direction * _speed * Time.fixedDeltaTime, _rb.velocity.y);
    }
    
    public void AttackEnded()
    {
        _playerStats._playerHp -= _damage;
    }
    
    public void Die()
    {
        if (_enemyStats._enemyHp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
