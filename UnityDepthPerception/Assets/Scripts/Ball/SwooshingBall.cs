using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class SwooshingBall : MonoBehaviour {

    public enum SwooshingPosition {
        Left, Center, Right
    };
    
    private PlayerController player;
    public Animator anim;
    
    public Transform burningBallPrefab;
    public Transform ballPrefab;
    
    
    public float moveSpeed = 60f;
    
    public bool bDynamicExpansion = true;
    public float secondsBeforeExpansion = 1.4f;
    public float expansionRate = 0.02f;
    public float shrinkingRate = 0.06f;
    public float maximumScaleX = 2.4f;
    public float minimumScaleX = 1f;
    
    private bool bConstricted = true;
    private SwooshingPosition m_swooshingPosition = SwooshingPosition.Center;
    
    private float m_deltaTimeBeforeSwooshing=0f;
    private bool m_expandable = false;
    private bool m_swooshingActive = false;

    private void Start() {
         GameObject[] goListPlayer = GameObject.FindGameObjectsWithTag("Player");
         GameObject playerGo = goListPlayer[0];

         player = playerGo.GetComponent<PlayerController>();

    }

    private void FixedUpdate() {
        if (bDynamicExpansion){
            ChangeExpansionBasedOnPlayerMovement();
        }
        //Movement
        transform.Translate(Vector2.left * Time.deltaTime * moveSpeed);  
    }

    public void ApplyHit(float spawnPositionX, float hitSpeed) {
        Debug.Log("Ball " + this + " got hit! Speed: " + hitSpeed);

        Vector2 spawnPosition;
        if (m_swooshingActive) {
            spawnPosition = new Vector2(spawnPositionX, this.transform.position.y); 
        }
        else {
            spawnPosition =  this.transform.position; 
        }


        if (hitSpeed >= 100) {
            Instantiate(burningBallPrefab, spawnPosition, Quaternion.identity);
        } else {
            Instantiate(ballPrefab, spawnPosition, Quaternion.identity);
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
                    //TODO Shrink Y
                    transform.localScale += new Vector3(expansionRate, 0, 0);
                }
            }
        }
        else {
            m_deltaTimeBeforeSwooshing = 0f;
            if (m_swooshingActive) {
                if (transform.localScale.x >= minimumScaleX) {
                    //TODO Expand Y back to 1
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
