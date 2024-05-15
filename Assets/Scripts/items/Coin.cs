using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private Transform _topPoint;
    [SerializeField] private Transform _bottomPoint;
    [SerializeField] private float _animationSpeed;

    private Rigidbody2D _rb;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        StartCoroutine(CoinAnimation());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            Destroy(gameObject);
        }
    }
    
    private IEnumerator CoinAnimation()
    {
        var direction = 1;
        
        while (true)
        {
            if (transform.position.y >= _topPoint.position.y)
            {
                direction = -1;
            }
            else if (transform.position.y <= _bottomPoint.position.y)
            {
                direction = 1;
            }
        
            /*_rb.velocity = new Vector2(_rb.velocity.x, direction * _animationSpeed);*/
            transform.Translate(new Vector2(0, direction * _animationSpeed));
            
            yield return null;
        }
    }
}