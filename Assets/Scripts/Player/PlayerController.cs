using System.Collections;
using CustomAttributes;
using InputSystem;
using Photon.Pun;
using UnityEngine;
using Zenject;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CapsuleCollider2D))]
    [RequireComponent(typeof(PhotonView))]

    public class PlayerController : MonoBehaviour, IManaUser
    {
        #region Field
        public PlayerStats Stats { get; private set; }

        // for testing
        public bool isMobile = false;

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
        [SerializeField] private int currentAirJumps;
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
        private PhotonView _photonView;
        private InputHandler _inputHandler;

        private bool _isGround;
        private bool _canRotate = false;
        private bool _canShoot = true;
        #endregion

        #region Const
        private Vector2 _leftFaceRotation = new Vector2(-1, 1);
        private Vector2 _rightFaceRotation = new Vector2(1, 1);
        #endregion

        public void SetUp(InputHandler inputHandler, PlayerStats playerStats, MobileInputContainer mobileInputContainer)
        {
            _inputHandler = inputHandler;
            Stats = playerStats;
            _mobileInputContainer = mobileInputContainer;

            _inputHandler.JumpButtonDown += Jump;
            _inputHandler.FireButtonDown += Fire;
            _inputHandler.AltFireButtonDown += AltFire;
        }

        private void Awake()
        {
            _photonView = GetComponent<PhotonView>();
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponentInChildren<Animator>();
            QualitySettings.vSyncCount = -1;
        }

        private void Start()
        {
            _mobileInputContainer.SetPlatformType(isMobile);
        }

        private void FixedUpdate()
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
            if (_rb.velocity.x < 0 && _canRotate == false)
            {
                _photonView.RPC(nameof(RotateFace), RpcTarget.AllBuffered, Quaternion.Euler(0, 180, 0));
                _canRotate = true;
            }
            else if (_rb.velocity.x > 0 && _canRotate)
            {
                _photonView.RPC(nameof(RotateFace), RpcTarget.AllBuffered, Quaternion.Euler(0, 0, 0));
                _canRotate = false;
            }
        }

        [PunRPC]
        private void RotateFace(Quaternion degress)
        {
            transform.localRotation = degress;
        }

        private void Jump()
        {
            jumpBufferTimer = _jumpBufferTime;

            if (coyoteTimer > 0f)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);

                _photonView.RPC(nameof(EnableJumpTrigger), RpcTarget.All);

                coyoteTimer = -1f;
                jumpBufferTimer = -1f;

                return;
            }

            if (currentAirJumps > 0)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
                _airJumpParticles.Emit(_airJumpParticlesCount);
                currentAirJumps--;
                jumpBufferTimer = -1f;

                return;
            }
            _photonView.RPC(nameof(ResetJumpTrigger), RpcTarget.All);
        }
        [PunRPC]
        private void EnableJumpTrigger()
        {
            _animator.SetTrigger("Jump");
        }
        [PunRPC]
        private void ResetJumpTrigger()
        {
            _animator.ResetTrigger("Jump");
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
                _photonView.RPC(nameof(FireRPC), RpcTarget.All);
            }
        }
        [PunRPC]
        private void FireRPC()
        {
            GameObject newBullet = ObjectPool.Instance.GetObject(_bulletPrefab, _shootPoint.transform);
            newBullet.transform.rotation = _shootPoint.rotation;
            newBullet.GetComponent<Bullet>().ApplyVelocity();

            ObjectPool.Instance.ReternObject(newBullet, 2f);

            _animator.SetTrigger("Shoot");
            StartCoroutine(ReloadFire());
        }
        private void AltFire()
        {
            if (_canShoot)
            {
                if (Stats.RemoveMana(this))
                {
                    _photonView.RPC(nameof(AltFireRPC), RpcTarget.All);
                }
            }
        }
        [PunRPC]
        private void AltFireRPC()
        {
            GameObject newBullet = ObjectPool.Instance.GetObject(_altBulletPrefab, _shootPoint.transform);
            newBullet.transform.rotation = _shootPoint.rotation;
            newBullet.GetComponent<Bullet>().ApplyVelocity();

            ObjectPool.Instance.ReternObject(newBullet, 2f);

            _animator.SetTrigger("Shoot");
            StartCoroutine(ReloadFire());
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

        private void OnDisable()
        {
            _inputHandler.JumpButtonDown -= Jump;
            _inputHandler.FireButtonDown -= Fire;
            _inputHandler.AltFireButtonDown -= AltFire;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(_checkGroundSphere.position, _checkGroundSphereRadius);
        }

        public int GetManacost()
        {
            return _altFireManacost;
        }
    }
}