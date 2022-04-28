using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBattler : MonoBehaviour
{
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
       StartCoroutine(gameManager.AllowDrawAgain());
       yield break;
    }

    private IEnumerator BattleFirstCards()
    {
        yield return new WaitForSeconds(2f);

        //Rotate Cards
        cardDecks.playerBattleCards[0].GetComponent<RotateCard>().enabled = true;
        cardDecks.enemyBattleCards[0].GetComponent<RotateCard>().enabled = true;

        //Pause then rotate Card Text
        yield return PauseBeforeRotatingFirstBattleText();

        //TODO add shake to cardto simulate Damage

        //Logic to determine which card wins a point
        if (cardDecks.playerBattleCards[0].CardStrength == cardDecks.enemyBattleCards[0].CardStrength)
        {
            yield break;
        }
        else if (cardDecks.playerBattleCards[0].CardStrength > cardDecks.enemyBattleCards[0].CardStrength)
        {
            scoreManager.AddToPlayerScore();
        }
        else
        {
            scoreManager.AddToEnemyScore();
        }        
    }

    private IEnumerator BattleSecondCards()
    {
        yield return new WaitForSeconds(2f);

        //Rotate Card
        cardDecks.playerBattleCards[1].GetComponent<RotateCard>().enabled = true;
        cardDecks.enemyBattleCards[1].GetComponent<RotateCard>().enabled = true;

        //Pause then rotate Card Text
        yield return PauseBeforeRotatingSecondBattleText();

        //TODO add shake to cardto simulate Damage

        //Logic to determine which card wins a point
        if (cardDecks.playerBattleCards[1].CardStrength == cardDecks.enemyBattleCards[1].CardStrength)
        {
            yield break;
        }
        if (cardDecks.playerBattleCards[1].CardStrength > cardDecks.enemyBattleCards[1].CardStrength)
        {
            scoreManager.AddToPlayerScore();
        }
        else
        {
            scoreManager.AddToEnemyScore();
        }
    }

    private IEnumerator BattleThirdCards()
    {
        yield return new WaitForSeconds(2f);

        //Rotates cards
        cardDecks.playerBattleCards[2].GetComponent<RotateCard>().enabled = true;
        cardDecks.enemyBattleCards[2].GetComponent<RotateCard>().enabled = true;

        //Pause then rotate Card Text
        yield return PauseBeforeRotatingThirdBattleText();

        //Logic to determine which card wins a point
        if (cardDecks.playerBattleCards[2].CardStrength == cardDecks.enemyBattleCards[2].CardStrength)
        {
            yield break;
        }
        if (cardDecks.playerBattleCards[2].CardStrength > cardDecks.enemyBattleCards[2].CardStrength)
        {
            scoreManager.AddToPlayerScore();
        }
        else
        {
            scoreManager.AddToEnemyScore();
        }       

    }

    private IEnumerator PauseBeforeRotatingFirstBattleText()
    {
        yield return new WaitForSeconds(1f);
        cardDecks.playerBattleCards[0].GetComponent<RotateText>().enabled = true;
        cardDecks.enemyBattleCards[0].GetComponent<RotateText>().enabled = true;
    }
    private IEnumerator PauseBeforeRotatingSecondBattleText()
    {
        yield return new WaitForSeconds(1f);
        cardDecks.playerBattleCards[1].GetComponent<RotateText>().enabled = true;
        cardDecks.enemyBattleCards[1].GetComponent<RotateText>().enabled = true;
    }
    private IEnumerator PauseBeforeRotatingThirdBattleText()
    {
        yield return new WaitForSeconds(1f);
        cardDecks.playerBattleCards[2].GetComponent<RotateText>().enabled = true;
        cardDecks.enemyBattleCards[2].GetComponent<RotateText>().enabled = true;
    }

}
