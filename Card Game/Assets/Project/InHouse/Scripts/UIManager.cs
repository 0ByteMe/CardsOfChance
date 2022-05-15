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
    [SerializeField] public TextMeshProUGUI playerScoreText;
    [SerializeField] public TextMeshProUGUI enemyScoreText;
    [SerializeField] private GameObject scoreUpdateVFX;

    public AnimationCurve myScoreTextCurve;

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
    public void UpdateScoreUI(TextMeshProUGUI scoreUI, int score)
    {
        scoreUI.text = score.ToString();
        Tween.LocalScale(scoreUI.transform, new Vector3(1.2f, 1.2f, 0), .5f, 0, myScoreTextCurve);
    }
}
