using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{    
    public Transform  tf;

    public float movespeed = 60f;    

    //private bool btumble = true;
    //private float m_timer = 0;
    //public float switchDirAfterSeconds = 1f;
    public int yMovement = 1;

    [Range(0,1)]
    private int m_hitAngleMod = 0; // default 0

    private void FixedUpdate()
    {
        //OPTION 1
        //m_timer = (m_timer + Time.deltaTime);
        //if (btumble && m_timer >= switchDirAfterSeconds)
        //{
        //    m_timer = m_timer % switchDirAfterSeconds;
        //    yMovement *= -1;
        //    transform.Translate((Vector2.left * Time.deltaTime * movespeed) + (Vector2.up * yMovement));
        //}
        //else
        //{
        //    transform.Translate(Vector2.left * Time.deltaTime * movespeed);
        //}

        //OPTION 2
        //transform.Translate(Vector2.left * Time.deltaTime * movespeed);

        //OPTION 3
        transform.Translate((Vector2.left * Time.deltaTime * movespeed)+(Vector2.up * m_hitAngleMod));
    }

    public void ApplyHit(float hitSpeed)
    {
        Debug.Log("Ball " + this + " got hit! Speed: " + hitSpeed);
        this.transform.Rotate(0f, 180f, 0f, Space.Self);
        movespeed += hitSpeed;
        m_hitAngleMod = 1; //TODO dynamic angle!
        //btumble = false;
    }
    
}
