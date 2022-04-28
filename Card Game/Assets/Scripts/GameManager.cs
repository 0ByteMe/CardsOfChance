using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DigitalRuby.Tween;

public class GameManager : MonoBehaviour
{
    [Header("Card Positions")]
    [SerializeField] Transform playerCard1Transform;
    [SerializeField] Transform playerCard2Transform;
    [SerializeField] Transform playerCard3Transform;
    [SerializeField] Transform enemyCard1Transform;
    [SerializeField] Transform enemyCard2Transform;
    [SerializeField] Transform enemyCard3Transform;
    [Header("Time to Deal Cards")]
    [SerializeField] float card1Duration;
    [SerializeField] float card2Duration;
    [SerializeField] float card3Duration;

    [SerializeField] bool buttonCanClick;
    
    ScoreManager scoreManager;
    CardDecks cardDecks;
    CardBattler cardBattler;

    private void Awake()
    {        
        scoreManager = GetComponent<ScoreManager>();
        cardDecks = GetComponent<CardDecks>();
        cardBattler = GetComponent<CardBattler>();
    }

    private void Start()
    {
        StartCoroutine(PauseBeforeClicking());        
    }

    public void DrawCards()
    {
        if (buttonCanClick)
        {
            DrawPlayerCards();
            DrawEnemyCards();
            cardBattler.CardBattle();            
        }
    }

    private void DrawPlayerCards()
    {
        buttonCanClick = false;

        //Moves card at index 0 of Shuffled List to Card Battle Position
        System.Action<ITween<Vector3>> updatePlayerCard1Pos = (t) =>
        {
            cardDecks.shuffledPlayerCards[0].transform.position = t.CurrentValue;
        };
        TweenFactory.Tween(null, cardDecks.shuffledPlayerCards[0].transform.position, playerCard1Transform.position, card1Duration, TweenScaleFunctions.CubicEaseIn, updatePlayerCard1Pos);


        //Moves card at index 1 of Shuffled List to Card Battle Position
        System.Action<ITween<Vector3>> updatePlayerCard2Pos = (t) =>
        {
            cardDecks.shuffledPlayerCards[1].transform.position = t.CurrentValue;
        };
        TweenFactory.Tween(null, cardDecks.shuffledPlayerCards[1].transform.position, playerCard2Transform.position, card2Duration, TweenScaleFunctions.CubicEaseIn, updatePlayerCard2Pos);


        //Moves card at index 2 of Shuffled List to Card Battle Position
        System.Action<ITween<Vector3>> updatePlayerCard3Pos = (t) =>
        {
            cardDecks.shuffledPlayerCards[2].transform.position = t.CurrentValue;
        };
        TweenFactory.Tween(null, cardDecks.shuffledPlayerCards[2].transform.position, playerCard3Transform.position, card3Duration, TweenScaleFunctions.CubicEaseIn, updatePlayerCard3Pos);


        cardDecks.AddToCurrentBattleCards(cardDecks.playerBattleCards, cardDecks.shuffledPlayerCards[0], cardDecks.shuffledPlayerCards[1], cardDecks.shuffledPlayerCards[2]);

        StartCoroutine(cardDecks.RemoveCardsFromDeck(cardDecks.shuffledPlayerCards));
        StartCoroutine(PauseBeforeClicking());
    }

    private void DrawEnemyCards()
    {
        System.Action<ITween<Vector3>> updateEnemyCard1Pos = (t) =>
        {
            cardDecks.shuffledEnemyCards[0].transform.position = t.CurrentValue;
        };
        TweenFactory.Tween(null, cardDecks.shuffledEnemyCards[0].transform.position, enemyCard1Transform.position, card1Duration , TweenScaleFunctions.CubicEaseIn, updateEnemyCard1Pos);


        System.Action<ITween<Vector3>> updateEnemyCard2Pos = (t) =>
        {
            cardDecks.shuffledEnemyCards[1].transform.position = t.CurrentValue;
        };
        TweenFactory.Tween(null, cardDecks.shuffledEnemyCards[1].transform.position,  enemyCard2Transform.position, card2Duration, TweenScaleFunctions.CubicEaseIn, updateEnemyCard2Pos);


        System.Action<ITween<Vector3>> updateEnemyCard3Pos = (t) =>
        {
            cardDecks.shuffledEnemyCards[2].transform.position = t.CurrentValue;
        };
        TweenFactory.Tween(null, cardDecks.shuffledEnemyCards[2].transform.position,  enemyCard3Transform.position, card3Duration, TweenScaleFunctions.CubicEaseIn, updateEnemyCard3Pos);

        cardDecks.AddToCurrentBattleCards(cardDecks.enemyBattleCards, cardDecks.shuffledEnemyCards[0], cardDecks.shuffledEnemyCards[1], cardDecks.shuffledEnemyCards[2]);

        StartCoroutine(cardDecks.RemoveCardsFromDeck(cardDecks.shuffledEnemyCards));
        StartCoroutine(PauseBeforeClicking());
    }   


    private IEnumerator PauseBeforeClicking()
    {
        yield return new WaitForSeconds(2.5f);

        buttonCanClick = true;
    }   
}
