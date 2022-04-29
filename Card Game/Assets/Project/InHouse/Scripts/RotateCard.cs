using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCard : MonoBehaviour
{
    Quaternion targetRotation;
    [SerializeField] float rotateSpeed = 0.1f;
    float timeCount = 0.0f;

    private void Start()
    {
        targetRotation = Quaternion.Euler(-270, -90, -90);  
        StartCoroutine(PauseThenDisable());
    }

    void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, timeCount * rotateSpeed);
        timeCount = timeCount + Time.deltaTime;        
    } 
    
    private IEnumerator PauseThenDisable()
    {
        yield return new WaitForSeconds(1.5f);
        enabled = false;
    }
}
