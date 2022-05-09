using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int playerScore;
    [SerializeField] private int enemyScore;

    [SerializeField] TextMeshProUGUI playerScoreText;
    [SerializeField] TextMeshProUGUI enemyScoreText;

    private void Start()
    {
        playerScore = 0;
        enemyScore = 0;
    }

    public int PlayerScore
    {
        get
        {
            return playerScore;
        }        
    }

    public int EnemyScore
    {
        get
        {
            return enemyScore;
        }
    }

    public void AddToEnemyScore()
    {
        enemyScore += 1;
        enemyScoreText.text = enemyScore.ToString();
    }

    public void AddToPlayerScore()
    {
        playerScore += 1;
        playerScoreText.text = playerScore.ToString();
    }

    public void ResetScores()
    {
        playerScore = 0;
        playerScoreText.text = playerScore.ToString();
        enemyScore = 0;
        enemyScoreText.text = enemyScore.ToString();
    }
}
