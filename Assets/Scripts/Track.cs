using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    
    //in hindsight the content of this script could be refactored into the player controller script, however the position of the straight track piece has been perfected using this more patchy method so it is kept.
    void Start()
    {
       
        transform.Translate(new Vector3(2, 0, 0));
        
    }

    
}
