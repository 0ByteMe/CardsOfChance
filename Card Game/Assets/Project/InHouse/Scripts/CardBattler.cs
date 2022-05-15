using System.Collections;
using UnityEngine;
using Pixelplacement;

public class CardBattler : MonoBehaviour
{
    [TextArea]
    [SerializeField]
    private string description;    
    [Header("Timing ")]        
    [SerializeField] [Range(0.1f, 3f)] float delayBetweenEachBattle;
    [SerializeField] [Range(0.1f, 3f)] float cameraMoveDuration;
    [SerializeField] [Range(0.1f, 3f)] float delayToRotatingCard;
    [SerializeField] [Range(0.1f, 3f)] float rotateDuration;    
    [SerializeField] [Range(0.1f, 3f)] float delayToRotatingCardDetails;
    [SerializeField] [Range(0.1f, 3f)] float rotateCardDetailsDuration;
    [SerializeField] [Range(0.1f, 3f)] float rotateCardDetailsDownDuration;
    [SerializeField] [Range(0.1f, 3f)] float delayToTakingHit;     
    [SerializeField] [Range(0.1f, 3f)] float delayToBothCardsHits;
    [SerializeField] [Range(0.1f, 3f)] float delayToWinningCardStartToFall;
    [Header("Hit Animation")]
    [SerializeField] Vector3 shakeIntensity;
    [SerializeField] [Range(0.1f, 2f)] float shakeDuration;
    [SerializeField] [Range(0.1f, 2f)] float shakeDelay;

    [SerializeField] private Spline cameraSpline;
    [SerializeField] Transform myCamera;

    private int randomNumber;
    private int lastNumber;

    Vector3 cardRotationAmount = new Vector3(0, 0, -180);
    Vector3 targetSpriteRotationAmount = new Vector3(-90, 0, 0);
    Vector3 targetNameTextRotationAmount = new Vector3(-90, 0, 0);
    Vector3 targetStrengthTextRotationAmount = new Vector3(90, 0, 0);
    Vector3 targetSpriteRotationReturn = new Vector3(90, 0, 0);
    Vector3 targetNameTextRotationReturn = new Vector3(90, 0, 0);
    Vector3 targetStrengthTextRotationReturn = new Vector3(-90, 0, 0);

    CardDecks cardDecks;
    ScoreManager scoreManager;
    GameManager gameManager;
    AudioManager audioManager;

    private void Awake()
    {
        cardDecks = GetComponent<CardDecks>();
        scoreManager = GetComponent<ScoreManager>();        
        gameManager = GetComponent<GameManager>();
        audioManager = GetComponent<AudioManager>();
    }   
    public IEnumerator CardBattle()
    {
       yield return BattleFirstCards();        
       yield return BattleSecondCards();        
       yield return BattleThirdCards();
       yield return cardDecks.RemoveCardsFromDeck(cardDecks.playerBattleCards);
       yield return cardDecks.RemoveCardsFromDeck(cardDecks.enemyBattleCards);
       yield return gameManager.AllowDrawingOfCards(gameManager.delayToAllowDraw + .5f);              
    }
    private IEnumerator BattleFirstCards()
    {
        yield return MoveCamera(0, .4f);

        yield return RotateCards(cardDecks.playerBattleCards[0], cardDecks.enemyBattleCards[0], delayToRotatingCard);        
        
        yield return DelayThenRotateAllCardDetails(cardDecks.playerBattleCards[0], cardDecks.enemyBattleCards[0], delayToRotatingCardDetails);

        yield return DelayThenShowCardStrengthPlayVFX(cardDecks.playerBattleCards[0], cardDecks.enemyBattleCards[0], rotateCardDetailsDuration);
            
        if (cardDecks.playerBattleCards[0].CardStrength > cardDecks.enemyBattleCards[0].CardStrength)
        {
            //PLAYER WINS POINT                       
            yield return HitSequence(cardDecks.enemyBattleCards[0], delayToTakingHit, shakeIntensity, shakeDuration, shakeDelay);
            yield return StartCardFallingSequence(cardDecks.enemyBattleCards[0]);
            yield return DelayBetweenDroppingCards(delayToWinningCardStartToFall);
            yield return StartCardFallingSequence(cardDecks.playerBattleCards[0]);
            scoreManager.AddToPlayerScore();  
        }
        else if (cardDecks.playerBattleCards[0].CardStrength < cardDecks.enemyBattleCards[0].CardStrength)
        {
            //ENEMY WINS POINT            
            yield return HitSequence(cardDecks.playerBattleCards[0], delayToTakingHit, shakeIntensity, shakeDuration, shakeDelay);
            yield return StartCardFallingSequence(cardDecks.playerBattleCards[0]);
            yield return DelayBetweenDroppingCards(delayToWinningCardStartToFall);
            yield return StartCardFallingSequence(cardDecks.enemyBattleCards[0]);
            scoreManager.AddToEnemyScore();            
        }
        else
        {
            //Nobody Wins Point
            yield return HitSequenceForNoWinner(cardDecks.playerBattleCards[0], cardDecks.enemyBattleCards[0], delayToBothCardsHits, shakeIntensity, shakeDuration, shakeDelay);
            StartCoroutine(StartCardFallingSequence(cardDecks.playerBattleCards[0]));
            StartCoroutine(StartCardFallingSequence(cardDecks.enemyBattleCards[0]));            
        }
    }
    private IEnumerator BattleSecondCards()
    {
        yield return MoveCamera(.4f, .7f);

        yield return DelayBetweenBattle(delayBetweenEachBattle);

        yield return RotateCards(cardDecks.playerBattleCards[1], cardDecks.enemyBattleCards[1], delayToRotatingCard);
        
        yield return DelayThenRotateAllCardDetails(cardDecks.playerBattleCards[1], cardDecks.enemyBattleCards[1], delayToRotatingCardDetails);

        yield return DelayThenShowCardStrengthPlayVFX(cardDecks.playerBattleCards[1], cardDecks.enemyBattleCards[1], rotateCardDetailsDuration);

        if (cardDecks.playerBattleCards[1].CardStrength > cardDecks.enemyBattleCards[1].CardStrength)
        {
            //Player wins point
            yield return StartCoroutine(HitSequence(cardDecks.enemyBattleCards[1], delayToTakingHit, shakeIntensity, shakeDuration, shakeDelay));
            yield return StartCardFallingSequence(cardDecks.enemyBattleCards[1]);
            yield return DelayBetweenDroppingCards(delayToWinningCardStartToFall);
            yield return StartCardFallingSequence(cardDecks.playerBattleCards[1]);
            scoreManager.AddToPlayerScore();
        }
        else if (cardDecks.playerBattleCards[1].CardStrength < cardDecks.enemyBattleCards[1].CardStrength)
        {
            //Enemy wins point
            yield return StartCoroutine(HitSequence(cardDecks.playerBattleCards[1], delayToTakingHit, shakeIntensity, shakeDuration, shakeDelay));
            yield return StartCardFallingSequence(cardDecks.playerBattleCards[1]);
            yield return DelayBetweenDroppingCards(delayToWinningCardStartToFall);
            yield return StartCardFallingSequence(cardDecks.enemyBattleCards[1]);
            scoreManager.AddToEnemyScore();
        }
        else
        {
            //Nobody Wins Point
            yield return StartCoroutine(HitSequenceForNoWinner(cardDecks.playerBattleCards[1], cardDecks.enemyBattleCards[1], delayToBothCardsHits, shakeIntensity, shakeDuration, shakeDelay));
            StartCoroutine(StartCardFallingSequence(cardDecks.playerBattleCards[1]));
            StartCoroutine(StartCardFallingSequence(cardDecks.enemyBattleCards[1]));
        }
    }
    private IEnumerator BattleThirdCards()
    {
        yield return MoveCamera(.7f, .9f);

        yield return DelayBetweenBattle(delayBetweenEachBattle);

        yield return RotateCards(cardDecks.playerBattleCards[2], cardDecks.enemyBattleCards[2], delayToRotatingCard);
        
        yield return DelayThenRotateAllCardDetails(cardDecks.playerBattleCards[2], cardDecks.enemyBattleCards[2], delayToRotatingCardDetails);

        yield return DelayThenShowCardStrengthPlayVFX(cardDecks.playerBattleCards[2], cardDecks.enemyBattleCards[2], rotateCardDetailsDuration);

        if (cardDecks.playerBattleCards[2].CardStrength > cardDecks.enemyBattleCards[2].CardStrength)
        {
            //Player wins point
            yield return StartCoroutine(HitSequence(cardDecks.enemyBattleCards[2], delayToTakingHit, shakeIntensity, shakeDuration, shakeDelay));
            yield return StartCardFallingSequence(cardDecks.enemyBattleCards[2]);
            yield return DelayBetweenDroppingCards(delayToWinningCardStartToFall);
            yield return StartCardFallingSequence(cardDecks.playerBattleCards[2]);
            scoreManager.AddToPlayerScore();
        }
        else if (cardDecks.playerBattleCards[2].CardStrength < cardDecks.enemyBattleCards[2].CardStrength)
        {
            //Enemy wins point
            yield return StartCoroutine(HitSequence(cardDecks.playerBattleCards[2], delayToTakingHit, shakeIntensity, shakeDuration, shakeDelay));
            yield return StartCardFallingSequence(cardDecks.playerBattleCards[2]);
            yield return DelayBetweenDroppingCards(delayToWinningCardStartToFall);
            yield return StartCardFallingSequence(cardDecks.enemyBattleCards[2]);
            scoreManager.AddToEnemyScore();
        }
        else
        {
            //Nobody Wins Point
            yield return StartCoroutine(HitSequenceForNoWinner(cardDecks.playerBattleCards[2], cardDecks.enemyBattleCards[2], delayToBothCardsHits, shakeIntensity, shakeDuration, shakeDelay));
            StartCoroutine(StartCardFallingSequence(cardDecks.playerBattleCards[2]));
            StartCoroutine(StartCardFallingSequence(cardDecks.enemyBattleCards[2]));
        }

        Tween.Spline(cameraSpline, myCamera, .9f, 0f, true, 1f, 0, Tween.EaseInOut, Tween.LoopType.None);

        if (cardDecks.shuffledPlayerCards.Count == 0 || cardDecks.shuffledEnemyCards.Count == 0)
        {
            StartCoroutine(gameManager.WinOrLoseGame());
        }       

    }

    private IEnumerator MoveCamera(float startPos, float endPos)
    {
        Tween.Spline(cameraSpline, myCamera, startPos, endPos, true, cameraMoveDuration, 0, Tween.EaseInOut, Tween.LoopType.None);
        yield return new WaitForSeconds(cameraMoveDuration);
    }
    private IEnumerator RotateCards(Card card1, Card card2, float delay)
    {
        yield return new WaitForSeconds(delay);
        Tween.Rotate(card1.transform, cardRotationAmount, Space.World, rotateDuration, 0, Tween.EaseInOutStrong);
        Tween.Rotate(card2.transform, cardRotationAmount, Space.World, rotateDuration, 0, Tween.EaseInOutStrong);
        audioManager.PlayFlipCardSFX();
    }
    private IEnumerator DelayThenRotateAllCardDetails(Card card1, Card card2, float delayBeforeRotating)
    {
        yield return new WaitForSeconds(delayBeforeRotating);
        card1.transform.GetChild(7).gameObject.SetActive(true);
        Tween.Rotate(card1.transform.GetChild(1), targetNameTextRotationAmount, Space.Self, rotateCardDetailsDuration, 0);
        Tween.Rotate(card1.transform.GetChild(2), targetSpriteRotationAmount, Space.Self, rotateCardDetailsDuration, 0);
        Tween.Rotate(card1.transform.GetChild(7), new Vector3(90,0,0), Space.Self, rotateCardDetailsDuration, 0);
        card2.transform.GetChild(7).gameObject.SetActive(true);
        Tween.Rotate(card2.transform.GetChild(1), targetNameTextRotationAmount, Space.Self, rotateCardDetailsDuration, 0);
        Tween.Rotate(card2.transform.GetChild(2), targetSpriteRotationAmount, Space.Self, rotateCardDetailsDuration, 0);
        Tween.Rotate(card2.transform.GetChild(7), new Vector3(90, 0, 0), Space.Self, rotateCardDetailsDuration, 0);
    }
    private IEnumerator DelayThenShowCardStrengthPlayVFX(Card card1, Card card2, float delay)
    {
        yield return new WaitForSeconds(delay);
        card1.transform.GetChild(2).GetChild(1).gameObject.SetActive(true);
        card1.transform.GetChild(2).GetChild(1).GetChild(0).gameObject.SetActive(true);
        card2.transform.GetChild(2).GetChild(1).gameObject.SetActive(true);
        card2.transform.GetChild(2).GetChild(1).GetChild(0).gameObject.SetActive(true);
    }
    private IEnumerator RotateAllCardDetailsBackDown(Card card1)
    {        
        Tween.Rotate(card1.transform.GetChild(1), targetNameTextRotationReturn, Space.Self, rotateCardDetailsDownDuration, 0);
        Tween.Rotate(card1.transform.GetChild(2), targetSpriteRotationReturn, Space.Self, rotateCardDetailsDownDuration, 0);
        Tween.Rotate(card1.transform.GetChild(7), new Vector3(-90, 0, 0), Space.Self, rotateCardDetailsDownDuration, 0);
        yield return null;
    }
    private IEnumerator HitSequence(Card card, float delay, Vector3 shakeIntensity, float shakeDuration, float shakeDelay)
    {
        yield return new WaitForSeconds(delay);
        card.transform.GetChild(3).gameObject.SetActive(true);
        Tween.Shake(card.transform, card.transform.position, shakeIntensity, shakeDuration, shakeDelay, Tween.LoopType.None);
        audioManager.PlayCardAttackedSFX();
        yield return new WaitForSeconds(shakeDuration);
    }  
    private IEnumerator HitSequenceForNoWinner(Card card1, Card card2, float delayToBothCardsHits, Vector3 shakeIntensity, float shakeDuration, float shakeDelay)
    {
        yield return new WaitForSeconds(delayToBothCardsHits);
        card1.transform.GetChild(3).gameObject.SetActive(true);
        Tween.Shake(card1.transform, card1.transform.position, shakeIntensity, shakeDuration, shakeDelay);
        card2.transform.GetChild(3).gameObject.SetActive(true);
        Tween.Shake(card2.transform, card2.transform.position, shakeIntensity, shakeDuration, shakeDelay);
        audioManager.PlayCardAttackedSFX();
        yield return new WaitForSeconds(shakeDuration);
    }    
    private IEnumerator StartCardFallingSequence(Card card)
    {       
        yield return RotateAllCardDetailsBackDown(card);       
        audioManager.PlaycardPoofSFX();        
        yield return Delay(rotateCardDetailsDownDuration);        
        card.transform.GetChild(4).gameObject.SetActive(true);
        card.transform.GetChild(5).gameObject.SetActive(true);
        card.transform.GetChild(6).gameObject.SetActive(true);        
        card.transform.GetChild(1).gameObject.SetActive(false);
        card.transform.GetChild(2).gameObject.SetActive(false);
        card.transform.GetChild(7).gameObject.SetActive(false);
        yield return Delay(rotateCardDetailsDuration);
        card.gameObject.AddComponent<BoxCollider>().size = new Vector3(2, 3.5f, 0.05f);
        card.gameObject.AddComponent<Rigidbody>();        
        card.GetComponent<Rigidbody>().AddForce(NewRandomNumberForce(), 0, NewRandomNumberForce(), ForceMode.Impulse);
        card.GetComponent<Rigidbody>().AddTorque(NewRandomNumberTorque(), 0, NewRandomNumberTorque(), ForceMode.Impulse);
        yield return null; 
    }    
    public IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
    }    
    private IEnumerator DelayBetweenBattle(float delay)
    {
        yield return new WaitForSeconds(delay);        
    }
    private IEnumerator DelayBetweenDroppingCards(float delay)
    {
        yield return new WaitForSeconds(delay);
    }
    private int NewRandomNumberForce()
    {
        randomNumber = Random.Range(-1, 4);
        if (randomNumber == lastNumber)
        {
            randomNumber = Random.Range(-1, 4);
        }
        lastNumber = randomNumber;

        return randomNumber;
    }
    private int NewRandomNumberTorque()
    {
        randomNumber = Random.Range(150, 500);
        if (randomNumber == lastNumber)
        {
            randomNumber = Random.Range(150, 500);
        }
        lastNumber = randomNumber;

        return randomNumber;
    }    
}
