using UnityEngine;
using Player;
using Zenject;

public class RollingStone : MonoBehaviour
{
    [SerializeField] private float rollDistance = 50f;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float maxDistance = 25f;

    private Rigidbody2D rb;
    private Transform player;
    private bool isRolling = false;

    private PlayerStats _playerStats;

    private Vector3 initialPosition;

    [Inject]
    public void Construct(PlayerStats playerStats)
    {
        _playerStats = playerStats;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player")?.transform;
        initialPosition = transform.position;
        rb.isKinematic = true;
    }

    void Update()
    {
        if (player != null && Vector2.Distance(transform.position, player.position) < rollDistance && !isRolling)
        {
            rb.isKinematic = false;
            StartRolling();
        }

        if (isRolling)
        {
            DeleteRolling();
        }
    }

    private void Awake()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("RS"), LayerMask.NameToLayer("Projectile"), true);
    }

    void StartRolling()
    {
        isRolling = true;
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);
    }

    void DeleteRolling()
    {
        float distance = Vector2.Distance(player.position, transform.position);
        if (distance > maxDistance && transform.position.x < player.position.x)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (_playerStats != null)
            {
                _playerStats.RemoveHealth(_playerStats.CurrentHealth);
                rb.velocity = Vector2.zero;
            }
        }
    }
}
