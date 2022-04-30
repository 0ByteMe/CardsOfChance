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
    [Header("Timing")]
    public float longestCardPlacementDuration; 
    [SerializeField] [Range(0.05f, 3f)] float card1PlacementDuration;
    [SerializeField] [Range(0.05f, 3f)] float card2PlacementDuration;
    [SerializeField] [Range(0.05f, 3f)] public float card3PlacementDuration;
    [Space(20)]
    [Tooltip("Delay before enemy cards can start to move after all 3 of the player's are placed.")] 
    [SerializeField] [Range(0.01f, 3f)] float delayBeforeEnemyCardsStartToGetMoved;        
    [Tooltip("Delay before the player can click Draw after all battles are completed.")] 
    [SerializeField] [Range(0.01f, 3f)] public float delayToAllowDraw;
    [Space(20)]
    [SerializeField] Button drawCardsButton;

    bool canDrawCards;
    
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
        //this sets the slowest's cards move duration to a variable
        //Used to stop cards being added too quickly at the end of Drawing Player/Enemy Cards
        CalculateLongestCardPlacementDuration();

        StopAbilityToDrawCards();
        StartCoroutine(AllowDrawingOfCards(delayToAllowDraw));
    }

    private void CalculateLongestCardPlacementDuration()
    {
        if (card1PlacementDuration > card2PlacementDuration && card1PlacementDuration > card3PlacementDuration)
        {
            longestCardPlacementDuration = card1PlacementDuration;
        }
        else if (card2PlacementDuration > card1PlacementDuration && card2PlacementDuration > card3PlacementDuration)
        {
            longestCardPlacementDuration = card2PlacementDuration;
        }
        else
        {
            longestCardPlacementDuration = card3PlacementDuration;
        }        
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
        StopAbilityToDrawCards();
        yield return DrawPlayerCards(delayBeforeEnemyCardsStartToGetMoved);
        yield return DrawEnemyCards();
        yield return cardBattler.CardBattle();
    }

    private IEnumerator DrawPlayerCards(float delayBeforeEnemyCardsArePlaced)
    {
        //Moves card of Shuffled List to Card Battle Position
        System.Action<ITween<Vector3>> updatePlayerCard1Pos = (t) =>
        {
            cardDecks.shuffledPlayerCards[0].transform.position = t.CurrentValue;
        };
        TweenFactory.Tween(null, cardDecks.shuffledPlayerCards[0].transform.position, playerBattleCard1.position, card1PlacementDuration, TweenScaleFunctions.CubicEaseIn, updatePlayerCard1Pos);
                

        //Moves card of Shuffled List to Card Battle Position
        System.Action<ITween<Vector3>> updatePlayerCard2Pos = (t) =>
        {
            cardDecks.shuffledPlayerCards[1].transform.position = t.CurrentValue;
        };
        TweenFactory.Tween(null, cardDecks.shuffledPlayerCards[1].transform.position, playerBattleCard2.position, card2PlacementDuration, TweenScaleFunctions.CubicEaseIn, updatePlayerCard2Pos);


        //Moves card of Shuffled List to Card Battle Position
        System.Action<ITween<Vector3>> updatePlayerCard3Pos = (t) =>
        {
            cardDecks.shuffledPlayerCards[2].transform.position = t.CurrentValue;
        };
        TweenFactory.Tween(null, cardDecks.shuffledPlayerCards[2].transform.position, playerBattleCard3.position, card3PlacementDuration, TweenScaleFunctions.CubicEaseIn, updatePlayerCard3Pos);

        
        yield return cardDecks.AddToCurrentBattleCards(cardDecks.playerBattleCards, cardDecks.shuffledPlayerCards[0], cardDecks.shuffledPlayerCards[1], cardDecks.shuffledPlayerCards[2], longestCardPlacementDuration);
        yield return cardDecks.RemoveCardsFromDeck(cardDecks.shuffledPlayerCards);           
        yield return new WaitForSeconds(delayBeforeEnemyCardsArePlaced);
    }
    private IEnumerator DrawEnemyCards()
    {
        System.Action<ITween<Vector3>> updateEnemyCard1Pos = (t) =>
        {
            cardDecks.shuffledEnemyCards[0].transform.position = t.CurrentValue;
        };
        TweenFactory.Tween(null, cardDecks.shuffledEnemyCards[0].transform.position, enemyBattleCard1.position, card1PlacementDuration , TweenScaleFunctions.CubicEaseIn, updateEnemyCard1Pos);


        System.Action<ITween<Vector3>> updateEnemyCard2Pos = (t) =>
        {
            cardDecks.shuffledEnemyCards[1].transform.position = t.CurrentValue;
        };
        TweenFactory.Tween(null, cardDecks.shuffledEnemyCards[1].transform.position,  enemyBattleCard2.position, card2PlacementDuration, TweenScaleFunctions.CubicEaseIn, updateEnemyCard2Pos);


        System.Action<ITween<Vector3>> updateEnemyCard3Pos = (t) =>
        {
            cardDecks.shuffledEnemyCards[2].transform.position = t.CurrentValue;
        };
        TweenFactory.Tween(null, cardDecks.shuffledEnemyCards[2].transform.position,  enemyBattleCard3.position, card3PlacementDuration, TweenScaleFunctions.CubicEaseIn, updateEnemyCard3Pos);

        yield return cardDecks.AddToCurrentBattleCards(cardDecks.enemyBattleCards, cardDecks.shuffledEnemyCards[0], cardDecks.shuffledEnemyCards[1], cardDecks.shuffledEnemyCards[2], longestCardPlacementDuration);
        yield return cardDecks.RemoveCardsFromDeck(cardDecks.shuffledEnemyCards);           
    }

    public IEnumerator AllowDrawingOfCards(float delay)
    {
        yield return new WaitForSeconds(delay);
        canDrawCards = true;
        EnableDrawButton();        
    }   
    public void StopAbilityToDrawCards()
    {
        canDrawCards = false;
        DisableDrawButton();        
    } 
    public void EnableDrawButton()
    {
        drawCardsButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.green;        
        drawCardsButton.enabled = true;
    }  
    private void DisableDrawButton()
    {
        drawCardsButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.red;        
        drawCardsButton.enabled = false;        
    }
    
}
