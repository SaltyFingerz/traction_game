using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackForcePassenger : MonoBehaviour
{
    //this script adds additional forces to the passenger carriage when it is on specific orientations of track that require them. 
    //the structure of this script is practically the same as that of TrackForce with minor adjustments.

    Rigidbody2D rb2d;
    private PlayerController playerController;
    public static bool onVertical = false;
    public static bool onInverted = false;

   
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void UpdateForce()
    {
        if (onVertical == true)
        {
            rb2d.AddForce(new Vector2(3.5f, 4.2f));

        }

        else if (onInverted == true)
        {
            rb2d.AddForce(new Vector2(0, 4.5f));
            
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.tag.Contains("rotated90") && other.gameObject.name.Contains("straight")) || (other.gameObject.tag.Contains("rotated45") && other.gameObject.name.Contains("up")))
        {
            onVertical = true;
            onInverted = false;
            PlayerController.movingRight = false;
            PlayerController.movingUp = true;
        }

        else if (other.gameObject.tag.Contains("rotated135"))
        {
            onVertical = false;
            onInverted = true;
            PlayerController.movingRight = false;
            PlayerController.movingUp = false;
            PlayerController.movingLeft = true;
        }
    }



    void Update()
    {
        UpdateForce();
    }


}
