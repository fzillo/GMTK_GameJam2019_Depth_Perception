using UnityEngine;

public class CameraController : MonoBehaviour {
    public Animator animator;

    private void Update() {
        if (Input.GetKey(KeyCode.F)) {
            animator.SetTrigger("DoShake");
        }
    }
}