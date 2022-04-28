using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RotateText : MonoBehaviour
{
    [SerializeField] TextMeshPro cardNameText;
    [SerializeField] TextMeshPro cardStrengthText;
    Quaternion targetRotation;
    float speed = 0.1f;
    float timeCount = 0.0f;

    private void Start()
    {
        targetRotation = Quaternion.Euler(-90, 0, 0);
        StartCoroutine(PauseThenDisable());
    }

    void Update()
    {
        cardNameText.transform.localRotation = Quaternion.Lerp(cardNameText.transform.localRotation, targetRotation, timeCount * speed);
        cardStrengthText.transform.localRotation = Quaternion.Lerp(cardNameText.transform.localRotation, targetRotation, timeCount * speed);
        timeCount = timeCount + Time.deltaTime;
    }

    private IEnumerator PauseThenDisable()
    {
        yield return new WaitForSeconds(1.5f);
        enabled = false;
    }
}
