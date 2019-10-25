using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxBall : MonoBehaviour {
    
    public SwooshingBall ball;
    public SwooshingBall.SwooshingPosition listenToPosition = SwooshingBall.SwooshingPosition.Initial;

    public bool CheckIfHit() {
        return listenToPosition.Equals(ball.GetSwooshingPosition());
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("HitBoxBat") && CheckIfHit()) {
            var player = other.GetComponentInParent<PlayerController>();
            ball.ApplyHit(player.GetHitSpeed());
        }
    }
}
