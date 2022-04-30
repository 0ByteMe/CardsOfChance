using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class CardBattler : MonoBehaviour
{
    [TextArea]
    [SerializeField]
    private string description;
    
    [Header("Timing ")]
    [Tooltip("Delay before battle starts after final card is placed.")] 
    [SerializeField] [Range(0.1f, 3f)] float delayBeforeFirstBattleStarts;
    [Tooltip("Delay between battles.")] 
    [SerializeField] [Range(0.1f, 3f)] float delayBetweenEachBattle;
    [Tooltip("Delay before rotating the entire card.")] 
    [SerializeField] [Range(0.1f, 3f)] float delayToRotatingCard;
    [Tooltip("Delay before rotating Sprite + Number + Name.")] 
    [SerializeField] [Range(0.01f, 3f)] float delayToRotatingCardDetails;
    [Tooltip("Delay before playing hit animation + VFX.")] 
    [SerializeField] [Range(0.1f, 3f)] float delayToTakingHit;
    [Tooltip("Delay before both cards taking a hit if its a draw.")] 
    [SerializeField] [Range(0.1f, 3f)] float delayToBothCardsHits;

    [Header("Hit Animation")]
    [SerializeField] Vector3 shakeIntensity;
    [SerializeField] [Range(0.1f, 2f)] float shakeDuration;
    [SerializeField] [Range(0.1f, 2f)] float shakeDelay;

    private int randomNumber;
    private int lastNumber;

    CardDecks cardDecks;
    ScoreManager scoreManager;
    GameManager gameManager;

    private void Awake()
    {
        cardDecks = GetComponent<CardDecks>();
        scoreManager = GetComponent<ScoreManager>();        
        gameManager = GetComponent<GameManager>();
    }   
    public IEnumerator CardBattle()
    {
       yield return new WaitForSeconds(delayBeforeFirstBattleStarts);
       yield return BattleFirstCards();        
       yield return BattleSecondCards();        
       yield return BattleThirdCards();
       yield return cardDecks.RemoveCardsFromDeck(cardDecks.playerBattleCards);
       yield return cardDecks.RemoveCardsFromDeck(cardDecks.enemyBattleCards);
       yield return gameManager.AllowDrawingOfCards(gameManager.delayToAllowDraw);       
    }

    private IEnumerator BattleFirstCards()
    {        
        yield return RotateCards(cardDecks.playerBattleCards[0], cardDecks.enemyBattleCards[0], delayToRotatingCard);
        
        yield return DelayThenRotateAllCardDetails(cardDecks.playerBattleCards[0], cardDecks.enemyBattleCards[0], delayToRotatingCardDetails);
            
        if (cardDecks.playerBattleCards[0].CardStrength > cardDecks.enemyBattleCards[0].CardStrength)
        {
            //PLAYER WINS POINT                       
            yield return HitSequence(cardDecks.enemyBattleCards[0], delayToTakingHit, shakeIntensity, shakeDuration, shakeDelay);
            StartCardFallingSequence(cardDecks.playerBattleCards[0]);
            StartCardFallingSequence(cardDecks.enemyBattleCards[0]);
            scoreManager.AddToPlayerScore();  
        }
        else if (cardDecks.playerBattleCards[0].CardStrength < cardDecks.enemyBattleCards[0].CardStrength)
        {
            //ENEMY WINS POINT            
            yield return HitSequence(cardDecks.playerBattleCards[0], delayToTakingHit, shakeIntensity, shakeDuration, shakeDelay);
            StartCardFallingSequence(cardDecks.playerBattleCards[0]);  
            StartCardFallingSequence(cardDecks.enemyBattleCards[0]);
            scoreManager.AddToEnemyScore();
        }
        else
        {
            //Nobody Wins Point
            yield return HitSequenceForNoWinner(cardDecks.playerBattleCards[0], cardDecks.enemyBattleCards[0], delayToBothCardsHits, shakeIntensity, shakeDuration, shakeDelay);
            StartCardFallingSequence(cardDecks.playerBattleCards[0]);
            StartCardFallingSequence(cardDecks.enemyBattleCards[0]);            
        }
    }

    private IEnumerator BattleSecondCards()
    {
        yield return DelayBetweenBattle(delayBetweenEachBattle);

        yield return RotateCards(cardDecks.playerBattleCards[1], cardDecks.enemyBattleCards[1], delayToRotatingCard);
        
        yield return DelayThenRotateAllCardDetails(cardDecks.playerBattleCards[1], cardDecks.enemyBattleCards[1], delayToRotatingCardDetails);
             
        if (cardDecks.playerBattleCards[1].CardStrength > cardDecks.enemyBattleCards[1].CardStrength)
        {
            //Player wins point
            yield return StartCoroutine(HitSequence(cardDecks.enemyBattleCards[1], delayToTakingHit, shakeIntensity, shakeDuration, shakeDelay));
            StartCardFallingSequence(cardDecks.playerBattleCards[1]);
            StartCardFallingSequence(cardDecks.enemyBattleCards[1]);
            scoreManager.AddToPlayerScore();
        }
        else if (cardDecks.playerBattleCards[1].CardStrength < cardDecks.enemyBattleCards[1].CardStrength)
        {
            //Enemy wins point
            yield return StartCoroutine(HitSequence(cardDecks.playerBattleCards[1], delayToTakingHit, shakeIntensity, shakeDuration, shakeDelay));
            StartCardFallingSequence(cardDecks.playerBattleCards[1]);
            StartCardFallingSequence(cardDecks.enemyBattleCards[1]);
            scoreManager.AddToEnemyScore();
        }
        else
        {
            //Nobody Wins Point
            yield return StartCoroutine(HitSequenceForNoWinner(cardDecks.playerBattleCards[1], cardDecks.enemyBattleCards[1], delayToBothCardsHits, shakeIntensity, shakeDuration, shakeDelay));
            StartCardFallingSequence(cardDecks.playerBattleCards[1]);
            StartCardFallingSequence(cardDecks.enemyBattleCards[1]);
        }
    }

    private IEnumerator BattleThirdCards()
    {
        yield return DelayBetweenBattle(delayBetweenEachBattle);

        yield return RotateCards(cardDecks.playerBattleCards[2], cardDecks.enemyBattleCards[2], delayToRotatingCard);
        
        yield return DelayThenRotateAllCardDetails(cardDecks.playerBattleCards[2], cardDecks.enemyBattleCards[2], delayToRotatingCardDetails);
            
        if (cardDecks.playerBattleCards[2].CardStrength > cardDecks.enemyBattleCards[2].CardStrength)
        {
            //Player wins point
            yield return StartCoroutine(HitSequence(cardDecks.enemyBattleCards[2], delayToTakingHit, shakeIntensity, shakeDuration, shakeDelay));
            StartCardFallingSequence(cardDecks.playerBattleCards[2]);
            StartCardFallingSequence(cardDecks.enemyBattleCards[2]);
            scoreManager.AddToPlayerScore();
        }
        else if (cardDecks.playerBattleCards[2].CardStrength < cardDecks.enemyBattleCards[2].CardStrength)
        {
            //Enemy wins point
            yield return StartCoroutine(HitSequence(cardDecks.playerBattleCards[2], delayToTakingHit, shakeIntensity, shakeDuration, shakeDelay));
            StartCardFallingSequence(cardDecks.playerBattleCards[2]);
            StartCardFallingSequence(cardDecks.enemyBattleCards[2]);
            scoreManager.AddToEnemyScore();
        }
        else
        {
            //Nobody Wins Point
            yield return StartCoroutine(HitSequenceForNoWinner(cardDecks.playerBattleCards[2], cardDecks.enemyBattleCards[2], delayToBothCardsHits, shakeIntensity, shakeDuration, shakeDelay));
            StartCardFallingSequence(cardDecks.playerBattleCards[2]);
            StartCardFallingSequence(cardDecks.enemyBattleCards[2]);
        }
    }

    private IEnumerator RotateCards(Card card1, Card card2, float delay)
    {
        yield return new WaitForSeconds(delay);
        card1.GetComponent<RotateCard>().enabled = true;
        card2.GetComponent<RotateCard>().enabled = true;
    }

    private IEnumerator DelayThenRotateAllCardDetails(Card card1, Card card2, float delayBeforeRotating)
    {
        yield return new WaitForSeconds(delayBeforeRotating);
        card1.GetComponent<RotateCardDetails>().enabled = true;
        card2.GetComponent<RotateCardDetails>().enabled = true;
    }
    
    private IEnumerator HitSequence(Card card, float delay, Vector3 shakeIntensity, float shakeDuration, float shakeDelay)
    {
        yield return new WaitForSeconds(delay);
        card.transform.GetChild(4).gameObject.SetActive(true);
        Tween.Shake(card.transform, card.transform.position, shakeIntensity, shakeDuration, shakeDelay, Tween.LoopType.None, null);        
    }

    void HandleTweenFinished(Card card)
    {
        card.gameObject.AddComponent<Rigidbody>();
        card.GetComponent<Rigidbody>().AddTorque(NewRandomNumber(), NewRandomNumber(), NewRandomNumber(), ForceMode.Impulse);
    }

    private IEnumerator HitSequenceForNoWinner(Card card1, Card card2, float delayToBothCardsHits, Vector3 shakeIntensity, float shakeDuration, float shakeDelay)
    {
        yield return new WaitForSeconds(delayToBothCardsHits);
        card1.transform.GetChild(4).gameObject.SetActive(true);
        Tween.Shake(card1.transform, card1.transform.position, shakeIntensity, shakeDuration, shakeDelay);
        card2.transform.GetChild(4).gameObject.SetActive(true);
        Tween.Shake(card2.transform, card2.transform.position, shakeIntensity, shakeDuration, shakeDelay);
        yield return new WaitForSeconds(shakeDuration);
    }
    
    private IEnumerator StartCardFallingSequence(Card card, float delay)
    {
        yield return new WaitForSeconds(delay);
        card.gameObject.AddComponent<Rigidbody>();
        card.GetComponent<Rigidbody>().AddTorque(NewRandomNumber(), NewRandomNumber(), NewRandomNumber(), ForceMode.Impulse);
    }
    private void StartCardFallingSequence(Card card)
    {
        card.gameObject.AddComponent<Rigidbody>();
        card.GetComponent<Rigidbody>().AddTorque(NewRandomNumber(), NewRandomNumber(), NewRandomNumber(), ForceMode.Impulse);        
    }
    private IEnumerator DelayBetweenBattle(float seconds)
    {
        yield return new WaitForSeconds(seconds);        
    }
    private int NewRandomNumber()
    {
        randomNumber = Random.Range(-50, 100);
        if (randomNumber == lastNumber)
        {
            randomNumber = Random.Range(-50, 100);
        }
        lastNumber = randomNumber;

        return randomNumber;
    }


}
