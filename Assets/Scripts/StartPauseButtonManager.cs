using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPauseButtonManager : MonoBehaviour
{

    public Sprite StartButton;
    public Sprite PauseButton;
    public static bool ButtonIsStart = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (UIButtonManager.PauBut)
        {
            gameObject.GetComponent<Image>().sprite = StartButton;
            ButtonIsStart = true;
        }

        else
        {
            gameObject.GetComponent<Image>().sprite = PauseButton;
            ButtonIsStart = false;
        }
    }
}
