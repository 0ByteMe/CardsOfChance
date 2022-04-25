using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDecks : MonoBehaviour
{
    [Header("Initial Card Decks")]
    public List<GameObject> playerCards;
    public List<GameObject> enemyCards;
    [Header("Shuffled Card Decks")]
    public List<GameObject> shuffledPlayerCards;
    public List<GameObject> shuffledEnemyCards;
    [Header("Current Battle Cards")]
    public List<GameObject> playerBattleCards;
    public List<GameObject> enemyBattleCards;

    public void AddToCurrentBattleCards(List<GameObject> listToAddTo, GameObject card1, GameObject card2, GameObject card3)
    {
        listToAddTo.Add(card1);
        listToAddTo.Add(card2);
        listToAddTo.Add(card3);
    }

    public IEnumerator RemoveDrawnPlayerCards()
    {
        yield return new WaitForSeconds(.6f);

        shuffledPlayerCards.RemoveAt(0);
        shuffledPlayerCards.RemoveAt(0);
        shuffledPlayerCards.RemoveAt(0);
    }
    public IEnumerator RemoveDrawnEnemyCards()
    {
        yield return new WaitForSeconds(.6f);

        shuffledEnemyCards.RemoveAt(0);
        shuffledEnemyCards.RemoveAt(0);
        shuffledEnemyCards.RemoveAt(0);
    }
}
