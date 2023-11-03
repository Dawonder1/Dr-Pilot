using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; set; }

    [SerializeField] GameObject deathPanel, titleScreen;
    [SerializeField] TextMeshProUGUI scoreText, scoreDead, highScoreDead;

    void Start()
    {
        Instance = this;
    }

    public void updateScore(int score)
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void playAgian()
    {
        SceneManager.LoadScene("Game");
    }

    public void setUpDeathPanel(int score, int highScore)
    {
        deathPanel.SetActive(true);
        scoreDead.text = "Score: " + score.ToString();
        highScoreDead.text = "Score: " + highScore.ToString();
    }

    public void setUpGameUI()
    {
        //remove game screen
        titleScreen.SetActive(false);
        //pop the score UI
        scoreText.gameObject.SetActive(true);
    }
}
