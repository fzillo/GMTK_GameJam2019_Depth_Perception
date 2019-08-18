using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public Transform prefabBall;
    public Vector3 ballSpawnShiftVector = new Vector3(0, 0, 0);

    private void OnCollisionEnter2D(Collision2D collision)
    {
        animator.SetTrigger("DoIdle");
    }

    public void SpawnBall()
    {
        Debug.Log("Ball Spawn Position: " + (this.transform.position + ballSpawnShiftVector));
        Instantiate(prefabBall, this.transform.position + ballSpawnShiftVector, animator.transform.rotation);
        //Debug.Break();
    }

    private void OnDestroy()
    {
        WaveController.enemiesOnField--;
    }
}
