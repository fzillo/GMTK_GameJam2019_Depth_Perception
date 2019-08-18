using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    public Wave[] waves;
    private Wave m_currentWave;

    private bool m_controllerActive = true;
    private int m_waveIndex = 0;

    void Start()
    {
        m_currentWave = waves[m_waveIndex];
    }

    void Update()
    {
        if (!m_controllerActive)
        {
            return;
        }

        if (m_currentWave.IsWaveFinished() && m_waveIndex< waves.Length)
        {
            //set old wave inactive
            m_currentWave.SetWaveActive(false);
            m_waveIndex++;
            m_currentWave = waves[m_waveIndex];
            //set new wave active
            m_currentWave.SetWaveActive(true);
        } else if (m_currentWave.IsItTimeToSpawn())
        {
            m_currentWave.SpawnNextEnemy();
        }
    }
}
