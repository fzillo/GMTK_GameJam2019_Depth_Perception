using UnityEngine;

public class OuterBoundary : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision) {
        Destroy(collision.gameObject);
    }
}