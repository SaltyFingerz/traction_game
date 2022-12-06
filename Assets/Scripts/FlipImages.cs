using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlipImages : MonoBehaviour
{

    public Sprite RightImage;
    public Sprite LeftImage;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (PlayerController.flipped)
        {
            gameObject.GetComponent<Image>().sprite = LeftImage;
        }
        else if (!PlayerController.flipped)
        {
            gameObject.GetComponent<Image>().sprite = RightImage;
        }
    }
}
