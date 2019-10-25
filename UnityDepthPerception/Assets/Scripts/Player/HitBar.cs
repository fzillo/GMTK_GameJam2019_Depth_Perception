using UnityEngine;

public class HitBar : MonoBehaviour {
    private float m_factor = 1;
    private int m_progress = 10; // starting with 10 percent
    public PlayerController player;

    private void OnEnable() {
        m_progress = 10;
    }

    public void AddTenPercentToProgress() {
        m_progress += 10;
        m_factor = m_progress / 100f;
        player.MultiplyBaseSpeedByFactor(m_factor);

        Debug.Log("HitBox Progress: " + m_progress);
    }
}