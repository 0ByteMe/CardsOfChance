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
}
