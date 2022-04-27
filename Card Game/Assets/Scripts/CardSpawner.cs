using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class CardSpawner : MonoBehaviour
{
    [Header("Card Deck Prefabs")]
    [SerializeField] private List<Card> playerCardPrefabs;
    [SerializeField] private List<Card> enemyCardPrefabs;
    
    [Header("Deck Locations")]
    [SerializeField] public Transform playerDeckLocation;
    [SerializeField] public Transform enemyDeckLocation;

    CardDecks cardDecks;

    private void Awake()
    {
        cardDecks = GetComponentInParent<CardDecks>();
    }

    private void Start()
    {
        InstantiateCardDecks();
        ShuffleCardDecks();
    }
    
    private void InstantiateCardDecks()
    {
        foreach (Card prefab in playerCardPrefabs)
        {
           Card newPlayerCard = Instantiate(prefab, playerDeckLocation.position, Quaternion.Euler(-90, -90, -90));
           cardDecks.playerCards.Add(newPlayerCard);
        }

        foreach (Card prefab in enemyCardPrefabs)
        {
            Card newEnemyCard = Instantiate(prefab, enemyDeckLocation.position, Quaternion.Euler(-90, -90, -90));
            cardDecks.enemyCards.Add(newEnemyCard);
        }
    }

    private void ShuffleCardDecks()
    {
        cardDecks.shuffledPlayerCards = new List<Card>();
        cardDecks.shuffledPlayerCards = cardDecks.playerCards.OrderBy(x => Random.value).ToList();

        cardDecks.shuffledEnemyCards = new List<Card>();
        cardDecks.shuffledEnemyCards = cardDecks.enemyCards.OrderBy(x => Random.value).ToList();
    }
}
