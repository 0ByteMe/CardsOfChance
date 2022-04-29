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
    [Tooltip("This is used before first battle as well.")] [Range(0.1f, 3f)] 
    [SerializeField] float delayBetweenEachBattle;
    [Tooltip("Delay before rotating the entire card.")]
    [Range(0.1f, 3f)] [SerializeField] float delayToRotatingCard;
    [Tooltip("Delay before rotating Sprite + Number + Name.")] [Range(0.1f, 3f)] 
    [SerializeField] float delayToRotatingCardDetails;
    [Tooltip("Delay before playing hit animation + VFX.")] [Range(0.1f, 3f)] 
    [SerializeField] float delayToTakingHit;
    [Tooltip("Delay before both cards taking a hit if its a draw.")] [Range(0.1f, 3f)] 
    [SerializeField] float delayToBothCardsHits;
    [Tooltip("Delay before the player can click Draw after all battles are completed.")] [Range(0.1f, 3f)]
    [SerializeField] public float delayToAllowDraw;    

    [Header("Hit Animation Shake")]
    [SerializeField] Vector3 shakeIntensity;
    [SerializeField] float shakeDuration;
    [SerializeField] float shakeDelay;

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
       yield return BattleFirstCards();        
       yield return BattleSecondCards();        
       yield return BattleThirdCards();
       StartCoroutine(cardDecks.RemoveCardsFromDeck(cardDecks.playerBattleCards));
       StartCoroutine(cardDecks.RemoveCardsFromDeck(cardDecks.enemyBattleCards));
       StartCoroutine(AllowDrawingOfCards(delayToAllowDraw));
       yield break;
    }

    private IEnumerator BattleFirstCards()
    {
        yield return DelayBetweenBattle(delayBetweenEachBattle);

        //Rotate Cards in battle
        RotateCards(cardDecks.playerBattleCards[0], cardDecks.enemyBattleCards[0]);

        //Pause then rotate All Card Texts for 'Pop-up' Effect
        yield return DelayThenRotateAllCardDetails(cardDecks.playerBattleCards[0], cardDecks.enemyBattleCards[0], delayToRotatingCardDetails);

            //Nobody Wins Point
        if (cardDecks.playerBattleCards[0].CardStrength == cardDecks.enemyBattleCards[0].CardStrength)
        {
            //TODO add rigidbody plus some torque?
            yield return HitSequenceForNoWinner(cardDecks.playerBattleCards[0], cardDecks.enemyBattleCards[0], delayToBothCardsHits, shakeIntensity, shakeDuration, shakeDelay);
            yield return DelayBetweenBattle(delayBetweenEachBattle);
            yield break;
        }
        else if (cardDecks.playerBattleCards[0].CardStrength > cardDecks.enemyBattleCards[0].CardStrength)
        {
            //PLAYER WINS POINT                       
            //TODO add rigidbody to enemy plus torque
            yield return HitSequence(cardDecks.enemyBattleCards[0], delayToTakingHit, shakeIntensity, shakeDuration, shakeDelay);
            scoreManager.AddToPlayerScore();
        }
        else
        {
            //ENEMY WINS POINT            
            //TODO add rigidbody to player plus torque
            yield return HitSequence(cardDecks.playerBattleCards[0], delayToTakingHit, shakeIntensity, shakeDuration, shakeDelay);
            scoreManager.AddToEnemyScore();
        }
        yield return DelayBetweenBattle(delayBetweenEachBattle);
    }

    private IEnumerator BattleSecondCards()
    {        
        RotateCards(cardDecks.playerBattleCards[1], cardDecks.enemyBattleCards[1]);
        
        yield return DelayThenRotateAllCardDetails(cardDecks.playerBattleCards[1], cardDecks.enemyBattleCards[1], delayToRotatingCardDetails);

        //Player and Enemy DRAW
        if (cardDecks.playerBattleCards[1].CardStrength == cardDecks.enemyBattleCards[1].CardStrength)
        {
            yield return HitSequenceForNoWinner(cardDecks.playerBattleCards[1], cardDecks.enemyBattleCards[1], delayToBothCardsHits, shakeIntensity, shakeDuration, shakeDelay);
            yield return DelayBetweenBattle(delayBetweenEachBattle);
            yield break;
        }
        if (cardDecks.playerBattleCards[1].CardStrength > cardDecks.enemyBattleCards[1].CardStrength)
        {
            //Player wins point
            yield return HitSequence(cardDecks.enemyBattleCards[1], delayToTakingHit, shakeIntensity, shakeDuration, shakeDelay);
            scoreManager.AddToPlayerScore();
        }
        else
        {
            //Enemy wins point
            yield return HitSequence(cardDecks.playerBattleCards[1], delayToTakingHit, shakeIntensity, shakeDuration, shakeDelay);
            scoreManager.AddToEnemyScore();
        }
        yield return DelayBetweenBattle(delayBetweenEachBattle);

    }

    private IEnumerator BattleThirdCards()
    {
        RotateCards(cardDecks.playerBattleCards[2], cardDecks.enemyBattleCards[2]);
        
        yield return DelayThenRotateAllCardDetails(cardDecks.playerBattleCards[2], cardDecks.enemyBattleCards[2], delayToRotatingCardDetails);

            //Player and Enemy DRAW
        if (cardDecks.playerBattleCards[2].CardStrength == cardDecks.enemyBattleCards[2].CardStrength)
        {
            yield return HitSequenceForNoWinner(cardDecks.playerBattleCards[2], cardDecks.enemyBattleCards[2], delayToBothCardsHits, shakeIntensity, shakeDuration, shakeDelay);
            yield break;
        }
        if (cardDecks.playerBattleCards[2].CardStrength > cardDecks.enemyBattleCards[2].CardStrength)
        {
            //Player wins point
            yield return HitSequence(cardDecks.enemyBattleCards[2], delayToTakingHit, shakeIntensity, shakeDuration, shakeDelay);
            scoreManager.AddToPlayerScore();
        }
        else
        {
            //Enemy wins point
            yield return HitSequence(cardDecks.playerBattleCards[2], delayToTakingHit, shakeIntensity, shakeDuration, shakeDelay);
            scoreManager.AddToEnemyScore();
        }        
    }

    private void RotateCards(Card card1, Card card2)
    {
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
        Tween.Shake(card.transform, card.transform.position, shakeIntensity, shakeDuration, shakeDelay);
        
    }

    private IEnumerator HitSequenceForNoWinner(Card card1, Card card2, float delayToBothCardsHits, Vector3 shakeIntensity, float shakeDuration, float shakeDelay)
    {
        yield return new WaitForSeconds(delayToBothCardsHits);
        card1.transform.GetChild(4).gameObject.SetActive(true);
        Tween.Shake(card1.transform, card1.transform.position, shakeIntensity, shakeDuration, shakeDelay);
        card2.transform.GetChild(4).gameObject.SetActive(true);
        Tween.Shake(card2.transform, card2.transform.position, shakeIntensity, shakeDuration, shakeDelay);

    }

    private IEnumerator DelayBetweenBattle(float seconds)
    {
        yield return new WaitForSeconds(seconds);        
    }

    public IEnumerator AllowDrawingOfCards(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        gameManager.canDrawCards = true;
        gameManager.EnableDrawButton();
    } 
    
}
