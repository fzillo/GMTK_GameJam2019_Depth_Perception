using System;
using UnityEngine;

[Serializable]
public class Wave : MonoBehaviour
{
    public Transform enemyPrefab;

    private int m_spawnIndex;
    private bool m_waveActive;
    private float m_waveDeltaTime;
    public EnemySpawn[] spawns;
    public string waveName;

    public void SpawnNextEnemy()
    {
        if (m_waveActive && m_spawnIndex < spawns.Length)
        {
            Debug.Log("Spawning Enemy at:" + spawns[m_spawnIndex].spawnPoint.position);
            Instantiate(enemyPrefab, spawns[m_spawnIndex].spawnPoint.position,
                spawns[m_spawnIndex].spawnPoint.rotation);
            //TODO Initialise Parameters for Enemy
            WaveController.enemiesOnField++;
            m_spawnIndex++;
        }
    }

    public bool IsWaveFinished()
    {
        if (m_spawnIndex >= spawns.Length)
        {
            return true;
        }

        return false;
    }

    public bool IsItTimeToSpawn()
    {
        if (m_waveActive && !IsWaveFinished() && m_waveDeltaTime % 1000 >= spawns[m_spawnIndex].spawnAfterSeconds)
        {
            return true;
        }

        return false;
    }

    private void Update()
    {
        if (m_waveActive)
            //Debug.Log("Current Wave deltaTime: " + m_waveDeltaTime);
        {
            m_waveDeltaTime += Time.deltaTime;
        }
    }

    public void SetWaveActive(bool bActive)
    {
        Debug.Log(this + " active: " + bActive);
        m_waveActive = bActive;
    }
}

[Serializable]
public class EnemySpawn
{
    public float delayBetweenThrows;
    public int numberOfThrows;
    public bool randomizeDelayBetweenThrows;
    public bool randomizeNumberOfThrows;
    public float spawnAfterSeconds;
    public Transform spawnPoint;
}