using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;
    [SerializeField] private float _lifeTime;

    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.velocity = transform.right * _speed;
        Invoke(nameof(DestroyThis), _lifeTime);
    }

    private void DestroyThis() => Destroy(gameObject);
}
