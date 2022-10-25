using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseTutePrompt5 : MonoBehaviour
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
        if(Input.GetKeyDown("D") || Input.GetKeyDown("right"))
        {
            StartCoroutine(NextPrompt());
        }
    }

    IEnumerator NextPrompt()
    {
        yield return new WaitForSeconds(1f);
        if (Input.GetKeyDown("D") || Input.GetKeyDown("right"))
        {
            PromptDeac.SetActive(false);
            PromptAct.SetActive(true);
        }
    }
}
