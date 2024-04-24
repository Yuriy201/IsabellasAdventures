using UnityEngine;

public class PlayerMove: MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Transform _checkGroundTransform;
    
    private Rigidbody2D _rb;
    private IInput _input;

    private void Awake()
    {
        _input = GetComponent<IInput>();
        _rb = GetComponent<Rigidbody2D>();
    }
    
    private void Move(Vector2 direction)
    {
        _rb.velocity = new Vector2(direction.x * _speed, _rb.velocity.y);
    }

    private void Jump(bool jump)
    {
        if(jump)
        {
            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
    }
    
    private void OnEnable()
    {
        _input.Move += Move;
        _input.Jump += Jump;
    }
    
    private void OnDisable()
    {
        _input.Move -= Move;
        _input.Jump -= Jump;
    }
}
