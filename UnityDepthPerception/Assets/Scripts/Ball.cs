using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{    
    public Rigidbody2D rb;

    public float movespeed = 20f;

    private bool btumble = true;
    private float m_timer = 0;
    public float switchDirAfterSeconds = 1f;
    public int yMovement = 1;

    private void FixedUpdate()
    {
        m_timer = (m_timer + Time.deltaTime);
        if (btumble && m_timer >= switchDirAfterSeconds)
        {
            m_timer = m_timer % switchDirAfterSeconds;
            yMovement *= - 1;
            rb.MovePosition(rb.position + (Vector2.left * Time.deltaTime * movespeed) + (Vector2.up * yMovement));
        } else
        {
            rb.MovePosition(rb.position + (Vector2.left * Time.deltaTime * movespeed));
        }
    }
}
