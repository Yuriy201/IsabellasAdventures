using Enemy;
using NeoxiderAudio;
using System;
using UnityEngine;

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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null) return;

        if (collision.TryGetComponent(out IDamagable target))
        {
            OnHit.Invoke(collision);
            target.GetDamage(_damage);
            AudioManager.PlaySound(ClipType.arrowHit);
        }

        //ObjectPool.Instance.ReternObject(gameObject);
    }

    public void ApplyVelocity()
    {
        _rigidbody.velocity = transform.right * _speed;
    }
}
