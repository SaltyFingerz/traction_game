using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    //the inventory essentially contains track pieces that are reduced upon use and gained upon loading a level or picking up a crate.
    public static int tracksAvailable; 
    public static int straightTracksAvailable = 42;
    public static int upTracksAvailable = 22;
    public static int downTracksAvailable = 22;
    // Start is called before the first frame update
   

   public void RefreshTracks()
    {
        straightTracksAvailable = 42;
        upTracksAvailable = 22;
        downTracksAvailable = 22;
        //sets the initial track pieces. This function gets called when (re)starting a level in the UIButtonManager script
    }

    public void PickUpTracks()
    {
        straightTracksAvailable += 50;
        upTracksAvailable += 50;
        downTracksAvailable += 50;
        //adds track pieces. This function gets called when colliding with a crate pickup in the PlayerController script. 
    }



    // Update is called once per frame
    void Update()
    {
        
        tracksAvailable = straightTracksAvailable + upTracksAvailable + downTracksAvailable;
        //this can be used for caculating the score so the more tracks available at the end the higher the score. 
    }
}
