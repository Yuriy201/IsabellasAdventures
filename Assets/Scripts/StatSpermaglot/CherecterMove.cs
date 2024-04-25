using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class CherecterMove: MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Transform _checkGrondSphere;
    [SerializeField] private float _checkGroundSphereRadius;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private Animator _animator;
    
    private Rigidbody2D _rb;
    private InputHandler _inputHandler;

    [Inject]
    private void GetInputHandler(InputHandler inputHandler)
    {
        _inputHandler = inputHandler;
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Walk();
    }

    private void Walk()
    {
        _rb.velocity = new Vector2(_inputHandler.Directon.x * _speed, _rb.velocity.y);
        _animator.SetFloat("Speed", Mathf.Abs(_rb.velocity.x));

        if (_rb.velocity.x < 0)
        {
            transform.localScale = new Vector2(-1f, 1f);
        }
        else if (_rb.velocity.x > 0)
        {
            transform.localScale = new Vector2(1f, 1f);
        }
    }
    
    private void Jump()
    {
        if (Physics2D.OverlapCircle(_checkGrondSphere.position, _checkGroundSphereRadius, ~_playerLayer))
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
        }
    }
    
    private void OnEnable()
    {
        _inputHandler.JumpButtonDown += Jump;
    }
}
