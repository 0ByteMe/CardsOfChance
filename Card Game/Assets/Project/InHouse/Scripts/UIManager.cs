using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;
using UnityEngine.UI;
using TMPro;


public class UIManager : MonoBehaviour
{
    [SerializeField] Button drawCardsButton;
    [SerializeField] public DisplayObject startGameUI;
    [SerializeField] public DisplayObject scoreUI;
    [SerializeField] public DisplayObject drawCardsButtonUI;
    [SerializeField] public DisplayObject gameOverUI;
    [SerializeField] GameObject playAgainButton;

    public void EnableDrawButton()
    {
        drawCardsButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.green;
        drawCardsButton.enabled = true;
    }
    public void DisableDrawButton()
    {
        drawCardsButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
        drawCardsButton.enabled = false;
    }
}
