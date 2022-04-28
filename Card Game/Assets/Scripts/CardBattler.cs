using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBattler : MonoBehaviour
{
    CardDecks cardDecks;
    ScoreManager scoreManager;

    private void Awake()
    {
        cardDecks = GetComponent<CardDecks>();
        scoreManager = GetComponent<ScoreManager>();
    }

    public void CardBattle()
    {
        StartCoroutine(BattleFirstCards());

        cardDecks.RemoveCardsFromDeck(cardDecks.playerBattleCards);
        cardDecks.RemoveCardsFromDeck(cardDecks.enemyBattleCards);
    }

    private IEnumerator BattleFirstCards()
    {
        yield return new WaitForSeconds(1f);

        if (cardDecks.playerBattleCards[1].CardStrength > cardDecks.enemyBattleCards[1].CardStrength)
        {
            scoreManager.AddToPlayerScore();
        }
        else
            scoreManager.AddToEnemyScore();

        StartCoroutine(BattleSecondCards());
    }

    private IEnumerator BattleSecondCards()
    {
        yield return new WaitForSeconds(1f);

        if (cardDecks.playerBattleCards[0].CardStrength > cardDecks.enemyBattleCards[0].CardStrength)
        {
            scoreManager.AddToPlayerScore();
        }
        else
            scoreManager.AddToEnemyScore();

        StartCoroutine(BattleThirdCards());

    }

    private IEnumerator BattleThirdCards()
    {
        yield return new WaitForSeconds(1f);

        if (cardDecks.playerBattleCards[2].CardStrength > cardDecks.enemyBattleCards[2].CardStrength)
        {
            scoreManager.AddToPlayerScore();
        }
        else
            scoreManager.AddToEnemyScore();
    }

}
