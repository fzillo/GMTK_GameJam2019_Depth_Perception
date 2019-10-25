using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingAwayBall : MonoBehaviour
{
   public float movespeed = 120;
   
   private void FixedUpdate() {
      transform.Translate(new Vector2(1,0.75f) * Time.deltaTime * movespeed);   
   }
}
