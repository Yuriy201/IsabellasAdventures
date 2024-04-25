using UnityEngine;

public class Movement : MonoBehaviour
{
    public Vector3 CurrentPos { get => _currentPos; }

    [SerializeField] private float _moveSpeed;

    private Vector3 _currentPos;
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRend;
    private bool _isFlipped;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRend = GetComponent<SpriteRenderer>();
        _isFlipped = _spriteRend.flipX;
    }

    public void MoveToThePoint(Vector3 point, float speedMod = 1)
    {
        Vector2 direction = (point - transform.position).normalized;
        CheckFlippNormalized(direction);
        _rb.MovePosition(_rb.position + direction * (_moveSpeed * speedMod * Time.fixedDeltaTime));
    }

    public void MoveToTarget(Vector2 position)
    {
        Vector2 newPos = Vector2.MoveTowards(_rb.position, position, _moveSpeed * Time.fixedDeltaTime);
        CheckFlipp(newPos);
        _rb.MovePosition(newPos);
    }

    public bool PointReached(Vector3 point)
    {
        return Mathf.Abs(transform.position.x - point.x) < 1.5f ? true : false;
    }

    private void CheckFlipp(Vector2 pos)
    {
        if (pos.x < transform.position.x && _isFlipped)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            _isFlipped = false;
        }
        else if (pos.x > transform.position.x && !_isFlipped)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            _isFlipped = true;
        }
    }

    private void CheckFlippNormalized(Vector3 point)
    {
        point = point.normalized;
        if (point.x < 0 && _isFlipped)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            _isFlipped = false;
        }
        else if (point.x > 0 && !_isFlipped)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            _isFlipped = true;
        }
    }
}

