using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _lifeTime;
    [SerializeField] private int _damage;

    private Rigidbody2D _rigidbody;
    private EnemyStats _enemyStats;
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.velocity = transform.right * _speed;
        Invoke(nameof(DestroyThis), _lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<EnemyStats>(out EnemyStats enemy))
        {
            enemy._enemyHp -= _damage;
        }
        Destroy(gameObject);
    }

    private void DestroyThis() => Destroy(gameObject);
}
