using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject goBatHitBox;

    public Animator animator;
    
    public float movespeed = 20f;
    public bool movable= true;

    private Vector2 m_moveVect = new Vector2();

    void Update()
    {

        m_moveVect.x = Input.GetAxisRaw("Horizontal");
        m_moveVect.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.Space)){
            animator.SetBool("SpacePressed", true);
            m_moveVect = Vector2.zero;
        } else
        {
            animator.SetBool("SpacePressed", false);
        }

        animator.SetFloat("Speed", m_moveVect.sqrMagnitude);

        
    }

    private void FixedUpdate()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerHitting"))
        {
            Debug.Log("Hitting");
            movable = false;
        } else
        {
            movable = true;
        }

        if (movable)
        {
            rb.MovePosition(Vector2Int.RoundToInt(rb.position + m_moveVect * Time.deltaTime * movespeed));
        }
    }

    public void ActivateBatHitbox()
    {
        goBatHitBox.SetActive(true);
    }

    public void DeactivateBatHitbox()
    {
        goBatHitBox.SetActive(false);
    }
}
