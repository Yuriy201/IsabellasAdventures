using Photon.Pun;
using UnityEngine;
using Player;
using UnityEngine.Events;

[DefaultExecutionOrder(50)]
public class PlayerSpawner : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Transform[] _spawnPoints;

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
    public void Spawn()
    {
        var spawnPoint = GetRandomSpawnPoint();

        PlayerController player = PhotonNetwork.Instantiate(_playerPrefab.name, spawnPoint.position, spawnPoint.rotation).GetComponent<PlayerController>();
        SpawnedPlayer = player;
        OnSpawned?.Invoke();
    }
}
