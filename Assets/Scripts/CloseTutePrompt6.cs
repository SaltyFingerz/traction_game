using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseTutePrompt6 : MonoBehaviour
{
    public GameObject PromptDeac;
    public GameObject PromptAct;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) || UIButtonManager.PauBut)
        {
            PromptDeac.SetActive(false);
            Invoke("NextPrompt", 1f);
        }
    }

    void NextPrompt()
    {
        
       
            PromptAct.SetActive(true);
       // PlayerPrefs.SetFloat("Prog1", 1f);
        
    }
}
