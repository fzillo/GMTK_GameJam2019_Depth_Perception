using UnityEngine;

public class HitBoxBat : MonoBehaviour {
    public PlayerController player;

    private void OnTriggerEnter2D(Collider2D collision) {
        var collidingBall = collision.gameObject.GetComponent<Ball>();
        collidingBall.ApplyHit(player.GetHitSpeed());
    }
}