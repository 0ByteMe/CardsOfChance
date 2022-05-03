using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Card : MonoBehaviour
{
    [SerializeField] TextMeshPro cardNameText;
    [SerializeField] TextMeshPro cardStrengthText;
    [SerializeField] TextMeshPro cardPlayerOrEnemyText;    
    [SerializeField] string cardName;
    [SerializeField] private int cardStrength;

    private string enemy = "Enemy";
    private string player = "Player";
    public bool isPlayer;

    private void Awake()
    {
        cardNameText.text = cardName;
        cardStrengthText.text = cardStrength.ToString();
    }

    private void Start()
    {
        if (isPlayer)
        {
            cardPlayerOrEnemyText.text = player;
        }
        else
        {
            cardPlayerOrEnemyText.text = enemy;
        }
    }

    public int CardStrength
    {
        get
        {            
            return cardStrength;
        }        
    }
}
