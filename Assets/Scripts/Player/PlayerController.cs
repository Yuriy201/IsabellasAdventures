using UnityEngine;
using System.Collections;
using CustomAttributes;
using InputSystem;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CapsuleCollider2D))]
    public abstract class PlayerController : MonoBehaviour, IManaUser
    {
        public PlayerStats Stats { get; private set; }

        // for testing
        public bool isMobile = false;

        [Space(5)]
        [Header("Movement")]
        [ReadOnlyProperty]
        [SerializeField] protected float _speed;
        [SerializeField] protected float _walkSpeed = 10f;
        [SerializeField] protected float _sprintSpeed = 15f;

        [Space(5)]
        [Header("Jump")]
        [SerializeField] protected float _jumpForce;
        [SerializeField] protected float _jumpTime = 0.5f;
        protected float jumpTimer;
        [SerializeField] protected int _airJumpsCount = 2;
        [ReadOnlyProperty]
        [SerializeField] protected int currentAirJumps;
        [Space(4)]
        [SerializeField] protected Transform _checkGroundSphere;
        [SerializeField] protected float _checkGroundSphereRadius;
        [SerializeField] protected LayerMask _ignoredLayers;
        [Space(4)]
        [SerializeField] protected ParticleSystem _airJumpParticles;
        [SerializeField] protected int _airJumpParticlesCount = 5;
        [Space(4)]
        [SerializeField] protected float _coyoteTime = 0.2f;
        protected float coyoteTimer;
        [Space(4)]
        [SerializeField] protected float _jumpBufferTime = 0.2f;
        protected float jumpBufferTimer;

        [Space(5)]
        [Header("Combat")]
        [SerializeField] protected GameObject _bulletPrefab;
        [SerializeField] protected GameObject _altBulletPrefab;
        [SerializeField][Range(0, 10)] protected int _altFireManacost;
        [SerializeField] protected Transform _shootPoint;
        [SerializeField] protected float _reloadTime;

        protected MobileInputContainer _mobileInputContainer;
        protected Animator _animator;
        protected Rigidbody2D _rb;
        protected InputHandler _inputHandler;

        protected bool _isGround;
        protected bool _canRotate = false;
        protected bool _canShoot = true;

        protected int GroundAnimationHash = Animator.StringToHash("IsGround");
        protected int SpeedFloatHash = Animator.StringToHash("Speed");
        protected int JumpTriggerHash = Animator.StringToHash("Jump");
        protected int ShootTriggerHash = Animator.StringToHash("Shoot");

        public void SetUp(InputHandler inputHandler, PlayerStats playerStats, MobileInputContainer mobileInputContainer)
        {
            _inputHandler = inputHandler;
            Stats = playerStats;
            _mobileInputContainer = mobileInputContainer;

            _inputHandler.JumpButtonDown += Jump;
            _inputHandler.JumpButtonUp += JumpButtonUp;
            _inputHandler.FireButtonDown += Fire;
            _inputHandler.AltFireButtonDown += AltFire;
        }
        protected virtual void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponentInChildren<Animator>();
            QualitySettings.vSyncCount = -1;
        }

        private void Start()
        {
            if(_mobileInputContainer!=null)
                _mobileInputContainer.SetPlatformType(isMobile);
            else
            {
                Debug.LogWarning("_mobileInputContainer null");
            }
        }

        private void FixedUpdate()
        {
            Walk();
            FaceRotation();
            CheckGround();
            JumpBuffer();
            JumpVelocity();
        }
        protected abstract void Walk();
        protected abstract void FaceRotation();
        protected abstract void Jump();

        protected void JumpVelocity()
        {
            if (_inputHandler.Jump && jumpTimer > 0f)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
                jumpTimer -= Time.deltaTime;
            }
        }

        private void JumpButtonUp()
        {
            jumpTimer = -1f;
            _animator.ResetTrigger(JumpTriggerHash);
        }
        private void CheckGround()
        {
            _isGround = Physics2D.OverlapCircle(_checkGroundSphere.position, _checkGroundSphereRadius, ~_ignoredLayers);
            _animator.SetBool(GroundAnimationHash, _isGround);

            if (_isGround && _rb.velocity.y <= 0f)
            {
                currentAirJumps = _airJumpsCount;
                coyoteTimer = _coyoteTime;
            }
            else
            {
                _animator.SetBool(GroundAnimationHash, _isGround);
                coyoteTimer -= Time.deltaTime;
            }
        }

        protected abstract void Fire();
        protected abstract void AltFire();
        private void JumpBuffer()
        {
            jumpBufferTimer -= Time.deltaTime;

            if (jumpBufferTimer > 0f && _isGround)
            {
                Jump();
            }
        }
        protected IEnumerator ReloadFire()
        {
            _canShoot = false;
            yield return new WaitForSeconds(_reloadTime);
            _canShoot = true;
        }

        public void StopPlayer()
        {
            _animator.SetFloat(SpeedFloatHash, 0f);
            _rb.velocity = Vector3.zero;
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
