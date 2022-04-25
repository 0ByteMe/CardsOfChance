using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class CardSpawner : MonoBehaviour
{
    [Header("Card Deck Prefabs")]
    [SerializeField] private GameObject[] playerCardPrefabs;
    [SerializeField] private GameObject[] enemyCardPrefabs;
    [Header("Initial Card Decks")]   
    public List<GameObject> playerCards;
    public List<GameObject> enemyCards;
    [Header("Shuffled Card Decks")]
    public List<GameObject> shuffledPlayerCards;
    public List<GameObject> shuffledEnemyCards;
    [Header("Deck Locations")]
    [SerializeField] public Transform playerDeckLocation;
    [SerializeField] public Transform enemyDeckLocation;

    private void Start()
    {
        InstantiateCardDecks();
        ShuffleCardDecks();
    }

    //Quaternion.Euler(-90, -90, -90) working angle, changing it for looks in video temporarily
    private void InstantiateCardDecks()
    {
        foreach (GameObject prefab in playerCardPrefabs)
        {
           GameObject newPlayerCard = Instantiate(prefab, playerDeckLocation.position, Quaternion.Euler(90, -90, -90));
            playerCards.Add(newPlayerCard);
        }

        foreach (GameObject prefab in enemyCardPrefabs)
        {
            GameObject newEnemyCard = Instantiate(prefab, enemyDeckLocation.position, Quaternion.Euler(90, -90, -90));
            enemyCards.Add(newEnemyCard);
        }
    }

    private void ShuffleCardDecks()
    {
        shuffledPlayerCards = new List<GameObject>();
        shuffledPlayerCards = playerCards.OrderBy(x => Random.value).ToList();

        shuffledEnemyCards = new List<GameObject>();
        shuffledEnemyCards = enemyCards.OrderBy(x => Random.value).ToList();
    }
}
