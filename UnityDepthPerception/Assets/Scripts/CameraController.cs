using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
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
