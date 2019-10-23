using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingBall : MonoBehaviour
{
    public PlayerController player;

    public Transform leftBorderMarker;
    public Transform rightBorderMarker;
    public GameObject blinkingBall;
    public GameObject hittableBall;

    public float maximumExpansion = 100f;
    
    public float movespeed = 60f;

    public float blinksPerSecond = 1;
       
    public bool bBlinking = false;
    public bool bExpandable = false;
    public bool bMovementbasedExpansion = false;
    public float timeUntilExpansion = 1f;
    public bool bAdjustBallVisibility = false;

    [Range(0, 1f)]
    public float expansionFactor = 1f;

    public float modifierExpanding = 0.01f;
    public float modifierShrinking = 0.02f;

    private int numberBlinkingBalls = 5;
    private int currentlyVisibleBlinkingBalls;

    private List<GameObject> m_blinkingBallList;
    private float m_maximumDeltaX = 0;

    private float deltaTimeBlink = 0;
    private float deltaTimeExpand = 0;

    private float m_leftXBorder;
    private float m_rightXBorder;

    private void Start()
    {
        m_leftXBorder = hittableBall.transform.position.x - maximumExpansion;
        m_rightXBorder = hittableBall.transform.position.x + maximumExpansion;

        leftBorderMarker.position = new Vector2(m_leftXBorder, leftBorderMarker.position.y);
        rightBorderMarker.position = new Vector2(m_rightXBorder, rightBorderMarker.position.y);

        //m_leftXBorder = leftBorderMarker.transform.position.x;
        //m_rightXBorder = rightBorderMarker.transform.position.x;
        
        m_maximumDeltaX = m_rightXBorder - m_leftXBorder;

        m_blinkingBallList = new List<GameObject>();
        m_blinkingBallList.Add(blinkingBall);

        if (numberBlinkingBalls>1)
        {
            for (int i = 1; i < numberBlinkingBalls; i++)
            {
                m_blinkingBallList.Add(Instantiate(blinkingBall, transform));
            }
        }

        currentlyVisibleBlinkingBalls = numberBlinkingBalls;
    }

    private void Update()
    {
      
    }

    private void FixedUpdate()
    {
        deltaTimeBlink += Time.deltaTime;


        //Expansion
        if (bExpandable)
        {
            if (bMovementbasedExpansion) { 
                ChangeExpansionMovementbased();
            }
            if (bAdjustBallVisibility)
            {
                int desiredVisibleBlinkingBallsCount = DetermineDesiredBallCount();

                if (desiredVisibleBlinkingBallsCount != currentlyVisibleBlinkingBalls) { 
                    AdjustBlinkingBallVisibility(desiredVisibleBlinkingBallsCount);
                }
            }
            CalculateCurrentExpansion();
        }

        //Blinking
        if (deltaTimeBlink >= (1 / blinksPerSecond))
        {

            if (bBlinking)
            {
                Blink();
            }
            deltaTimeBlink = 0;
        }

        //Movement
        transform.Translate(Vector2.left * Time.deltaTime * movespeed); // + (Vector2.up * m_hitAngleMod)            
    }

    private void AdjustBlinkingBallVisibility(int desiredVisibleBlinkingBallsCount)
    {
        int n_ballsActivated = 0;

        for (int i = 0; i < m_blinkingBallList.Count; i++)
        {
            if (i <= desiredVisibleBlinkingBallsCount - 1) { 
                m_blinkingBallList[i].SetActive(true);
                n_ballsActivated++;
            } else
            {
                m_blinkingBallList[i].SetActive(false);
            }
        }

        currentlyVisibleBlinkingBalls = n_ballsActivated;
    }

    private int DetermineDesiredBallCount()
    {

        if (expansionFactor >= 0 && expansionFactor < 0.1f)
        {
            return  1;
        } else if (expansionFactor >= 0.1f && expansionFactor < 0.2f)
        {
            return 2;
        }
        else if (expansionFactor >= 0.2f && expansionFactor < 0.3f)
        {
            return 3;
        }
        else if (expansionFactor >= 0.3f && expansionFactor < 0.5f)
        {
            return 4;
        }
        else if (expansionFactor >= 0.5f && expansionFactor <= 1f)
        {
            return 5;
        }
        return 1;
    }


    private void ChangeExpansionMovementbased()
    {
        deltaTimeExpand += Time.deltaTime;

        if (player.IsMoving())
        {
            if (expansionFactor > 0) { 
                expansionFactor = (expansionFactor - modifierShrinking);
            }
            else
            {
                expansionFactor = 0;
            }

            deltaTimeExpand = 0;
        }
        else
        {
            if (expansionFactor < 1)
            {
                if (deltaTimeExpand >= timeUntilExpansion) { 
                    expansionFactor = (expansionFactor + modifierExpanding);
                }
            }
            else
            {
                expansionFactor = 1f;
            }           
        }
    }

    private void CalculateCurrentExpansion()
    {
        float leftY = leftBorderMarker.transform.position.y;
        float rightY = rightBorderMarker.transform.position.y;

        float leftX = leftBorderMarker.transform.position.x;
        float rightX = rightBorderMarker.transform.position.x;
        
        float currentDeltaX = rightX - leftX;

        float differenceBetweenDeltas = (m_maximumDeltaX * expansionFactor) - currentDeltaX;
        float differenceBetweenDeltasHalved = differenceBetweenDeltas * 0.5f;

        float newLeftX = leftX - differenceBetweenDeltasHalved;
        float newRightX = rightX + differenceBetweenDeltasHalved;

        leftBorderMarker.SetPositionAndRotation(new Vector2(newLeftX, leftY), Quaternion.identity);
        rightBorderMarker.SetPositionAndRotation(new Vector2(newRightX, rightY), Quaternion.identity);
    }

    private void Blink()
    {
        float leftXBorder = leftBorderMarker.transform.position.x;
        float rightXBorder = rightBorderMarker.transform.position.x;

        float deltaX = rightXBorder - leftXBorder;
        float lengthUnit = deltaX / currentlyVisibleBlinkingBalls; //m_blinkingBallList.Count

        //Debug.Log("deltaX " + deltaX + " lengthUnit " + lengthUnit);

        for (int i = 0; i < m_blinkingBallList.Count; i++)
        {
            GameObject ball = m_blinkingBallList[i];

            if (ball.activeSelf == false)
            {
                continue;
            }

            float leftRange = leftXBorder + (lengthUnit * i);
            float rightRange = leftXBorder + (lengthUnit * (i +1));
            //Debug.Log("index " + i + " leftXBorder " + leftXBorder + " rightXBorder " + rightXBorder + " leftRange " + leftRange + " rightRange " + rightRange);

            float xValueBlink = UnityEngine.Random.Range(leftRange, rightRange);
            float yValueBlink = ball.transform.position.y;
            ball.transform.position = new Vector2(xValueBlink, yValueBlink);
        }
    }
}
