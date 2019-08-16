using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Animator animator;
    
    void Update()
    {

        if (Input.GetKey(KeyCode.F))
        {
            animator.SetTrigger("DoShake");
        }
    }
}
