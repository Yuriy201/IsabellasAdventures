using UnityEngine;
using Player;

public class Coin : MonoBehaviour
{
    [SerializeField] private float _animSpeed;

    private void Update()
    {
        transform.Rotate(0, _animSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            CoinCounter.Instance.AddCoin();
            Destroy(gameObject);
        }
    }
}
