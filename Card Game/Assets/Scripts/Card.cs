using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Card : MonoBehaviour
{
    [SerializeField] TextMeshPro cardNameText;
    [SerializeField] TextMeshPro cardStrengthText;
    [SerializeField] string cardName;
    [SerializeField] private int cardStrength;

    private void Awake()
    {
        cardNameText.text = cardName;
        cardStrengthText.text = cardStrength.ToString();
    }

    public int CardStrength
    {
        get
        {            
            return cardStrength;
        }        
    }
}
