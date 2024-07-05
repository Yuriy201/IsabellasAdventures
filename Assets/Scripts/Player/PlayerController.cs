using System.Collections;
using CustomAttributes;
using InputSystem;
using UnityEngine;
using Zenject;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CapsuleCollider2D))]

    public class PlayerController : MonoBehaviour, IManaUser
    {
        #region Field
        public PlayerStats Stats { get; private set; }

        [Space(5)]
        [Header("Movement")]
        [ReadOnlyProperty] 
        [SerializeField] private float _speed;
        [SerializeField] private float _walkSpeed = 10f;
        [SerializeField] private float _sprintSpeed = 15f;

        [Space(5)]
        [Header("Jump")]
        [SerializeField] private float _jumpForce;
        [SerializeField] private int _airJumpsCount = 2;
        [ReadOnlyProperty]
        [SerializeField]  private int currentAirJumps;
        [Space(4)]
        [SerializeField] private Transform _checkGroundSphere;
        [SerializeField] private float _checkGroundSphereRadius;
        [SerializeField] private LayerMask _ignoredLayers;
        [Space(4)]
        [SerializeField] private ParticleSystem _airJumpParticles;
        [SerializeField] private int _airJumpParticlesCount = 5;
        [Space(4)]
        [SerializeField] private float _coyoteTime = 0.2f;
        private float coyoteTimer;
        [Space(4)]
        [SerializeField] private float _jumpBufferTime = 0.2f;
        private float jumpBufferTimer;

        [Space]
        [Header("Mobile Input")]
        [SerializeField] private MobileInputContainer _mobileInputContainer;

        [Space(5)]
        [Header("Combat")]
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private GameObject _altBulletPrefab;
        [SerializeField][Range(0, 10)] private int _altFireManacost;
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
        private void Inject(InputHandler inputHandler, PlayerStats playerStats)
        {
            _inputHandler = inputHandler;
            Stats = playerStats;
        }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponentInChildren<Animator>();
            QualitySettings.vSyncCount = -1;           
        }

        private void Update()
        {
            Walk();
            FaceRotation();
            CheckGround();
            JumpBuffer();
        }

        private void Walk()
        {
            _speed = _inputHandler.Sprinting ? _sprintSpeed : _walkSpeed;

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
            jumpBufferTimer = _jumpBufferTime;

            if (coyoteTimer > 0f)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
                _animator.SetTrigger("Jump");
                coyoteTimer = -1f;
                jumpBufferTimer = -1f;

                return;
            }

            if (currentAirJumps > 0)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
                //_animator.SetTrigger("Jump");
                _airJumpParticles.Emit(_airJumpParticlesCount);
                currentAirJumps--;

                jumpBufferTimer = -1f;
            }
            else _animator.ResetTrigger("Jump");
        }

        private void CheckGround()
        {
            _isGround = Physics2D.OverlapCircle(_checkGroundSphere.position, _checkGroundSphereRadius, ~_ignoredLayers);
            _animator.SetBool("IsGround", _isGround);

            if (_isGround && _rb.velocity.y <= 0f) 
            {
                currentAirJumps = _airJumpsCount;
                coyoteTimer = _coyoteTime;
            }
            else
            {
                coyoteTimer -= Time.deltaTime;
            }
        }

        private void Fire()
        {
            if (_canShoot)
            {
                GameObject newBullet = ObjectPool.Instance.GetObject(_bulletPrefab, _shootPoint.transform);
                newBullet.transform.rotation = _shootPoint.rotation;
                newBullet.GetComponent<Bullet>().ApplyVelocity();

                ObjectPool.Instance.ReternObject(newBullet, 2f);

                _animator.SetTrigger("Shoot");
                StartCoroutine(ReloadFire());
            }
        }

        private void AltFire()
        {
            if (_canShoot)
            {
                if (Stats.RemoveMana(this))
                {
                    GameObject newBullet = ObjectPool.Instance.GetObject(_altBulletPrefab, _shootPoint.transform);
                    newBullet.transform.rotation = _shootPoint.rotation;
                    newBullet.GetComponent<Bullet>().ApplyVelocity();

                    ObjectPool.Instance.ReternObject(newBullet, 2f);

                    _animator.SetTrigger("Shoot");
                    StartCoroutine(ReloadFire());
                }
            }
        }

        private void JumpBuffer()
        {
            jumpBufferTimer -= Time.deltaTime;  

            if (jumpBufferTimer > 0f && _isGround)
            {
                Jump();
            }
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
            _inputHandler.AltFireButtonDown += AltFire;
        }

        private void OnDisable()
        {
            _inputHandler.JumpButtonDown -= Jump;
            _inputHandler.FireButtonDown -= Fire;
            _inputHandler.AltFireButtonDown -= AltFire;
        }

        public int GetManacost()
        {
            return _altFireManacost;
        }
    }
}