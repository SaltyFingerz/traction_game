using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("Component")]
    public TextMeshProUGUI timerText;
    [Header("Timer Settings")]

    [SerializeField]
    public static float currentTime;
    public static bool stop = false;
    float startingTime;

    // Start is called before the first frame update
 

     void Update()
    {
        if (stop)
        {
            currentTime = currentTime;
            timerText.text = currentTime.ToString("0.0");
            print("stop");
        }

        else

        {
            currentTime += Time.deltaTime;
            timerText.text = currentTime.ToString("0.0");
        }

       

    }
}
