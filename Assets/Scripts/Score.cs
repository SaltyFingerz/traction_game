using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Score : MonoBehaviour
{

    private UIButtonManager uIButtonManager;



    //using Brackeys Score and UI video (E07) (https://www.youtube.com/watch?v=TAGZxRMloyU&t=392s) This tutorial was used for the basics of recording score. The code here is different as it uses pickups, and is dependent on time rather than distance. 
    public static int BaseScore = 100; //this is the initial score when starting a level.
    public static int CurrentScore; // this is the current score the player has, it is not displayed the player until achieving a new high score. 
    public static int CrateScore = 0;
  
    void Update()
    {
       
        CurrentScore = BaseScore - (int)Time.timeSinceLevelLoad + CrateScore; //the current score is the base score minus the time. Thus the faster they complete the levels the higher the score will be. 
                                                                              //dying or resetting resets the score to 100. The crate score is nullified upon loading scenes in the UIButtonManagerScript.

        
      
    }

   
}
