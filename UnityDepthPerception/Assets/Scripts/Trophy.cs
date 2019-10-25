using UnityEngine;

public class Trophy : MonoBehaviour {
    public Animator animator;
    public Sprite brokenTrophy;

    private bool m_broken;
    private bool m_checkAnimEnd;
    public SpriteRenderer spriteRenderer;


    private void OnTriggerEnter2D(Collider2D other) {
        if (!m_broken) {
            animator.SetTrigger("DoBreak");
            m_broken = true;
            m_checkAnimEnd = true;
        }
    }

    private void FixedUpdate() {
        if (m_checkAnimEnd && animator.GetCurrentAnimatorStateInfo(0).IsName("TrophyBroken")) {
            Debug.Log(gameObject.name + " broke!");
            animator.enabled = false;
            m_checkAnimEnd = false;
        }
    }
}