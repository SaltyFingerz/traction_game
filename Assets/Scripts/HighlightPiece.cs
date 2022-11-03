using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighlightPiece : MonoBehaviour
{
    public Sprite NoSelection;
    public Sprite StraightPiece;
    public Sprite DownPiece;
    public Sprite UpPiece;
    public Sprite StartButton;
    public Sprite PauseButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.nextTrack == "straight")
        {
            gameObject.GetComponent<Image>().sprite = StraightPiece;
        }

        if (PlayerController.nextTrack == "up")
        {
            gameObject.GetComponent<Image>().sprite = UpPiece;
        }

        if (PlayerController.nextTrack == "down")
        {
            gameObject.GetComponent<Image>().sprite = DownPiece;
        }

        if (PlayerController.nextTrack == "none")
        {
            gameObject.GetComponent<Image>().sprite = NoSelection;
        }

        

    }
}
