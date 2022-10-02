using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackTriggers : MonoBehaviour
{


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("train_front"))
        {

           GetComponent<BoxCollider2D>().enabled = false;
            // ref: https://docs.unity3d.com/ScriptReference/BoxCollider2D.html the box collider 2D with the trigger must be first in order in the unity editor otherwise the actual (non-trigger) collider will be disabled. 
            //Changing the order of the box colliders meant unpacking the straight_track prefab. 
            //This function is to avoid adding more than one track piece to a single track piece. 

        }
    }
  
}
