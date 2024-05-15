using UnityEngine;
using Zenject;

public class CheckCollision : MonoBehaviour
{
    private PlayerStats _playerStats;

    [Inject]
    private void Inject(PlayerStats playerStats) => _playerStats = playerStats;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Coin"))
        {
            _playerStats._coin++;
            _playerStats._coinText.text = _playerStats._coin.ToString();
            collision.gameObject.SetActive(false);
        }
    }
}
