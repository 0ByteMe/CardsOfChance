using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DigitalRuby.Tween;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Card Positions")]
    [SerializeField] Transform playerBattleCard1;
    [SerializeField] Transform playerBattleCard2;
    [SerializeField] Transform playerBattleCard3;
    [SerializeField] Transform enemyBattleCard1;
    [SerializeField] Transform enemyBattleCard2;
    [SerializeField] Transform enemyBattleCard3;
    [Header("Time to Place Battle Cards")]
    [SerializeField] float Card1PlacementDuration;
    [SerializeField] float Card2PlacementDuration;
    [SerializeField] public float Card3PlacementDuration;

    [SerializeField] Button drawCardsButton;
    [SerializeField] public bool canDrawCards;
    
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
        DisableDrawButton();
        StartCoroutine(cardBattler.AllowDrawingOfCards(cardBattler.delayToAllowDraw));
    }

    public void DrawCards()
    {
        if (canDrawCards)
        {
            StartCoroutine(DrawCardSequence());           
        }
    }
    private IEnumerator DrawCardSequence()
    {
        DisableDrawButton();
        yield return DrawPlayerCards(Card1PlacementDuration);
        yield return DrawEnemyCards();
        yield return cardBattler.CardBattle();
    }

    private IEnumerator DrawPlayerCards(float duration)
    {
        //Moves card of Shuffled List to Card Battle Position
        System.Action<ITween<Vector3>> updatePlayerCard1Pos = (t) =>
        {
            cardDecks.shuffledPlayerCards[0].transform.position = t.CurrentValue;
        };
        TweenFactory.Tween(null, cardDecks.shuffledPlayerCards[0].transform.position, playerBattleCard1.position, Card1PlacementDuration, TweenScaleFunctions.CubicEaseIn, updatePlayerCard1Pos);
                

        //Moves card of Shuffled List to Card Battle Position
        System.Action<ITween<Vector3>> updatePlayerCard2Pos = (t) =>
        {
            cardDecks.shuffledPlayerCards[1].transform.position = t.CurrentValue;
        };
        TweenFactory.Tween(null, cardDecks.shuffledPlayerCards[1].transform.position, playerBattleCard2.position, Card2PlacementDuration, TweenScaleFunctions.CubicEaseIn, updatePlayerCard2Pos);


        //Moves card of Shuffled List to Card Battle Position
        System.Action<ITween<Vector3>> updatePlayerCard3Pos = (t) =>
        {
            cardDecks.shuffledPlayerCards[2].transform.position = t.CurrentValue;
        };
        TweenFactory.Tween(null, cardDecks.shuffledPlayerCards[2].transform.position, playerBattleCard3.position, Card3PlacementDuration, TweenScaleFunctions.CubicEaseIn, updatePlayerCard3Pos);

        //Adds cards to Battle Playing Cards
        cardDecks.AddToCurrentBattleCards(cardDecks.playerBattleCards, cardDecks.shuffledPlayerCards[0], cardDecks.shuffledPlayerCards[1], cardDecks.shuffledPlayerCards[2]);

        StartCoroutine(cardDecks.RemoveCardsFromDeck(cardDecks.shuffledPlayerCards));   
        
        yield return new WaitForSeconds(duration);
    }

    private IEnumerator DrawEnemyCards()
    {
        System.Action<ITween<Vector3>> updateEnemyCard1Pos = (t) =>
        {
            cardDecks.shuffledEnemyCards[0].transform.position = t.CurrentValue;
        };
        TweenFactory.Tween(null, cardDecks.shuffledEnemyCards[0].transform.position, enemyBattleCard1.position, Card1PlacementDuration , TweenScaleFunctions.CubicEaseIn, updateEnemyCard1Pos);


        System.Action<ITween<Vector3>> updateEnemyCard2Pos = (t) =>
        {
            cardDecks.shuffledEnemyCards[1].transform.position = t.CurrentValue;
        };
        TweenFactory.Tween(null, cardDecks.shuffledEnemyCards[1].transform.position,  enemyBattleCard2.position, Card2PlacementDuration, TweenScaleFunctions.CubicEaseIn, updateEnemyCard2Pos);


        System.Action<ITween<Vector3>> updateEnemyCard3Pos = (t) =>
        {
            cardDecks.shuffledEnemyCards[2].transform.position = t.CurrentValue;
        };
        TweenFactory.Tween(null, cardDecks.shuffledEnemyCards[2].transform.position,  enemyBattleCard3.position, Card3PlacementDuration, TweenScaleFunctions.CubicEaseIn, updateEnemyCard3Pos);

        cardDecks.AddToCurrentBattleCards(cardDecks.enemyBattleCards, cardDecks.shuffledEnemyCards[0], cardDecks.shuffledEnemyCards[1], cardDecks.shuffledEnemyCards[2]);

        StartCoroutine(cardDecks.RemoveCardsFromDeck(cardDecks.shuffledEnemyCards));

        yield return null;
    }       
    
    private void DisableDrawButton()
    {
        drawCardsButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.red;        
        drawCardsButton.enabled = false;        
    }
    public void EnableDrawButton()
    {
        drawCardsButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.green;        
        drawCardsButton.enabled = true;
    }

    public void AllowNextDraw()
    {
        canDrawCards = true;
    }
    public void DisableNextDraw()
    {
        canDrawCards = false;
    }
    private IEnumerator DelayThenAllowFirstDraw(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        EnableDrawButton();
    }
}
