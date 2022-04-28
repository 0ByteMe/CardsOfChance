using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class CardBattler : MonoBehaviour
{
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
       StartCoroutine(gameManager.AllowDrawingOfCards());
       yield break;
    }

    private IEnumerator BattleFirstCards()
    {
        yield return new WaitForSeconds(2f);

        //Rotate Cards in battle
        cardDecks.playerBattleCards[0].GetComponent<RotateCard>().enabled = true;
        cardDecks.enemyBattleCards[0].GetComponent<RotateCard>().enabled = true;

        //Pause then rotate All Card Texts for 'Pop-up' Effect
        yield return PauseThenPopUpFirstCardDetails();        

        //Logic to determine which card wins a point
        if (cardDecks.playerBattleCards[0].CardStrength == cardDecks.enemyBattleCards[0].CardStrength)
        {
            //TODO add rigidbody plus some torque?
            yield return DelayThenPlayHitAnimation(cardDecks.playerBattleCards[0]); 
            yield return DelayThenPlayHitAnimation(cardDecks.enemyBattleCards[0]);
            yield break;
        }
        else if (cardDecks.playerBattleCards[0].CardStrength > cardDecks.enemyBattleCards[0].CardStrength)
        {
            //PLAYER WINS POINT                       
            //TODO add rigidbody to enemy plus torque
            yield return DelayThenPlayHitAnimation(cardDecks.enemyBattleCards[0]);
            Tween.Shake(cardDecks.enemyBattleCards[0].transform, cardDecks.enemyBattleCards[0].transform.position, shakeIntensity, shakeDuration, shakeDelay);            
            scoreManager.AddToPlayerScore();
        }
        else
        {
            //ENEMY WINS POINT            
            //TODO add rigidbody to player plus torque
            yield return DelayThenPlayHitAnimation(cardDecks.playerBattleCards[0]);
            Tween.Shake(cardDecks.playerBattleCards[0].transform, cardDecks.playerBattleCards[0].transform.position, shakeIntensity, shakeDuration, shakeDelay);                     
            scoreManager.AddToEnemyScore();
        }        
    }

    private IEnumerator BattleSecondCards()
    {
        yield return new WaitForSeconds(2f);

        //Rotate Cards in battle
        cardDecks.playerBattleCards[1].GetComponent<RotateCard>().enabled = true;
        cardDecks.enemyBattleCards[1].GetComponent<RotateCard>().enabled = true;

        //Pause then rotate All Card Texts for 'Pop-up' Effect
        yield return PauseThenPopUpSecondCardDetails();        

        //If Player and Enemy Draw
        if (cardDecks.playerBattleCards[1].CardStrength == cardDecks.enemyBattleCards[1].CardStrength)
        {
            yield return DelayThenPlayHitAnimation(cardDecks.playerBattleCards[1]);
            yield return DelayThenPlayHitAnimation(cardDecks.enemyBattleCards[1]);
            yield break;
        }
        if (cardDecks.playerBattleCards[1].CardStrength > cardDecks.enemyBattleCards[1].CardStrength)
        {
            //player wins point
            yield return DelayThenPlayHitAnimation(cardDecks.enemyBattleCards[1]);
            Tween.Shake(cardDecks.enemyBattleCards[1].transform, cardDecks.enemyBattleCards[1].transform.position, shakeIntensity, shakeDuration, shakeDelay);
            scoreManager.AddToPlayerScore();
        }
        else
        {
            //enemy wins point
            yield return DelayThenPlayHitAnimation(cardDecks.playerBattleCards[1]);
            Tween.Shake(cardDecks.playerBattleCards[1].transform, cardDecks.playerBattleCards[1].transform.position, shakeIntensity, shakeDuration, shakeDelay);
            scoreManager.AddToEnemyScore();
        }
    }

    private IEnumerator BattleThirdCards()
    {
        yield return new WaitForSeconds(2f);

        //Rotates cards in Battle
        cardDecks.playerBattleCards[2].GetComponent<RotateCard>().enabled = true;
        cardDecks.enemyBattleCards[2].GetComponent<RotateCard>().enabled = true;

        //Pause then rotate All Card Texts for 'Pop-up' Effect
        yield return PauseThenPopUpThirdCardDetails();

        //If Player and Enemy Draw
        if (cardDecks.playerBattleCards[2].CardStrength == cardDecks.enemyBattleCards[2].CardStrength)
        {
            StartCoroutine(DelayThenPlayHitAnimation(cardDecks.playerBattleCards[2]));
            StartCoroutine(DelayThenPlayHitAnimation(cardDecks.enemyBattleCards[2]));
            yield break;
        }
        if (cardDecks.playerBattleCards[2].CardStrength > cardDecks.enemyBattleCards[2].CardStrength)
        {
            //player wins point
            yield return DelayThenPlayHitAnimation(cardDecks.enemyBattleCards[2]);
            Tween.Shake(cardDecks.enemyBattleCards[2].transform, cardDecks.enemyBattleCards[2].transform.position, shakeIntensity, shakeDuration, shakeDelay);
            scoreManager.AddToPlayerScore();
        }
        else
        {
            //enemy wins point
            yield return DelayThenPlayHitAnimation(cardDecks.playerBattleCards[2]);
            Tween.Shake(cardDecks.playerBattleCards[2].transform, cardDecks.playerBattleCards[2].transform.position, shakeIntensity, shakeDuration, shakeDelay);
            scoreManager.AddToEnemyScore();
        }

        StartCoroutine(gameManager.AllowDrawingOfCards());

    }

    private IEnumerator PauseThenPopUpFirstCardDetails()
    {
        yield return new WaitForSeconds(1f);
        cardDecks.playerBattleCards[0].GetComponent<RotateCardDetails>().enabled = true;
        cardDecks.enemyBattleCards[0].GetComponent<RotateCardDetails>().enabled = true;
    }
    private IEnumerator PauseThenPopUpSecondCardDetails()
    {
        yield return new WaitForSeconds(1f);
        cardDecks.playerBattleCards[1].GetComponent<RotateCardDetails>().enabled = true;
        cardDecks.enemyBattleCards[1].GetComponent<RotateCardDetails>().enabled = true;
    }
    private IEnumerator PauseThenPopUpThirdCardDetails()
    {
        yield return new WaitForSeconds(1f);
        cardDecks.playerBattleCards[2].GetComponent<RotateCardDetails>().enabled = true;
        cardDecks.enemyBattleCards[2].GetComponent<RotateCardDetails>().enabled = true;
    } 
    private IEnumerator DelayThenPlayHitAnimation(Card card)
    {
        yield return new WaitForSeconds(1.2f);
        card.transform.GetChild(4).gameObject.SetActive(true);
    }
}
