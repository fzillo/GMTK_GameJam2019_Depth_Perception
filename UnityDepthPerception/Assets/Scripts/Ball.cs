using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public GameObject parent;
    
    public void ApplyHit(float hitSpeed)
    {
        Debug.Log("Ball " + this + " got hit! Speed: " + hitSpeed);
        //TODO Instantiate Ball flying away
        Destroy(parent);
    }

    private void OnDestroy()
    {
        Destroy(parent);
    }

}
