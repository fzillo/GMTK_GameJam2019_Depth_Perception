using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxBat : MonoBehaviour
{

    public PlayerController player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Ball collidingBall = collision.gameObject.GetComponent<Ball>();
        collidingBall.ApplyHit(player.GetHitSpeed());
    }

   
}
