using System.Collections;
using UnityEngine;
using DigitalRuby.Tween;
using Pixelplacement;
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
    [SerializeField] private Spline cameraSpline;
    [SerializeField] Transform myCamera;    
    [SerializeField] Vector3 cameraStartPosition;
    [SerializeField] float cameraStartTweenDuration;
    [SerializeField] DisplayObject startGameUI;
    [SerializeField] DisplayObject scoreUI;
    [SerializeField] DisplayObject drawCardsButtonUI;
    [SerializeField] DisplayObject gameOverUI;
    [SerializeField] GameObject playAgainButton;


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
        myCamera.transform.position = cameraStartPosition;
        startGameUI.SetActive(true);        

        CalculateLongestCardPlacementDuration();
        StopAbilityToDrawCards();
        StartCoroutine(AllowDrawingOfCards(delayToAllowDraw));
    }
    public void StartGame()
    {
        StartCoroutine(TweenCameraToBattleArea(cameraStartTweenDuration));        
    }
    private IEnumerator TweenCameraToBattleArea(float duration)
    {
        Tween.Spline(cameraSpline, myCamera, 0, 1f, false, duration, 0, Tween.EaseInOut, Tween.LoopType.None);
        startGameUI.SetActive(false);
        yield return new WaitForSeconds(duration);
        drawCardsButtonUI.SetActive(true);
        scoreUI.SetActive(true);
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
        Tween.Position(cardDecks.shuffledPlayerCards[0].transform, playerBattleCard1.position, card1PlacementDuration, 0);
        Tween.Position(cardDecks.shuffledPlayerCards[1].transform, playerBattleCard2.position, card2PlacementDuration, 0);
        Tween.Position(cardDecks.shuffledPlayerCards[2].transform, playerBattleCard3.position, card3PlacementDuration, 0);

        yield return cardDecks.AddToCurrentBattleCards(cardDecks.playerBattleCards, cardDecks.shuffledPlayerCards[0], cardDecks.shuffledPlayerCards[1], cardDecks.shuffledPlayerCards[2], longestCardPlacementDuration);
        yield return cardDecks.RemoveCardsFromDeck(cardDecks.shuffledPlayerCards);
        yield return new WaitForSeconds(delayBeforeEnemyCardsArePlaced);
    }
    private IEnumerator DrawEnemyCards()
    {
        Tween.Position(cardDecks.shuffledEnemyCards[0].transform, enemyBattleCard1.position, card1PlacementDuration, 0);
        Tween.Position(cardDecks.shuffledEnemyCards[1].transform, enemyBattleCard2.position, card2PlacementDuration, 0);
        Tween.Position(cardDecks.shuffledEnemyCards[2].transform, enemyBattleCard3.position, card3PlacementDuration, 0);

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
    public IEnumerator WinOrLoseGame()
    {
        yield return cardBattler.Delay(2f);

        if(scoreManager.PlayerScore == scoreManager.EnemyScore)
        {
            drawCardsButtonUI.SetActive(false);
            gameOverUI.SetActive(true);            
        }
        else if(scoreManager.PlayerScore > scoreManager.EnemyScore)
        {
            drawCardsButtonUI.SetActive(false);
            gameOverUI.SetActive(true);            
        }
        else
        {
            drawCardsButtonUI.SetActive(false);
            gameOverUI.SetActive(true);            
        }
    }

    public void PlayAgain()
    {
        NewGame();
    }

    private void NewGame()
    {
        scoreManager.ResetScores();        
        cardDecks.ShuffleCardDecks();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
