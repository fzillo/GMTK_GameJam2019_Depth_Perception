using UnityEngine;

public class HittableBall : MonoBehaviour {
    
    public GameObject parent;

    public Transform burningBallPrefab;
    public Transform ballPrefab;

    public void ApplyHit(float hitSpeed) {
        Debug.Log("Ball " + this + " got hit! Speed: " + hitSpeed);

        if (hitSpeed >= 100) {
            Instantiate(burningBallPrefab, this.transform.position, Quaternion.identity);
        }
        else {
            Instantiate(ballPrefab, this.transform.position, Quaternion.identity);
        }
        
        Destroy(parent);
    }

    private void OnDestroy() {
        Destroy(parent);
    }
}