using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
public class CherecterMove: MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private Transform _checkGrondSphere;
    [SerializeField] private float _checkGroundSphereRadius;
    [SerializeField] private LayerMask _playerLayer;
    
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _rb.velocity = new Vector2(_inputHandler.Directon.x * _speed, _rb.velocity.y);
    }

    private void Jump()
    {
        if (Physics2D.OverlapCircle(_checkGrondSphere.position, _checkGroundSphereRadius, ~_playerLayer))
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
        }
        else
        {
            print("asfasf");
        }
    }
    
    private void OnEnable()
    {
        _inputHandler.JumpButtonDown += Jump;
    }
}
