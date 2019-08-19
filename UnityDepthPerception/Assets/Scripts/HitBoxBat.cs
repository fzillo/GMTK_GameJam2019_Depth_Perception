using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxBat : MonoBehaviour
{
    public int baseSpeed = 20;

    [SerializeField]
    private int hitSpeed = 0;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Ball collidingBall = collision.gameObject.GetComponent<Ball>();
        collidingBall.ApplyHit(hitSpeed);
        hitSpeed = 0;
    }

    public void AddToBaseSpeed(int additionalSpeed) {
        hitSpeed = baseSpeed + additionalSpeed;
    }

}
