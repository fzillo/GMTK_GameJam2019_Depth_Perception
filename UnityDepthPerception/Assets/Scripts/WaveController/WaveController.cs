using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    public Wave[] waves;
    private Wave m_currentWave = null;

    public static int enemiesOnField = 0;

    private bool m_controllerActive = true;
    private int m_waveIndex = 0;

    void Start()
    {
        if (waves.Length > 0) { 
            m_currentWave = waves[m_waveIndex];
            m_currentWave.SetWaveActive(true);

            InvokeRepeating("HandleWaveSpawning", 1f, 1f);
        }
       else
       {
            Debug.LogError("No Waves defined! Cannot start spawning!");
       }
    }

    void HandleWaveSpawning()
    {
        if (!m_controllerActive || m_currentWave==null)
        {
            return;
        }

        if (m_currentWave.IsWaveFinished() && enemiesOnField == 0)
        {
            //set old wave inactive
            m_currentWave.SetWaveActive(false);
            m_waveIndex++;
            //set new wave active if possible
            if (m_waveIndex< waves.Length)
            {
                Debug.Log("Setting next Wave active.");
                m_currentWave = waves[m_waveIndex];
                m_currentWave.SetWaveActive(true);
            }
            //if not deactivate Wave Controller
            else
            {
                Debug.Log("Disabling Wave Controller.");
                m_controllerActive = false;
            }
        }
        
        if (!m_currentWave.IsWaveFinished() && m_currentWave.IsItTimeToSpawn())
        {
            m_currentWave.SpawnNextEnemy();
        }
    }
}
