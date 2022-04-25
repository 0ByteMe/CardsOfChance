using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Card : MonoBehaviour
{
    [SerializeField] TextMeshPro _cardNameText;
    [SerializeField] TextMeshPro _cardStrengthText;
    [SerializeField] string _cardName;
    [SerializeField] int _CardStrength;

    private void Awake()
    {
        _cardNameText.text = _cardName;
        _cardStrengthText.text = _CardStrength.ToString();
    }
}
