using UnityEngine;
using UnityEngine.Serialization;

public class Enemy : MonoBehaviour {
    public Animator animator;
    public Vector3 ballSpawnOffset = new Vector3(0, 0, 0);
    public Transform prefabBlinkingBall;

    private void OnCollisionEnter2D(Collision2D collision) {
        animator.SetTrigger("DoIdle");
    }

    public void SpawnBall() {
        Debug.Log("Ball Spawn Position: " + (transform.position + ballSpawnOffset));
        Instantiate(prefabBlinkingBall, transform.position + ballSpawnOffset, animator.transform.rotation);
        Debug.Break();
    }

    private void OnDestroy() {
        WaveController.enemiesOnField--;
    }
}