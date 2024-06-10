using Enemy;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _lifeTime;
    [SerializeField] private int _damage;

    private Rigidbody2D _rigidbody;
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.velocity = transform.right * _speed;
        Invoke(nameof(DestroyAfterDelay), _lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null) return;

        if (collision.TryGetComponent(out IDamagable target))
            target.GetDamage(_damage);

        Destroy(gameObject);
    }

    private void DestroyAfterDelay() => Destroy(gameObject);
}
