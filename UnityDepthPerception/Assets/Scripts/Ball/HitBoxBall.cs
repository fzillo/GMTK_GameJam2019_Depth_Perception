using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class HitBoxBall : MonoBehaviour {
    
    public SwooshingBall ball;
    public SwooshingBall.SwooshingPosition listenToPosition = SwooshingBall.SwooshingPosition.Center;

    private bool bAlreadyHit = false;

    //TODO Implement HitDelay in PlayerController and Check here
    private int delayedHitFrames = 3;
    
    public bool CheckIfHit() {
        return listenToPosition.Equals(ball.GetSwooshingPosition());
    }

    private void OnTriggerEnter2D(Collider2D other) {
        HandleHit(other);
        
    }
    
    private void OnTriggerStay2D(Collider2D other) {
        HandleHit(other);
    }


    private void HandleHit(Collider2D other) {
        if (bAlreadyHit) {
            return;
        }

        if (other.CompareTag("HitBoxBat") && CheckIfHit()) {
            float spawnPositionX = this.transform.position.x + ((other.transform.position.x - this.transform.position.x) / 2);
            
            var player = other.GetComponentInParent<PlayerController>();
            bAlreadyHit = true;
            ball.ApplyHit(spawnPositionX, player.GetHitSpeed());
        }
    }
}
