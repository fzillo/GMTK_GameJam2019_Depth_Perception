using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class SwooshingBall : MonoBehaviour {

    public enum SwooshingPosition {
        Left, Center, Right
    };
    
    public PlayerController player;
    public Animator anim;
    
    public Transform burningBallPrefab;
    public Transform ballPrefab;

    public Transform leftHitSpawnPosition;
    public Transform centerHitSpawnPosition;
    public Transform rightHitSpawnPosition;

    public bool bDynamicExpansion = true;
    public float secondsBeforeExpansion = 2f;
    public float expansionRate = 0.01f;
    public float shrinkingRate = 0.02f;
    public float maximumScaleX = 2.6f;
    public float minimumScaleX = 1f;
    
    private bool bConstricted = true;
    private SwooshingPosition m_swooshingPosition = SwooshingPosition.Center;
    
    private float m_deltaTimeBeforeSwooshing=0f;
    private bool m_expandable = false;
    private bool m_swooshingActive = false;

    private void FixedUpdate() {
        if (bDynamicExpansion){
            ChangeExpansionBasedOnPlayerMovement();
        }
    }

    public void ApplyHit(float hitSpeed) {
        Debug.Log("Ball " + this + " got hit! Speed: " + hitSpeed);

        Vector2 spawnposition = this.transform.position;

         if (m_swooshingPosition.Equals(SwooshingPosition.Left)) {
             spawnposition = leftHitSpawnPosition.position;
         } else if (m_swooshingPosition.Equals(SwooshingPosition.Center)) {
             spawnposition = centerHitSpawnPosition.position;
         } else if (m_swooshingPosition.Equals(SwooshingPosition.Right)) {
             spawnposition = rightHitSpawnPosition.position;
         }

        if (hitSpeed >= 100) {
            Instantiate(burningBallPrefab, spawnposition, Quaternion.identity);
        } else {
            Instantiate(ballPrefab, spawnposition, Quaternion.identity);
        }
        
        Destroy(this.gameObject);
    }

    private void ChangeExpansionBasedOnPlayerMovement() {
        if (!player.IsMoving()) {
            if (!m_swooshingActive){
                m_deltaTimeBeforeSwooshing += Time.deltaTime;

                if (m_deltaTimeBeforeSwooshing >= secondsBeforeExpansion) {
                    anim.SetTrigger("StartSwooshing");
                    m_swooshingActive = true;
                    m_expandable = true;
                    m_deltaTimeBeforeSwooshing = 0f;
                    anim.ResetTrigger("EndSwooshing");
                }
            } else {
                if (m_expandable && transform.localScale.x <= maximumScaleX) {
                    transform.localScale += new Vector3(expansionRate, 0, 0);
                }
            }
        }
        else {
            if (m_swooshingActive) {
                if (transform.localScale.x >= minimumScaleX) {
                    transform.localScale += new Vector3(-shrinkingRate, 0, 0);
                }
                else {
                    transform.localScale = Vector3.one;
                    anim.SetTrigger("EndSwooshing");
                    anim.ResetTrigger("StartSwooshing");
                    m_expandable = false;
                }
            }
        }
    }
    
    public bool IsConstricted() {
        return bConstricted;
    }

    public void SetSwooshingActiveByFlag(int flag) {
        if (flag != 0){
            m_swooshingActive = true;
        }
        else {
            m_swooshingActive = false;
        }
    }
    
    public void SetSwooshingPosition(SwooshingPosition position) {
        m_swooshingPosition = position;
    }

    public SwooshingPosition GetSwooshingPosition() {
        return m_swooshingPosition;
    }

}
