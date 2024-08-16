using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Neoxiuder
{
    public class Spawner : MonoBehaviour
    {
        [Header("Spawn Setting")]
        [SerializeField] private GameObject[] prefabs;
        [SerializeField] private int spawnLimit = 3;
        [SerializeField] private float spawnDelay = 2f;

        [SerializeField] private Transform spawnPoint;

        [SerializeField] private bool spawnOnAwake;

        public bool isSpawning;

        private List<GameObject> spawnedObjects = new List<GameObject>();
        private int spawnedCount = 0;

        public bool debugSpawn;

        void Start()
        {
            if (spawnOnAwake)
                StartSpawn();
        }

        private void StartSpawn()
        {
            StartCoroutine(SpawnObjects());
        }

        private void Update()
        {
            if (debugSpawn)
            {
                debugSpawn = false;
                StartSpawn();
            }
        }

        private IEnumerator SpawnObjects()
        {
            spawnedCount = 0;
            isSpawning = true;

            while (isSpawning && spawnedCount < spawnLimit)
            {
                SpawnRandomObject();
                spawnedCount++;
                yield return new WaitForSeconds(spawnDelay);
            }
        }

        private void SpawnRandomObject()
        {
            if (prefabs.Length == 0) return;

            int randomIndex = prefabs.Length == 1 ? 0 : Random.Range(0, prefabs.Length);
            GameObject prefabToSpawn = prefabs[randomIndex];
            GameObject spawnedObject = Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity, transform);
            spawnedObjects.Add(spawnedObject);
        }

        public void RemoveAllSpawnedObjects()
        {
            foreach (GameObject obj in spawnedObjects)
            {
                if (obj != null)
                    Destroy(obj);
            }

            spawnedObjects.Clear();
        }

        public int GetActiveObjectCount()
        {
            int count = 0;

            for (int i = 0; i < spawnedObjects.Count; i++)
            {
                if (spawnedObjects[i] != null)
                {
                    count++;
                }
            }

            return count;
        }

        private void OnValidate()
        {
            if (spawnPoint == null)
                spawnPoint = transform;
        }
    }
}