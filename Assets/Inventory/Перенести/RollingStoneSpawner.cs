using UnityEngine;
using Zenject;
using System.Collections;

public class RollingStoneSpawner : MonoBehaviour
{
    [SerializeField] private GameObject stonePrefab;
    [SerializeField] private int numberOfStones = 5;
    [SerializeField] private float spawnDistance = 10f;
    [SerializeField] private float delayBetweenStones = 1f;
    [SerializeField] private float spawnInterval = 10f;

    private Transform player;
    private Vector3 spawnPosition;
    private float timeSinceLastSpawn = 0f;

    private bool isSpawning = false;
    private int spawnedStones = 0;

    private DiContainer _container;

    [Inject]
    public void Construct(DiContainer container)
    {
        _container = container;
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
        spawnPosition = transform.position;

        Debug.Log("RollingStoneSpawner Start() - Спавнер инициализирован");
    }

    void Update()
    {
        if (player != null && Vector2.Distance(player.position, spawnPosition) < spawnDistance && !isSpawning)
        {
            Debug.Log("RollingStoneSpawner Update() - Игрок в зоне спавна. Начинаю спавн камней.");
            isSpawning = true;
            timeSinceLastSpawn = 0f;
        }

        if (isSpawning)
        {
            timeSinceLastSpawn += Time.deltaTime;

            if (timeSinceLastSpawn >= spawnInterval && spawnedStones < numberOfStones)
            {
                Debug.Log("RollingStoneSpawner Update() - Время прошло, спавню камень.");
                StartCoroutine(SpawnStoneWithDelay());
                timeSinceLastSpawn = 0f;
            }
        }
    }

    private IEnumerator SpawnStoneWithDelay()
    {
        if (spawnedStones < numberOfStones)
        {
            Debug.Log("RollingStoneSpawner SpawnStoneWithDelay() - Спавню камень в позиции: " + spawnPosition);

            GameObject newStone = _container.InstantiatePrefab(stonePrefab, spawnPosition, Quaternion.identity, null);
            spawnedStones++;

            if (spawnedStones >= numberOfStones)
            {
                Debug.Log("RollingStoneSpawner SpawnStoneWithDelay() - Все камни заспавнены.");
                isSpawning = false;
            }

            yield return new WaitForSeconds(delayBetweenStones);
        }
    }
}
