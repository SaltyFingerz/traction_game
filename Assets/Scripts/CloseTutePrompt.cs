using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseTutePrompt : MonoBehaviour
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
        if(Input.anyKey)
        {
            StartCoroutine(NextPrompt());
        }
    }

    IEnumerator NextPrompt()
    {
        yield return new WaitForSeconds(2f);
        PromptDeac.SetActive(false);
        PromptAct.SetActive(true);
    }
}
