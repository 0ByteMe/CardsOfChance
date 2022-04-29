using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDecks : MonoBehaviour
{
    [Header("Initial Card Decks")]
    public List<Card> playerCards;
    public List<Card> enemyCards;
    [Header("Shuffled Card Decks")]
    public List<Card> shuffledPlayerCards;
    public List<Card> shuffledEnemyCards;
    [Header("Current Battle Cards")]
    public List<Card> playerBattleCards;
    public List<Card> enemyBattleCards;     

    public void AddToCurrentBattleCards(List<Card> listToAddTo, Card card1, Card card2, Card card3)
    {
        listToAddTo.Add(card1);
        listToAddTo.Add(card2);
        listToAddTo.Add(card3);
    }

    public IEnumerator RemoveCardsFromDeck(List<Card> deckOfCards)
    {
        //TODO think about making a delay variable in GameManager and feeding it into here so that you can 
        //never get cards too fast
        yield return new WaitForSeconds(2f);

        deckOfCards.RemoveAt(0);
        deckOfCards.RemoveAt(0);
        deckOfCards.RemoveAt(0);
    }  
}
