using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class CharacterPlayer : MonoBehaviour
{
    public PlayerContoller Controller;
    public float Speed;
    public float FaceJump;
    [SerializeField] private Transform _checkGrondSphere;
    [SerializeField] private float _checkGroundSphereRadius;
    [SerializeField] private LayerMask GroundLayers;

    Animator _animator;
    Rigidbody2D _rigidbody;
    SpriteRenderer sprite;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    public void InputMove(float Direction)
    {
        if (CheckGround())
        {
            _rigidbody.velocity = new Vector2(Direction * Speed * Time.deltaTime, _rigidbody.velocity.y);

            if (Direction != 0.0f)
                sprite.flipX = Direction < 0.0f;
        }

        _animator.SetFloat("Speed", _rigidbody.velocity.magnitude / Speed);
    }

    public void Jump()
    {
        if (!CheckGround())
            return;

        _rigidbody.AddForce(Vector2.up * FaceJump, ForceMode2D.Impulse);
    }


    private bool CheckGround()
    {
        bool  _isGround = Physics2D.OverlapCircle(_checkGrondSphere.position + transform.position, _checkGroundSphereRadius, GroundLayers);
        _animator.SetBool("IsGround", _isGround);
        return _isGround;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteraction L_Target))
        {
            L_Target.Interaction(gameObject);
        }
    }
}
