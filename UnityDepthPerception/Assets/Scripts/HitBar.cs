using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBar : MonoBehaviour
{
    public int progress=0;
    public HitBoxBat hitBoxBat;

    private void OnEnable()
    {
        progress = 0;
    }

    public void AddTenPointsToProgress()
    {
        progress += 10;
        hitBoxBat.AddToBaseSpeed(progress);

        Debug.Log("HitBox Progress: " + progress);
    }
}
