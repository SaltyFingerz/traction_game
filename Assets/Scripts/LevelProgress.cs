using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelProgress : MonoBehaviour
{
  

    public GameObject Lock3;
    public GameObject Lock4;
    public GameObject Lock5;
    public GameObject Lock6;
    public GameObject Lock7;

    public GameObject Cert1;

    public GameObject Gold2;
    public GameObject Gold3;
    public GameObject Gold4;
    public GameObject Gold5;
    public GameObject Gold6;
    public GameObject Gold7;

    public GameObject Silver2;
    public GameObject Silver3;
    public GameObject Silver4;
    public GameObject Silver5;
    public GameObject Silver6;
    public GameObject Silver7;

    public GameObject Bronze2;
    public GameObject Bronze3;
    public GameObject Bronze4;
    public GameObject Bronze5;
    public GameObject Bronze6;
    public GameObject Bronze7;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetFloat("Prog1") == 1f)
        {
            Cert1.SetActive(true);
        }
        //Level 2 complete:
        if (PlayerPrefs.GetFloat("Prog2") > 1f)
        {
            Lock3.SetActive(false);
        }

        if(PlayerPrefs.GetFloat("Prog2") == 2.1f)
        {
            Gold2.SetActive(true);
        }
        else if (PlayerPrefs.GetFloat("Prog2") == 2.2f)
        {
            Silver2.SetActive(true);
        }

        else if (PlayerPrefs.GetFloat("Prog2") == 2.3f)
        {
            Bronze2.SetActive(true);
        }

        //Level 3 complete:
        if (PlayerPrefs.GetFloat("Prog3") > 1f)
        {
            Lock4.SetActive(false);
        }

        if (PlayerPrefs.GetFloat("Prog3") == 3.1f)
        {
            Gold3.SetActive(true);
        }
        else if (PlayerPrefs.GetFloat("Prog3") == 3.2f)
        {
            Silver3.SetActive(true);
        }
        else if (PlayerPrefs.GetFloat("Prog3") == 3.3f)
        {
            Bronze3.SetActive(true);
        }
        //Level4 complete:
        if (PlayerPrefs.GetFloat("Prog4") > 1f)
        {
            Lock5.SetActive(false);
        }
        if (PlayerPrefs.GetFloat("Prog4") == 4.1f)
        {
            Gold4.SetActive(true);
        }
        else if (PlayerPrefs.GetFloat("Prog4") == 4.2f)
        {
            Silver4.SetActive(true);
        }
        else if (PlayerPrefs.GetFloat("Prog4") == 4.3f)
        {
            Bronze4.SetActive(true);
        }
        //Level 5 complete:
        if (PlayerPrefs.GetFloat("Prog5") >1 )
        {
            Lock6.SetActive(false);
        }
        if (PlayerPrefs.GetFloat("Prog5") == 5.1f)
        {
            Gold5.SetActive(true);
        }
        else if (PlayerPrefs.GetFloat("Prog5") == 5.2f)
        {
            Silver5.SetActive(true);
        }
        else if (PlayerPrefs.GetFloat("Prog5") == 5.3f)
        {
            Bronze5.SetActive(true);
        }
        //Level6 complete:
        if (PlayerPrefs.GetFloat("Prog6") > 1f)
        {
            Lock7.SetActive(false);
        }
        if (PlayerPrefs.GetFloat("Prog6") == 6.1f)
        {
            Gold6.SetActive(true);
        }
        else if (PlayerPrefs.GetFloat("Prog6") == 6.2f)
        {
            Silver6.SetActive(true);
        }
        else if (PlayerPrefs.GetFloat("Prog6") == 6.3f)
        {
            Bronze6.SetActive(true);
        }
        //level7 complete:
        if (PlayerPrefs.GetFloat("Prog7") == 7.1)
        {
            Gold7.SetActive(true);
        }
        else if (PlayerPrefs.GetFloat("Prog7") == 7.2f)
        {
            Silver7.SetActive(true);
        }
        else if (PlayerPrefs.GetFloat("Prog7") == 7.3f)
        {
            Bronze7.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
