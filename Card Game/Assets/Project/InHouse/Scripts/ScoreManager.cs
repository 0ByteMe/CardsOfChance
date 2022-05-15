using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int playerScore;
    [SerializeField] private int enemyScore;

    UIManager uiManager;

    private void Awake()
    {
        uiManager = GetComponent<UIManager>();
    }
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
        uiManager.UpdateScoreUI(uiManager.enemyScoreText, EnemyScore);
    }
    public void AddToPlayerScore()
    {
        playerScore += 1;
        uiManager.UpdateScoreUI(uiManager.playerScoreText, PlayerScore);
    }
    public void ResetScores()
    {
        playerScore = 0;
        uiManager.UpdateScoreUI(uiManager.playerScoreText, PlayerScore);
        enemyScore = 0;
        uiManager.UpdateScoreUI(uiManager.enemyScoreText, EnemyScore);

    }
}
