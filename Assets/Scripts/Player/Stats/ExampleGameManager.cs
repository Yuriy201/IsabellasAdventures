using UnityEngine.SceneManagement;
using UnityEngine;
using Zenject;

namespace Player
{
    public class ExampleGameManager : MonoBehaviour
    {
        private PlayerStats _playerStats;

        [Inject]
        private void Inject(PlayerStats stats) => _playerStats = stats;

        private void OnEnable() => _playerStats.OnDied += StartPause;

        private void OnDisable() => _playerStats.OnDied -= StartPause;

        private void StartPause() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
