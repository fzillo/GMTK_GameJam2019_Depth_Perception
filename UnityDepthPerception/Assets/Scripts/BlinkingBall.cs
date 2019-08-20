﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingBall : MonoBehaviour
{
    public Transform leftBorder;
    public Transform rightBorder;
    public Rigidbody2D blinkingBall;
    public float blinksPerSecond = 1;

    public int numberBlinkingBalls = 5;

    public bool bBlinking = false;
    public bool bExpandable = false;

    [Range(0, 1f)]
    public float expansionFactor = 1f;
    
    private List<Rigidbody2D> m_blinkingBallList;
    private float m_maximumDeltaX = 0;

    private float m_deltaTime = 0;

    private float m_leftXBorder;
    private float m_rightXBorder;

    private void Start()
    {
        m_leftXBorder = leftBorder.transform.position.x;
        m_rightXBorder = rightBorder.transform.position.x;
        
        m_maximumDeltaX = m_rightXBorder - m_leftXBorder;

        m_blinkingBallList = new List<Rigidbody2D>();
        m_blinkingBallList.Add(blinkingBall);

        if (numberBlinkingBalls>1)
        {
            for (int i = 1; i < numberBlinkingBalls; i++)
            {
                m_blinkingBallList.Add(Instantiate(blinkingBall, transform));
            }
        }
    }

    private void Update()
    {
        m_deltaTime += Time.deltaTime;

        if (bExpandable) {
            ChangeExpansion();
        }

        if (m_deltaTime >= (1/blinksPerSecond))        {

            if (bBlinking)
            {
                Blink();
            }
            m_deltaTime = 0;
        }

    }

    private void ChangeExpansion()
    {
        float leftY = leftBorder.transform.position.y;
        float rightY = rightBorder.transform.position.y;

        float leftX = leftBorder.transform.position.x;
        float rightX = rightBorder.transform.position.x;
        
        float currentDeltaX = rightX - leftX;

        float differenceBetweenDeltas = (m_maximumDeltaX * expansionFactor) - currentDeltaX;
        float differenceBetweenDeltasHalved = differenceBetweenDeltas * 0.5f;

        float newLeftX = leftX - differenceBetweenDeltasHalved;
        float newRightX = rightX + differenceBetweenDeltasHalved;

        leftBorder.SetPositionAndRotation(new Vector2(newLeftX, leftY), Quaternion.identity);
        rightBorder.SetPositionAndRotation(new Vector2(newRightX, rightY), Quaternion.identity);
    }

    private void Blink()
    {
        float leftXBorder = leftBorder.transform.position.x;
        float rightXBorder = rightBorder.transform.position.x;

        float deltaX = rightXBorder - leftXBorder;
        float lengthUnit = deltaX / m_blinkingBallList.Count;

        //Debug.Log("deltaX " + deltaX + " lengthUnit " + lengthUnit);
        
        for (int i = 0; i < m_blinkingBallList.Count; i++)
        {
            Rigidbody2D ball = m_blinkingBallList[i];

            float leftRange = leftXBorder + (lengthUnit * i);
            float rightRange = leftXBorder + (lengthUnit * (i +1));
            //Debug.Log("index " + i + " leftXBorder " + leftXBorder + " rightXBorder " + rightXBorder + " leftRange " + leftRange + " rightRange " + rightRange);

            float xValueBlink = UnityEngine.Random.Range(leftRange, rightRange);
            float yValueBlink = ball.position.y;
            ball.MovePosition(new Vector2(xValueBlink, yValueBlink));
        }
    }
}
