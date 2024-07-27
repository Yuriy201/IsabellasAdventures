using Photon.Pun;
using UnityEngine;
using Player;
using UnityEngine.Events;
using Zenject;

[DefaultExecutionOrder(50)]
public class PlayerSpawner : Spawner
{
    [SerializeField] private GameObject _networkPlayerPrefab;
    [SerializeField] private GameObject _defaultPlayerPrefab;

    [Inject] private GameConfig _gameConfig;

    public UnityAction OnSpawned;
    public PlayerController SpawnedPlayer { get; private set; }

    private void Awake()
    {
        Spawn();
    }
    private Transform GetRandomSpawnPoint()
    {
        var randomValue = Random.Range(0, _spawnPoints.Length);

        return _spawnPoints[randomValue];
    }
    public override void Spawn()
    {
        var spawnPoint = GetRandomSpawnPoint();

        if (_gameConfig.IsMultiplayer)
        {
            SpawnedPlayer = PhotonNetwork.Instantiate(_networkPlayerPrefab.name, spawnPoint.position, spawnPoint.rotation).GetComponent<PlayerController>();
            Debug.Log("Network Spawner");
        }
        else
        {
            SpawnedPlayer = Instantiate(_defaultPlayerPrefab, spawnPoint.position, spawnPoint.rotation).GetComponent<PlayerController>();
            Debug.Log("Default Spawner");
        }
        OnSpawned?.Invoke();
    }
}
