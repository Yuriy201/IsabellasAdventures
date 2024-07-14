using Cinemachine;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class DynamicCameraFollow : MonoBehaviour
{
    [Inject] private PlayerSpawner _spawner;

    private Transform _target;
    private CinemachineVirtualCamera _camera;

    private void Awake()
    {
        _camera = GetComponent<CinemachineVirtualCamera>();

        _spawner.OnSpawned += Follow;
    }
    private void OnDisable()
    {
        _spawner.OnSpawned -= Follow;
    }
    private void Follow()
    {
        _target = _spawner.SpawnedPlayer.transform;
        _camera.Follow = _target;
    }
}
