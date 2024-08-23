using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public GameObject[] enemies;
    }

    public Wave[] waves;
    private int currentWaveIndex = 0;

    private void Start()
    {
        ActivateWave(waves[currentWaveIndex]);
        StartCoroutine(StartNextWave());
    }

    private IEnumerator StartNextWave()
    {
        while (currentWaveIndex < waves.Length)
        {
            yield return new WaitUntil(() => AreAllEnemiesMissing(waves[currentWaveIndex].enemies));
            yield return new WaitForSeconds(30f);
            currentWaveIndex++;
            if (currentWaveIndex < waves.Length)
            {
                ActivateWave(waves[currentWaveIndex]);
            }
        }
    }

    private void ActivateWave(Wave wave)
    {
        foreach (var enemy in wave.enemies)
        {
            if (enemy != null)
            {
                enemy.SetActive(true);
            }
        }
    }

    private bool AreAllEnemiesMissing(GameObject[] enemies)
    {
        foreach (var enemy in enemies)
        {
            if (enemy != null)
            {
                return false;
            }
        }
        return true;
    }
}
