using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    [SerializeField] protected Transform[] _spawnPoints;

    public abstract void Spawn();
}
