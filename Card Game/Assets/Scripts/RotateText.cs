using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RotateText : MonoBehaviour
{
    [SerializeField] TextMeshPro cardNameText;
    [SerializeField] Transform cardStrengthPivotPoint;
    Quaternion targetNameTextRotation;
    Quaternion targetStrengthTextRotation;

    float speed = 0.1f;
    float timeCount = 0.0f;

    private void Start()
    {
        targetNameTextRotation = Quaternion.Euler(-90, 0, 0);
        targetStrengthTextRotation = Quaternion.Euler(0, 180, 0);
        StartCoroutine(PauseThenDisable());
    }

    void Update()
    {
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
