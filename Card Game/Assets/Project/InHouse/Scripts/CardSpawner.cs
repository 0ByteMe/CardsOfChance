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
        //Creates all the cards and places them in Scene
        InstantiateCardDecks();
        //proceeds to shuffle them into a new shuffled deck
        cardDecks.ShuffleCardDecks();
    }    
    public void InstantiateCardDecks()
    {
        foreach (Card prefab in playerCardPrefabs)
        {
           Card newPlayerCard = Instantiate(prefab, playerDeckLocation.position, prefab.transform.rotation);
           newPlayerCard.isPlayer = true;
           cardDecks.playerCards.Add(newPlayerCard);
        }

        foreach (Card prefab in enemyCardPrefabs)
        {
            Card newEnemyCard = Instantiate(prefab, enemyDeckLocation.position, prefab.transform.rotation);            
            cardDecks.enemyCards.Add(newEnemyCard);
        }
    }    
}
