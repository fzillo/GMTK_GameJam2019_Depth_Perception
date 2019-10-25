using System.Collections.Generic;
using UnityEngine;

public class BlinkingBall : MonoBehaviour {
    private readonly int numberBlinkingBalls = 5;
    public bool bAdjustBallVisibility;

    public bool bBlinking;
    public bool bExpandable;
    public GameObject blinkingBall;

    public float blinksPerSecond = 1;
    public bool bMovementbasedExpansion;
    private int currentlyVisibleBlinkingBalls;

    private float deltaTimeBlink;
    private float deltaTimeExpand;

    [Range(0, 1f)] public float expansionFactor = 1f;

    public GameObject hittableBall;

    public Transform leftBorderMarker;

    private List<GameObject> m_blinkingBallList;

    private float m_leftXBorder;
    private float m_maximumDeltaX;
    private float m_rightXBorder;

    public float maximumExpansion = 100f;

    public float modifierExpanding = 0.01f;
    public float modifierShrinking = 0.02f;

    public float movespeed = 60f;
    public PlayerController player;
    public Transform rightBorderMarker;
    public float timeUntilExpansion = 1f;

    private void Start() {
        m_leftXBorder = hittableBall.transform.position.x - maximumExpansion;
        m_rightXBorder = hittableBall.transform.position.x + maximumExpansion;

        leftBorderMarker.position = new Vector2(m_leftXBorder, leftBorderMarker.position.y);
        rightBorderMarker.position = new Vector2(m_rightXBorder, rightBorderMarker.position.y);

        //m_leftXBorder = leftBorderMarker.transform.position.x;
        //m_rightXBorder = rightBorderMarker.transform.position.x;

        m_maximumDeltaX = m_rightXBorder - m_leftXBorder;

        m_blinkingBallList = new List<GameObject>();
        m_blinkingBallList.Add(blinkingBall);

        if (numberBlinkingBalls > 1) {
            for (var i = 1; i < numberBlinkingBalls; i++) {
                m_blinkingBallList.Add(Instantiate(blinkingBall, transform));
            }
        }

        currentlyVisibleBlinkingBalls = numberBlinkingBalls;
    }

    private void Update() {
    }

    private void FixedUpdate() {
        deltaTimeBlink += Time.deltaTime;


        //Expansion
        if (bExpandable) {
            if (bMovementbasedExpansion) {
                ChangeExpansionMovementbased();
            }

            if (bAdjustBallVisibility) {
                var desiredVisibleBlinkingBallsCount = DetermineDesiredBallCount();

                if (desiredVisibleBlinkingBallsCount != currentlyVisibleBlinkingBalls) {
                    AdjustBlinkingBallVisibility(desiredVisibleBlinkingBallsCount);
                }
            }

            CalculateCurrentExpansion();
        }

        //Blinking
        if (deltaTimeBlink >= 1 / blinksPerSecond) {
            if (bBlinking) {
                Blink();
            }

            deltaTimeBlink = 0;
        }

        //Movement
        transform.Translate(Vector2.left * Time.deltaTime * movespeed); // + (Vector2.up * m_hitAngleMod)            
    }

    private void AdjustBlinkingBallVisibility(int desiredVisibleBlinkingBallsCount) {
        var n_ballsActivated = 0;

        for (var i = 0; i < m_blinkingBallList.Count; i++) {
            if (i <= desiredVisibleBlinkingBallsCount - 1) {
                m_blinkingBallList[i].SetActive(true);
                n_ballsActivated++;
            }
            else {
                m_blinkingBallList[i].SetActive(false);
            }
        }

        currentlyVisibleBlinkingBalls = n_ballsActivated;
    }

    private int DetermineDesiredBallCount() {
        if (expansionFactor >= 0 && expansionFactor < 0.1f) {
            return 1;
        }

        if (expansionFactor >= 0.1f && expansionFactor < 0.2f) {
            return 2;
        }

        if (expansionFactor >= 0.2f && expansionFactor < 0.3f) {
            return 3;
        }

        if (expansionFactor >= 0.3f && expansionFactor < 0.5f) {
            return 4;
        }

        if (expansionFactor >= 0.5f && expansionFactor <= 1f) {
            return 5;
        }

        return 1;
    }


    private void ChangeExpansionMovementbased() {
        deltaTimeExpand += Time.deltaTime;

        if (player.IsMoving()) {
            if (expansionFactor > 0) {
                expansionFactor = expansionFactor - modifierShrinking;
            }
            else {
                expansionFactor = 0;
            }

            deltaTimeExpand = 0;
        }
        else {
            if (expansionFactor < 1) {
                if (deltaTimeExpand >= timeUntilExpansion) {
                    expansionFactor = expansionFactor + modifierExpanding;
                }
            }
            else {
                expansionFactor = 1f;
            }
        }
    }

    private void CalculateCurrentExpansion() {
        var leftY = leftBorderMarker.transform.position.y;
        var rightY = rightBorderMarker.transform.position.y;

        var leftX = leftBorderMarker.transform.position.x;
        var rightX = rightBorderMarker.transform.position.x;

        var currentDeltaX = rightX - leftX;

        var differenceBetweenDeltas = m_maximumDeltaX * expansionFactor - currentDeltaX;
        var differenceBetweenDeltasHalved = differenceBetweenDeltas * 0.5f;

        var newLeftX = leftX - differenceBetweenDeltasHalved;
        var newRightX = rightX + differenceBetweenDeltasHalved;

        leftBorderMarker.SetPositionAndRotation(new Vector2(newLeftX, leftY), Quaternion.identity);
        rightBorderMarker.SetPositionAndRotation(new Vector2(newRightX, rightY), Quaternion.identity);
    }

    private void Blink() {
        var leftXBorder = leftBorderMarker.transform.position.x;
        var rightXBorder = rightBorderMarker.transform.position.x;

        var deltaX = rightXBorder - leftXBorder;
        var lengthUnit = deltaX / currentlyVisibleBlinkingBalls; //m_blinkingBallList.Count

        //Debug.Log("deltaX " + deltaX + " lengthUnit " + lengthUnit);

        for (var i = 0; i < m_blinkingBallList.Count; i++) {
            var ball = m_blinkingBallList[i];

            if (ball.activeSelf == false) {
                continue;
            }

            var leftRange = leftXBorder + lengthUnit * i;
            var rightRange = leftXBorder + lengthUnit * (i + 1);
            //Debug.Log("index " + i + " leftXBorder " + leftXBorder + " rightXBorder " + rightXBorder + " leftRange " + leftRange + " rightRange " + rightRange);

            var xValueBlink = Random.Range(leftRange, rightRange);
            var yValueBlink = ball.transform.position.y;
            ball.transform.position = new Vector2(xValueBlink, yValueBlink);
        }
    }
}