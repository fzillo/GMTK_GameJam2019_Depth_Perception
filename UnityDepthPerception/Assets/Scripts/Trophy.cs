using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trophy : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public Sprite brokenTrophy;

    private bool m_broken = false;
    private bool m_checkAnimEnd = false;

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!m_broken) { 
            animator.SetTrigger("DoBreak");
            m_broken = true;
            m_checkAnimEnd = true;
        }
    }

    private void FixedUpdate()
    {        
        if (m_checkAnimEnd && animator.GetCurrentAnimatorStateInfo(0).IsName("TrophyBroken"))
        {
            Debug.Log(this.gameObject.name + " broke!");
            animator.enabled = false;
            m_checkAnimEnd = false;
        }
    }

}
