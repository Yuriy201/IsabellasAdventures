using Enemy;
using NeoxiderAudio;
using System;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _lifeTime;
    [SerializeField] private int _damage;

    private Rigidbody2D _rigidbody;

    public event Action<Collider2D>? OnHit;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    private void Start()
    {
        StartCoroutine(DestroyAfterLifetime());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null) return;

        if (collision.TryGetComponent(out IDamagable target))
        {
            OnHit?.Invoke(collision);
            target.GetDamage(_damage);
            AudioManager.PlaySound(ClipType.arrowHit);
        }

        // Return to pool if using object pooling
        // ObjectPool.Instance.ReternObject(gameObject);
    }

    public void ApplyVelocity()
    {
        _rigidbody.velocity = transform.right * _speed;
    }

    private IEnumerator DestroyAfterLifetime()
    {
        yield return new WaitForSeconds(_lifeTime);
        // Destroy(gameObject);  // Comment this line out or remove it to prevent destruction
    }
}
