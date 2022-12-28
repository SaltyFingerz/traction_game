using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutePrompt8 : MonoBehaviour
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
        if (!UIButtonManager.PauBut)
        {
            StartCoroutine(NextPrompt());
        }
    }

    IEnumerator NextPrompt()
    {
        yield return new WaitForSeconds(1f);
        PromptDeac.SetActive(false);
        PromptAct.SetActive(true);
        PlayerPrefs.SetFloat("Prog1", 1f);

    }
}
