using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingBall : MonoBehaviour
{
    public Transform leftBorder;
    public Transform rightBorder;
    public List<Rigidbody2D> blinkingBalls;
    public float blinksPerSecond = 1;

    public bool bBlinking = false;
    public bool bExpandable = false;

    [Range(0, 1f)]
    public float expansionFactor = 1f;

    [SerializeField]
    private float m_deltaTime = 0;
    [SerializeField]
    private float m_maximumDeltaX = 0;

    private float m_leftXBorder;
    private float m_rightXBorder;

    private void Start()
    {
        m_leftXBorder = leftBorder.transform.position.x;
        m_rightXBorder = rightBorder.transform.position.x;
        
        m_maximumDeltaX = m_rightXBorder - m_leftXBorder;
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
        //float leftX = m_leftXBorder;
        //float leftY = leftBorder.transform.position.y;
        //float rightX = m_rightXBorder;
        //float rightY = rightBorder.transform.position.y;

        //float deltaX = rightX - leftX;
        //float midPointX = leftX + (deltaX / 2);

        //float deltaXCorrected = deltaX * percentAbsExpansion;
        //float deltaXCorrectedHalved = deltaXCorrected / 2;

        //float newLeftX = midPointX - deltaXCorrectedHalved;
        //float newRightX = midPointX + deltaXCorrectedHalved;

        //leftBorder.SetPositionAndRotation(new Vector2(newLeftX, leftY), Quaternion.identity);
        //rightBorder.SetPositionAndRotation(new Vector2(newRightX, rightY), Quaternion.identity);


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
        float lengthUnit = deltaX / blinkingBalls.Count;

        Debug.Log("deltaX " + deltaX + " lengthUnit " + lengthUnit);
        
        for (int i = 0; i<blinkingBalls.Count; i++)
        {
            Rigidbody2D ball = blinkingBalls[i];

            float leftRange = leftXBorder + (lengthUnit * i);
            float rightRange = leftXBorder + (lengthUnit * (i +1));
            Debug.Log("index " + i + " leftXBorder " + leftXBorder + " rightXBorder " + rightXBorder + " leftRange " + leftRange + " rightRange " + rightRange);

            float xValueBlink = UnityEngine.Random.Range(leftRange, rightRange);
            float yValueBlink = ball.position.y;
            ball.MovePosition(new Vector2(xValueBlink, yValueBlink));
        }
    }
}
