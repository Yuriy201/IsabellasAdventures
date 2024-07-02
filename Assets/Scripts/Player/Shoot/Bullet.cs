using Enemy;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _lifeTime;
    [SerializeField] private int _damage;

    private Rigidbody2D _rigidbody;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null) return;

        if (collision.TryGetComponent(out IDamagable target))
            target.GetDamage(_damage);

        ObjectPool.Instance.ReternObject(gameObject);
    }

    public void ApplyVelocity()
    {
        _rigidbody.velocity = transform.right * _speed;
    }
}
