using InputSystem;
using NeoxiderUi;
using Player;
using UnityEngine;
using Zenject;

public class PlayerTemporaryStorage : MonoBehaviour
{
    [SerializeField] private Page _gameOverPage;

    [Inject] private PlayerSpawner _spawner;

    private InputHandler _inputHandler;
    private PlayerStats _playerStats;
    private MobileInputContainer _mobileInputContainer;

    private void Awake()
    {
        _spawner.OnSpawned += SetUp;
    }
    private void OnDisable()
    {
        _spawner.OnSpawned -= SetUp;
    }
    [Inject]
    private void GetData(InputHandler inputHandler, PlayerStats playerStats, MobileInputContainer mobileInputContainer)
    {
        _inputHandler = inputHandler;
        _playerStats = playerStats;
        _mobileInputContainer = mobileInputContainer;
    }
    private void SetUp()
    {
        _spawner.SpawnedPlayer.SetUp(_inputHandler, _playerStats, _mobileInputContainer);
        _spawner.SpawnedPlayer.gameObject.GetComponent<PlayerDeath>().SetUp(_gameOverPage);
    }
}
