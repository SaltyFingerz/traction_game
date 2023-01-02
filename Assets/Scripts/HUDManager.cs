using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    
    public Text trackText; //this variable is the text that is displayed under each track piece type, denoting the number of each piece's availability.
    public GameObject Player; //this links to the train_front in unity editor
   // public int callingTracksNumber;
    private InventoryManager inventoryManager; //this links to the inventory manager script where the information of the amount of each track piece available is.

   

  void Update ()
    {
        if (gameObject.name.Contains("StraightTracks")) //if this script is attached to the StraightTracks game object child of the UI HUD object
        {
            trackText.text = "x"+InventoryManager.straightTracksAvailable.ToString(); //display in the text that is underneath the straight tracks HUD image the number of straight tracks available in the inventory. 
        }
        else if (gameObject.name.Contains("UpTracks")) //if this script is attached to the UpTracks game object child of the UI HUD object...
        {
            trackText.text = "x"+ InventoryManager.upTracksAvailable.ToString();
        }
        else if (gameObject.name.Contains("DownTracks"))
        {
            trackText.text = "x" + InventoryManager.downTracksAvailable.ToString();
        }
        // ref: Brackeys tutorial https://www.youtube.com/watch?v=TAGZxRMloyU . I modified the code to display the inventory in the HUD instead of the score. 
    }
} 
