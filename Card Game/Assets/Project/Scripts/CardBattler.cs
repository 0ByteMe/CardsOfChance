using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class CardBattler : MonoBehaviour
{
    [Header("Timing ")]
    [SerializeField] float delayBeforeBattle;
    [SerializeField] float delayBeforeHitAnimation;
    [SerializeField] public float delayToAllowDrawingOfCards;

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
       yield return BattleFirstCards(delayBeforeBattle);        
       yield return BattleSecondCards(delayBeforeBattle);        
       yield return BattleThirdCards(delayBeforeBattle);
       StartCoroutine(cardDecks.RemoveCardsFromDeck(cardDecks.playerBattleCards));
       StartCoroutine(cardDecks.RemoveCardsFromDeck(cardDecks.enemyBattleCards));
       StartCoroutine(AllowDrawingOfCards(delayToAllowDrawingOfCards));
       yield break;
    }

    private IEnumerator BattleFirstCards(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        //Rotate Cards in battle
        RotateCards(cardDecks.playerBattleCards[0], cardDecks.enemyBattleCards[0]);

        //Pause then rotate All Card Texts for 'Pop-up' Effect
        yield return DelayThenRotateAllCardDetails(cardDecks.playerBattleCards[0], cardDecks.enemyBattleCards[0]);

        //Logic to determine which card wins a point
        if (cardDecks.playerBattleCards[0].CardStrength == cardDecks.enemyBattleCards[0].CardStrength)
        {
            //TODO add rigidbody plus some torque?
            StartCoroutine(DelayThenPlayHitAnimation(cardDecks.playerBattleCards[0], delayBeforeHitAnimation));
            StartCoroutine(DelayThenPlayHitAnimation(cardDecks.enemyBattleCards[0], delayBeforeHitAnimation));
            Tween.Shake(cardDecks.playerBattleCards[0].transform, cardDecks.playerBattleCards[0].transform.position, shakeIntensity, shakeDuration, shakeDelay);
            Tween.Shake(cardDecks.enemyBattleCards[0].transform, cardDecks.enemyBattleCards[0].transform.position, shakeIntensity, shakeDuration, shakeDelay);
            yield break;
        }
        else if (cardDecks.playerBattleCards[0].CardStrength > cardDecks.enemyBattleCards[0].CardStrength)
        {
            //PLAYER WINS POINT                       
            //TODO add rigidbody to enemy plus torque
            yield return DelayThenPlayHitAnimation(cardDecks.enemyBattleCards[0], delayBeforeHitAnimation);
            Tween.Shake(cardDecks.enemyBattleCards[0].transform, cardDecks.enemyBattleCards[0].transform.position, shakeIntensity, shakeDuration, shakeDelay);            
            scoreManager.AddToPlayerScore();
        }
        else
        {
            //ENEMY WINS POINT            
            //TODO add rigidbody to player plus torque
            yield return DelayThenPlayHitAnimation(cardDecks.playerBattleCards[0], delayBeforeHitAnimation);
            Tween.Shake(cardDecks.playerBattleCards[0].transform, cardDecks.playerBattleCards[0].transform.position, shakeIntensity, shakeDuration, shakeDelay);                     
            scoreManager.AddToEnemyScore();
        }        
    }

    private IEnumerator BattleSecondCards(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        
        RotateCards(cardDecks.playerBattleCards[1], cardDecks.enemyBattleCards[1]);
        
        yield return DelayThenRotateAllCardDetails(cardDecks.playerBattleCards[1], cardDecks.enemyBattleCards[1]);

        //Player and Enemy DRAW
        if (cardDecks.playerBattleCards[1].CardStrength == cardDecks.enemyBattleCards[1].CardStrength)
        {
            StartCoroutine(DelayThenPlayHitAnimation(cardDecks.playerBattleCards[1], delayBeforeHitAnimation));
            StartCoroutine(DelayThenPlayHitAnimation(cardDecks.enemyBattleCards[1], delayBeforeHitAnimation));
            Tween.Shake(cardDecks.playerBattleCards[1].transform, cardDecks.playerBattleCards[1].transform.position, shakeIntensity, shakeDuration, shakeDelay);
            Tween.Shake(cardDecks.enemyBattleCards[1].transform, cardDecks.enemyBattleCards[1].transform.position, shakeIntensity, shakeDuration, shakeDelay);
            yield break;
        }
        if (cardDecks.playerBattleCards[1].CardStrength > cardDecks.enemyBattleCards[1].CardStrength)
        {
            //Player wins point
            yield return DelayThenPlayHitAnimation(cardDecks.enemyBattleCards[1], delayBeforeHitAnimation);
            Tween.Shake(cardDecks.enemyBattleCards[1].transform, cardDecks.enemyBattleCards[1].transform.position, shakeIntensity, shakeDuration, shakeDelay);
            scoreManager.AddToPlayerScore();
        }
        else
        {
            //Enemy wins point
            yield return DelayThenPlayHitAnimation(cardDecks.playerBattleCards[1], delayBeforeHitAnimation);
            Tween.Shake(cardDecks.playerBattleCards[1].transform, cardDecks.playerBattleCards[1].transform.position, shakeIntensity, shakeDuration, shakeDelay);
            scoreManager.AddToEnemyScore();
        }
    }

    private IEnumerator BattleThirdCards(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        RotateCards(cardDecks.playerBattleCards[2], cardDecks.enemyBattleCards[2]);
        
        yield return DelayThenRotateAllCardDetails(cardDecks.playerBattleCards[2], cardDecks.enemyBattleCards[2]);

            //Player and Enemy DRAW
        if (cardDecks.playerBattleCards[2].CardStrength == cardDecks.enemyBattleCards[2].CardStrength)
        {
            StartCoroutine(DelayThenPlayHitAnimation(cardDecks.playerBattleCards[2], delayBeforeHitAnimation));
            StartCoroutine(DelayThenPlayHitAnimation(cardDecks.enemyBattleCards[2], delayBeforeHitAnimation));
            Tween.Shake(cardDecks.playerBattleCards[2].transform, cardDecks.playerBattleCards[2].transform.position, shakeIntensity, shakeDuration, shakeDelay);
            Tween.Shake(cardDecks.enemyBattleCards[2].transform, cardDecks.enemyBattleCards[2].transform.position, shakeIntensity, shakeDuration, shakeDelay);
            yield break;
        }
        if (cardDecks.playerBattleCards[2].CardStrength > cardDecks.enemyBattleCards[2].CardStrength)
        {
            //Player wins point
            yield return DelayThenPlayHitAnimation(cardDecks.enemyBattleCards[2], delayBeforeHitAnimation);
            Tween.Shake(cardDecks.enemyBattleCards[2].transform, cardDecks.enemyBattleCards[2].transform.position, shakeIntensity, shakeDuration, shakeDelay);
            scoreManager.AddToPlayerScore();
        }
        else
        {
            //Enemy wins point
            yield return DelayThenPlayHitAnimation(cardDecks.playerBattleCards[2], delayBeforeHitAnimation);
            Tween.Shake(cardDecks.playerBattleCards[2].transform, cardDecks.playerBattleCards[2].transform.position, shakeIntensity, shakeDuration, shakeDelay);
            scoreManager.AddToEnemyScore();
        }        
    }

    private void RotateCards(Card card1, Card card2)
    {
        card1.GetComponent<RotateCard>().enabled = true;
        card2.GetComponent<RotateCard>().enabled = true;
    }

    private IEnumerator DelayThenRotateAllCardDetails(Card card1, Card card2)
    {
        yield return new WaitForSeconds(1f);
        card1.GetComponent<RotateCardDetails>().enabled = true;
        card2.GetComponent<RotateCardDetails>().enabled = true;
    }
    
    private IEnumerator DelayThenPlayHitAnimation(Card card, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        card.transform.GetChild(4).gameObject.SetActive(true);
    }

    public IEnumerator AllowDrawingOfCards(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        gameManager.canDrawCards = true;
        gameManager.EnableDrawButton();
    }
}
