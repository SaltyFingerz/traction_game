using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{


    //to make the crate disappear upon pickup
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("train"))
        {
            Destroy(gameObject);
            //ref: https://docs.unity3d.com/ScriptReference/Object.Destroy.html 
            //the above is for the pickup game object to exit the scene upon pick-up.
        }

    }

   
}
