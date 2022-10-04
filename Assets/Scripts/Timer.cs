using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("Component")]
    public TextMeshProUGUI timerText;
    [Header("Timer Settings")]

    public float currentTime;

    float startingTime;

    // Start is called before the first frame update
 

     void Update()
    {
        currentTime += Time.deltaTime;
        timerText.text = currentTime.ToString("0.0");

    }
}
