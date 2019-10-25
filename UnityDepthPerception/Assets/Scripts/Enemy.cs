using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public Vector3 ballSpawnShiftVector = new Vector3(0, 0, 0);
    public Transform prefabBall;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        animator.SetTrigger("DoIdle");
    }

    public void SpawnBall()
    {
        Debug.Log("Ball Spawn Position: " + (transform.position + ballSpawnShiftVector));
        Instantiate(prefabBall, transform.position + ballSpawnShiftVector, animator.transform.rotation);
        //Debug.Break();
    }

    private void OnDestroy()
    {
        WaveController.enemiesOnField--;
    }
}