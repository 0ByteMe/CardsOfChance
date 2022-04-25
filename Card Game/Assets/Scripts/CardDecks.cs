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

    public List<GameObject> playerBattleCards;
    public List<GameObject> enemyBattleCards;


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
