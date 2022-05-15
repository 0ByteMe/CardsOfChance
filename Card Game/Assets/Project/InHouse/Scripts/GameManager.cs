using System.Collections;
using UnityEngine;
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
    [SerializeField] [Range(0.05f, 3f)] float cardPlacementDuration;    
    [SerializeField] [Range(0.01f, 3f)] float delayBeforeEnemyCardsStartToGetMoved;    
    [SerializeField] [Range(0.01f, 3f)] public float delayToAllowDraw;
    [Space(20)]    
    [SerializeField] private Spline cameraSpline;
    [SerializeField] Transform myCamera;    
    [SerializeField] Vector3 cameraStartPosition;
    [SerializeField] float cameraStartTweenDuration;  

    bool canDrawCards;

    ScoreManager scoreManager;
    CardDecks cardDecks;
    CardBattler cardBattler;
    CardSpawner cardSpawner;
    AudioManager audioManager;
    UIManager uiManager;

    private void Awake()
    {
        scoreManager = GetComponent<ScoreManager>();
        cardDecks = GetComponent<CardDecks>();
        cardBattler = GetComponent<CardBattler>();
        cardSpawner = GetComponent<CardSpawner>();       
        audioManager = GetComponent<AudioManager>();
        uiManager = GetComponent<UIManager>();
    }
    private void Start()
    {
        myCamera.transform.position = cameraStartPosition;
        uiManager.startGameUI.SetActive(true);        

        StopAbilityToDrawCards();
        StartCoroutine(AllowDrawingOfCards(delayToAllowDraw));
    }
    public void StartGame()
    {
        StartCoroutine(TweenCameraToBattleArea(cameraStartTweenDuration));        
    }    
    public void DrawCards()
    {
        if (canDrawCards)
        {
            StartCoroutine(DrawCardSequence());
        }
    }
    public void StopAbilityToDrawCards()
    {
        canDrawCards = false;
        uiManager.DisableDrawButton();
    }   
    public void PlayAgain()
    {
        scoreManager.ResetScores();
        cardSpawner.InstantiateCardDecks();
        cardDecks.ShuffleCardDecks();
        uiManager.gameOverUI.SetActive(false);
        uiManager.drawCardsButtonUI.SetActive(true);
        StopAbilityToDrawCards();
        StartCoroutine(AllowDrawingOfCards(delayToAllowDraw));
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    private IEnumerator TweenCameraToBattleArea(float duration)
    {
        Tween.Spline(cameraSpline, myCamera.transform, 0, 1f, false, duration, 0, Tween.EaseInOut, Tween.LoopType.None);
        uiManager.startGameUI.SetActive(false);
        yield return new WaitForSeconds(duration);
        uiManager.drawCardsButtonUI.SetActive(true);
        uiManager.scoreUI.SetActive(true);
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
        yield return PlaceCardSFX();
        yield return PlaceCard(cardDecks.shuffledPlayerCards[0], playerBattleCard1, cardPlacementDuration);
        yield return PlaceCardSFX();
        yield return PlaceCard(cardDecks.shuffledPlayerCards[1], playerBattleCard2, cardPlacementDuration);
        yield return PlaceCardSFX();
        yield return PlaceCard(cardDecks.shuffledPlayerCards[2], playerBattleCard3, cardPlacementDuration);

        yield return cardDecks.AddToCurrentBattleCards(cardDecks.playerBattleCards, cardDecks.shuffledPlayerCards[0], cardDecks.shuffledPlayerCards[1], cardDecks.shuffledPlayerCards[2], cardPlacementDuration);
        yield return cardDecks.RemoveCardsFromDeck(cardDecks.shuffledPlayerCards);
        yield return cardDecks.RemoveCardsFromDeck(cardDecks.playerCards);
        yield return new WaitForSeconds(delayBeforeEnemyCardsArePlaced);
    }
    private IEnumerator DrawEnemyCards()
    {
        yield return PlaceCardSFX();
        yield return PlaceCard(cardDecks.shuffledEnemyCards[0], enemyBattleCard1, cardPlacementDuration);
        yield return PlaceCardSFX();
        yield return PlaceCard(cardDecks.shuffledEnemyCards[1], enemyBattleCard2, cardPlacementDuration);
        yield return PlaceCardSFX();
        yield return PlaceCard(cardDecks.shuffledEnemyCards[2], enemyBattleCard3, cardPlacementDuration);

        yield return cardDecks.AddToCurrentBattleCards(cardDecks.enemyBattleCards, cardDecks.shuffledEnemyCards[0], cardDecks.shuffledEnemyCards[1], cardDecks.shuffledEnemyCards[2], cardPlacementDuration);
        yield return cardDecks.RemoveCardsFromDeck(cardDecks.shuffledEnemyCards);
        yield return cardDecks.RemoveCardsFromDeck(cardDecks.enemyCards);
    }
    public IEnumerator AllowDrawingOfCards(float delay)
    {
        yield return new WaitForSeconds(delay);
        canDrawCards = true;
        uiManager.EnableDrawButton();
    }
    public IEnumerator WinOrLoseGame()
    {
        uiManager.drawCardsButtonUI.SetActive(false);
        yield return cardBattler.Delay(1f);

        if(scoreManager.PlayerScore == scoreManager.EnemyScore)
        {
            uiManager.drawCardsButtonUI.SetActive(false);
            uiManager.gameOverUI.GetComponentInChildren<TextMeshProUGUI>().text = "Its a Tie!";
            uiManager.gameOverUI.SetActive(true);
            audioManager.PlayGameOverTieSFX();
        }
        else if(scoreManager.PlayerScore > scoreManager.EnemyScore)
        {
            uiManager.drawCardsButtonUI.SetActive(false);
            uiManager.gameOverUI.GetComponentInChildren<TextMeshProUGUI>().text = "You Win!";
            uiManager.gameOverUI.SetActive(true);    
            audioManager.PlayGameOverWinSFX();
        }
        else
        {
            uiManager.drawCardsButtonUI.SetActive(false);
            uiManager.gameOverUI.GetComponentInChildren<TextMeshProUGUI>().text = "You Lose!";
            uiManager.gameOverUI.SetActive(true);  
            audioManager.PlayGameOverLoseSFX();
        }
    }
    private IEnumerator PlaceCard(Card card, Transform position, float placementDuration)
    {
        Tween.Position(card.transform, position.position, placementDuration, 0);
        yield return new WaitForSeconds(placementDuration);
    }
    private IEnumerator PlaceCardSFX()
    {
        audioManager.PlayPlaceCardsSFX();
        yield return null;
    }

   
}
