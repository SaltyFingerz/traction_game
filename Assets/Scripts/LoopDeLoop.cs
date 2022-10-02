using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopDeLoop : MonoBehaviour
{
    Collider2D loopColliders;
   //this script is attached to up tracks as a consecution of these make up the essential loopdeloop configuration.
    void Start()
    {
        loopColliders = GetComponent<Collider2D>();
    }

    private void OnCollisionExit2D(Collision2D theOther)
    {
        if (theOther.gameObject.name.Contains("train"))
        {
            StartCoroutine(DeactivateLoop());
            //ref: https://docs.unity3d.com/ScriptReference/WaitForSeconds.html 
            //Coroutine is used to introduce an IEnumerator and a time span after which the tracks will become intangible. 
        }

    }

    IEnumerator DeactivateLoop()
    {
        yield return new WaitForSeconds(1.5f);
        loopColliders.enabled = false;

        
        //this is to allow the train to exit the loop after having traversed it.

    }
}
