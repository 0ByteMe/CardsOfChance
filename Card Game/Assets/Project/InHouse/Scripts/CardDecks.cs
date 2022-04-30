using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CardDecks : MonoBehaviour
{
    [Header("Initial Card Decks")]
    public List<Card> playerCards;
    public List<Card> enemyCards;
    [Header("Shuffled Card Decks")] [Space(10)]
    public List<Card> shuffledPlayerCards;
    public List<Card> shuffledEnemyCards;
    [Header("Battle Cards")] [Space(10)]
    public List<Card> playerBattleCards;
    public List<Card> enemyBattleCards;

   public void ShuffleCardDecks()
    {
        shuffledPlayerCards = new List<Card>();
        shuffledPlayerCards = playerCards.OrderBy(x => Random.value).ToList();

        shuffledEnemyCards = new List<Card>();
        shuffledEnemyCards = enemyCards.OrderBy(x => Random.value).ToList();
    }
   public IEnumerator AddToCurrentBattleCards(List<Card> listToAddTo, Card card1, Card card2, Card card3, float delay)
    {
        listToAddTo.Add(card1);
        listToAddTo.Add(card2);
        listToAddTo.Add(card3);

        yield return new WaitForSeconds(delay);
    }
   public IEnumerator RemoveCardsFromDeck(List<Card> deckOfCards)
    {
        deckOfCards.RemoveAt(0);
        deckOfCards.RemoveAt(0);
        deckOfCards.RemoveAt(0);

        yield return null;
    }    
}
