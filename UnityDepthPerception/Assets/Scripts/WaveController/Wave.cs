using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public string waveName;
    public EnemySpawn[] spawns;
    public int spawnIndex = 0;

    private bool m_waveActive = false;
    private float m_waveDeltaTime = 0;
    
    public void SpawnNextEnemy()
    {
        if (m_waveActive && spawnIndex < spawns.Length)
        {
            Instantiate(spawns[spawnIndex].enemyPrefab, spawns[spawnIndex].spawnPoint.position, spawns[spawnIndex].spawnPoint.rotation);
            spawnIndex++;
        }
    }

    public bool IsWaveFinished()
    {
        if (spawnIndex > spawns.Length) { 
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsItTimeToSpawn()
    {
        if (m_waveActive && m_waveDeltaTime%1000 >= spawns[spawnIndex].spawnAfterSeconds)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Update()
    {
        if (m_waveActive) { 
            m_waveDeltaTime += Time.deltaTime;
        }
    }

    public void SetWaveActive(bool bActive)
    {
        m_waveActive = bActive;
    }
}

public class EnemySpawn
{
    public Transform enemyPrefab;    
    public Transform spawnPoint;
    public float spawnAfterSeconds;
    public int numberOfThrows;
}
