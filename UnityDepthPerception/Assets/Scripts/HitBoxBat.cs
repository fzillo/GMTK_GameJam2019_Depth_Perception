using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxBat : MonoBehaviour
{
    //TODO make dynamic
    public float hitSpeed = 120f;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Ball collidingBall = collision.gameObject.GetComponent<Ball>();
        collidingBall.ApplyHit(hitSpeed);
    }
}
