using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DigitalRuby.Tween;

public class GameManager : MonoBehaviour
{
    [Header("Card Positions")]
    [SerializeField] Transform _playerCard1Transform;
    [SerializeField] Transform _playerCard2Transform;
    [SerializeField] Transform _playerCard3Transform;
    [SerializeField] Transform _enemyCard1Transform;
    [SerializeField] Transform _enemyCard2Transform;
    [SerializeField] Transform _enemyCard3Transform;
    [Header("Time to Deal Cards")]
    [SerializeField] float card1Duration;
    [SerializeField] float card2Duration;
    [SerializeField] float card3Duration;

    [SerializeField] bool buttonCanClick;

    CardSpawner cardSpawner;
    ScoreManager scoreManager;

    private void Awake()
    {
        cardSpawner = FindObjectOfType<CardSpawner>();
        scoreManager = GetComponent<ScoreManager>();
    }

    private void Start()
    {
        buttonCanClick = true;
    }

    public void DrawCards()
    {
        if (buttonCanClick)
        {
            DrawPlayerCards();
            DrawEnemyCards();
        }
    }
    private void DrawPlayerCards()
    {
        buttonCanClick = false;

        //Moves card at index 0 of Shuffled List to Card Battle Position
        System.Action<ITween<Vector3>> updatePlayerCard1Pos = (t) =>
        {
            cardSpawner.shuffledPlayerCards[0].transform.position = t.CurrentValue;
        };
        TweenFactory.Tween(null, cardSpawner.shuffledPlayerCards[0].transform.position, _playerCard1Transform.position, card1Duration, TweenScaleFunctions.CubicEaseIn, updatePlayerCard1Pos);

        //Moves card at index 1 of Shuffled List to Card Battle Position
        System.Action<ITween<Vector3>> updatePlayerCard2Pos = (t) =>
        {
            cardSpawner.shuffledPlayerCards[1].transform.position = t.CurrentValue;
        };
        TweenFactory.Tween(null, cardSpawner.shuffledPlayerCards[1].transform.position, _playerCard2Transform.position, card2Duration, TweenScaleFunctions.CubicEaseIn, updatePlayerCard2Pos);

        //Moves card at index 2 of Shuffled List to Card Battle Position
        System.Action<ITween<Vector3>> updatePlayerCard3Pos = (t) =>
        {
            cardSpawner.shuffledPlayerCards[2].transform.position = t.CurrentValue;
        };
        TweenFactory.Tween(null, cardSpawner.shuffledPlayerCards[2].transform.position, _playerCard3Transform.position, card3Duration, TweenScaleFunctions.CubicEaseIn, updatePlayerCard3Pos);

        Invoke("RemoveDrawnPlayerCards", 1f);
        StartCoroutine(PauseBeforeClicking());
    }
    private void DrawEnemyCards()
    {
        System.Action<ITween<Vector3>> updateEnemyCard1Pos = (t) =>
        {
            cardSpawner.shuffledEnemyCards[0].transform.position = t.CurrentValue;
        };
        TweenFactory.Tween(null, cardSpawner.shuffledEnemyCards[0].transform.position, _enemyCard1Transform.position, card1Duration , TweenScaleFunctions.CubicEaseIn, updateEnemyCard1Pos);


        System.Action<ITween<Vector3>> updateEnemyCard2Pos = (t) =>
        {
            cardSpawner.shuffledEnemyCards[1].transform.position = t.CurrentValue;
        };
        TweenFactory.Tween(null, cardSpawner.shuffledEnemyCards[1].transform.position,  _enemyCard2Transform.position, card2Duration, TweenScaleFunctions.CubicEaseIn, updateEnemyCard2Pos);


        System.Action<ITween<Vector3>> updateEnemyCard3Pos = (t) =>
        {
            cardSpawner.shuffledEnemyCards[2].transform.position = t.CurrentValue;
        };
        TweenFactory.Tween(null, cardSpawner.shuffledEnemyCards[2].transform.position,  _enemyCard3Transform.position, card3Duration, TweenScaleFunctions.CubicEaseIn, updateEnemyCard3Pos);

        Invoke("RemoveDrawnEnemyCards", 1f);
        StartCoroutine(PauseBeforeClicking());
    }
    private IEnumerator PauseBeforeClicking()
    {
        yield return new WaitForSeconds(2f);

        buttonCanClick = true;
    }
    private void RemoveDrawnPlayerCards()
    {
        cardSpawner.shuffledPlayerCards.RemoveAt(0);
        cardSpawner.shuffledPlayerCards.RemoveAt(0);
        cardSpawner.shuffledPlayerCards.RemoveAt(0);
    }    
    private void RemoveDrawnEnemyCards()
    {
        cardSpawner.shuffledEnemyCards.RemoveAt(0);
        cardSpawner.shuffledEnemyCards.RemoveAt(0);
        cardSpawner.shuffledEnemyCards.RemoveAt(0);
    }
}
