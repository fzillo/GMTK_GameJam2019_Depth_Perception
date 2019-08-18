using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave : MonoBehaviour
{
    public string waveName;
    public Transform enemyPrefab;
    public EnemySpawn[] spawns;
    
    private int m_spawnIndex = 0;
    private bool m_waveActive = false;
    private float m_waveDeltaTime = 0;
        
    public void SpawnNextEnemy()
    {
        if (m_waveActive && m_spawnIndex < spawns.Length)
        {
            Debug.Log("Spawning Enemy at:" + spawns[m_spawnIndex].spawnPoint.position);
            Instantiate(enemyPrefab, spawns[m_spawnIndex].spawnPoint.position, spawns[m_spawnIndex].spawnPoint.rotation);
            WaveController.enemiesOnField++;
            m_spawnIndex++;
        }
    }

    public bool IsWaveFinished()
    {
        if (m_spawnIndex >= spawns.Length) { 
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsItTimeToSpawn()
    {
        if (m_waveActive && !IsWaveFinished() && m_waveDeltaTime % 1000 >= spawns[m_spawnIndex].spawnAfterSeconds)
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
        if (m_waveActive)
        {
            //Debug.Log("Current Wave deltaTime: " + m_waveDeltaTime);
            m_waveDeltaTime += Time.deltaTime;            
        }
    }

    public void SetWaveActive(bool bActive)
    {
        Debug.Log(this + " active: " + bActive);
        m_waveActive = bActive;
    }
}

[System.Serializable]
public class EnemySpawn
{   
    public Transform spawnPoint;
    public float spawnAfterSeconds;
    public int numberOfThrows;
}
