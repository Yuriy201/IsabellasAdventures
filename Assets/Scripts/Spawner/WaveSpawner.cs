using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject wolfPrefab;
    public GameObject ravenPrefab;
    public Transform[] spawnPoints;
    public float spawnInterval = 60f;
    public int totalWolves = 5;
    public int totalRavens = 5;

    private void Start()
    {
        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        int wolvesSpawned = 0;
        int ravensSpawned = 0;

        while (wolvesSpawned < totalWolves || ravensSpawned < totalRavens)
        {
            if (wolvesSpawned < totalWolves)
            {
                Transform spawnPoint = spawnPoints[wolvesSpawned % spawnPoints.Length];
                GameObject wolf = SpawnObject(wolfPrefab, spawnPoint.position, spawnPoint.rotation);
                wolvesSpawned++;
            }

            if (ravensSpawned < totalRavens)
            {
                Transform spawnPoint = spawnPoints[ravensSpawned % spawnPoints.Length];
                GameObject raven = SpawnObject(ravenPrefab, spawnPoint.position, spawnPoint.rotation);
                ravensSpawned++;
            }

            yield return new WaitForSeconds(spawnInterval / (totalWolves + totalRavens));
        }
    }

    private GameObject SpawnObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject obj = Instantiate(prefab, position, rotation);
        InitializeSpawnedObject(obj);
        return obj;
    }

    private void InitializeSpawnedObject(GameObject obj)
    {
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.simulated = true;
        }

        Collider2D collider = obj.GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = true;
        }

        Animator animator = obj.GetComponent<Animator>();
        if (animator != null)
        {
            animator.Rebind();
            animator.Update(0);
        }
    }
}
