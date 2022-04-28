using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RotateCardDetails : MonoBehaviour
{
    [SerializeField] TextMeshPro cardNameText;
    [SerializeField] Transform cardStrengthPivotPoint;
    [SerializeField] Transform cardSpritePivotPoint;
    Quaternion targetNameTextRotation;
    Quaternion targetStrengthTextRotation;
    Quaternion targetSpriteRotation;

    float speed = 0.1f;
    float timeCount = 0.0f;

    private void Start()
    {
        targetSpriteRotation = Quaternion.Euler(-90, 0, 0);
        targetNameTextRotation = Quaternion.Euler(-90, 0, 0);
        targetStrengthTextRotation = Quaternion.Euler(0, 180, 0);
        StartCoroutine(PauseThenDisable());
    }

    void Update()
    {
        cardSpritePivotPoint.transform.localRotation = Quaternion.Lerp(cardSpritePivotPoint.transform.localRotation, targetSpriteRotation, timeCount * speed);
        cardNameText.transform.localRotation = Quaternion.Lerp(cardNameText.transform.localRotation, targetNameTextRotation, timeCount * speed);
        cardStrengthPivotPoint.transform.localRotation = Quaternion.Lerp(cardStrengthPivotPoint.transform.localRotation, targetStrengthTextRotation, timeCount * speed);
        timeCount = timeCount + Time.deltaTime;
    }

    private IEnumerator PauseThenDisable()
    {
        yield return new WaitForSeconds(1.5f);
        enabled = false;
    }
}
