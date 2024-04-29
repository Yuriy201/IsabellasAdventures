using System.Collections;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class Player : MonoBehaviour
{
    #region Field
    [SerializeField] private float _speed;

    [Space]
    [SerializeField] private float _jumpForce;
    [SerializeField] private Transform _checkGrondSphere;
    [SerializeField] private float _checkGroundSphereRadius;
    [SerializeField] private LayerMask _ignoredLayers;

    [Space]
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _reloadTime;

    private Animator _animator;
    private Rigidbody2D _rb;
    private InputHandler _inputHandler;

    private bool _isGround;
    private bool _canShoot = true;
    #endregion

    #region Const
    private Vector2 _leftFaceRotation = new Vector2(-1, 1);
    private Vector2 _rightFaceRotation = new Vector2(1, 1);
    #endregion

    [Inject]
    private void Inject(InputHandler inputHandler)
    {
        _inputHandler = inputHandler;
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        Walk();
        FaceRotation();
        CheckGround();
    }

    private void Walk()
    {
        _rb.velocity = new Vector2(_inputHandler.Directon.x * _speed, _rb.velocity.y);
        _animator.SetFloat("Speed", Mathf.Abs(_rb.velocity.x));
    }

    private void FaceRotation()
    {
        if (_rb.velocity.x < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else if (_rb.velocity.x > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
    
    private void Jump()
    {
        if (_isGround)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
            _animator.SetTrigger("Jump");
        }
        else _animator.ResetTrigger("Jump");
    }

    private void CheckGround()
    {
        _isGround = Physics2D.OverlapCircle(_checkGrondSphere.position, _checkGroundSphereRadius, ~_ignoredLayers);
        _animator.SetBool("IsGround", _isGround);
    }

    private void Fire()
    {
        if (_canShoot)
        {
            Instantiate(_bulletPrefab, _shootPoint.position, transform.rotation);
            _animator.SetTrigger("Shoot");
            StartCoroutine(ReloadFire());
        }
        else print("reload");
    }
    
    private IEnumerator ReloadFire()
    {
        _canShoot = false;
        yield return new WaitForSeconds(_reloadTime);
        _canShoot = true;
    }
    
    private void OnEnable()
    {
        _inputHandler.JumpButtonDown += Jump;
        _inputHandler.FireButtonDown += Fire;
    }
    private void OnDisable()
    {
        _inputHandler.JumpButtonDown -= Jump;
        _inputHandler.FireButtonDown -= Fire;
    }
}
