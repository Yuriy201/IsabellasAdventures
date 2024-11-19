using UnityEngine;
using Zenject;
using Player;
namespace Enemy
{
    public class RocksTr : Enemy
    {
        private PlayerStats _playerStats;
        [Inject]
        private void Inject(PlayerStats playerStats)
        {
            _playerStats = playerStats;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                _playerStats.RemoveHealth(15);
            }
        }
    }
}