using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private const float LANE_DISTANCE = 5;
    private int lane = 0;

    //Gameplay
    public bool isGameOver = false;
    public bool isGameStarted = false;

    //Scores
    public int score = 0;
    public int HighScore = 0;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        HighScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    // Update is called once per frame
    void Update()
    {
        //playerinputs
        if (!isGameOver)
        {
            if (MobileInput.Instance.SwipeLeft)
                lane--;
            else if (MobileInput.Instance.SwipeRight)
                lane++;
            Mathf.Clamp(lane, -1, 1);

            //move across the 3 lanes
            Vector3 moveDirection = (Vector3.right * lane).normalized;
            rb.MovePosition(moveDirection * LANE_DISTANCE);
        }

        if (MobileInput.Instance.Tap && !isGameStarted)
        {
            isGameStarted = true;
            StartCoroutine(FindObjectOfType<SegmentSpawner>().SpawnSegment());
            UIManager.Instance.setUpGameUI();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        crash();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isGameOver)
        {
            score++;
            UIManager.Instance.updateScore(score);
        }
    }

    private void crash()
    {
        isGameOver = true;
        Debug.Log("Game Over");

        //drop the plane
        rb.AddForce(Vector3.down * LANE_DISTANCE, ForceMode.Impulse);
        rb.AddTorque(Vector3.left *  LANE_DISTANCE, ForceMode.Impulse);
        rb.useGravity = true;

        //set highscore
        if (score >= HighScore)
        {
            HighScore = score;
            PlayerPrefs.SetInt("HighScore", HighScore);
        }

        UIManager.Instance.setUpDeathPanel(score, HighScore);
    }
}
