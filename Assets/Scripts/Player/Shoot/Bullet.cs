using Enemy;
using NeoxiderAudio;
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;

    private Rigidbody2D _rigidbody;

    public event Action<Collider2D>? OnHit;

    private List<IDamagable> hittedTargets = new List<IDamagable>();

    private void OnEnable()
    {
        hittedTargets.Clear();
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null) return;

        if (collision.TryGetComponent(out IDamagable target))
        {
            if (!hittedTargets.Contains(target))
            {
                hittedTargets.Add(target);

                //OnHit?.Invoke(collision);
                target.GetDamage(_damage);
                AudioManager.PlaySound(ClipType.arrowHit);
            }
        }

        // Return to pool if using object pooling
        // ObjectPool.Instance.ReternObject(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision == null) return;

        OnHit?.Invoke(collision.collider);
    }

    public void ApplyVelocity()
    {
        _rigidbody.velocity = transform.right * _speed;
    }
}
