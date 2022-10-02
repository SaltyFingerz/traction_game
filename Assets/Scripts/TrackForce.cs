using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackForce : MonoBehaviour
{
    // this script is to function as an attractor of the tracks during the loopdeloop, providing the necessary forces to be applied to the train to keep it on track in haphazard circumstances. 
    // this script is used only by the train_front. The other carriages have their own TrackForce scripts with subtle differences of force to meet their individual requirements and time these to when they reach the relevant points of track. 

    Rigidbody2D rb2d;
    private PlayerController playerController;
    private Score score; //score is altered in this script to reward players who successfully do a loop de loop stunt.
    private UIDropDownManager uIDropDownManager; //the UIDropdownManager is included for adjusting the forces applied according to the speed at which the train moves (which is determined in the drop down menu)

    //below are the different states at which the train requires additional helping forces to be applied to it to keep it on track.
    public static bool onVertical = false;
    public static bool onInverted = false;
    public static bool onSteepDown = false;
    public static bool onAssistedAsc = false;
    public static bool onAssistedDesc = false;


    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void UpdateForce()
    {
        if (onVertical == true)
        {
            rb2d.AddForce(new Vector2(5f, 9f));
            //AddForce code was taught in the 'player controller' practical of DES105 and is used here to control the position of the train during the course of a loopdeloop configuration so that it stays on track.
           

           
        }

        else if (onInverted == true)
        {
            rb2d.AddForce(new Vector2(-5f, 9f));
            transform.Rotate(0, 0, 1, Space.Self);
            //modified the following code from https://docs.unity3d.com/ScriptReference/Transform.Rotate.html: cube1.transform.Rotate(xAngle, yAngle, zAngle, Space.Self) to make the train rotated enough to stay on track during loopdeloop;


        }

        else if (onSteepDown == true) 
        {
            rb2d.AddForce(new Vector2(-10f, 0));
            transform.Rotate(0, 0, 1, Space.Self);
           


        }

        else if (onAssistedAsc == true) //this is not used in the loop de loop but is required for riding over steep bumps of track.
        {
            rb2d.AddForce(new Vector2(1, 1));
          
        }

        else if (onAssistedDesc == true)
        {
            rb2d.AddForce(new Vector2(-10f, 4));
            Score.BaseScore += 50; //reward player for doing loop or other similarly risky move.
        }
    }

    void OnTriggerEnter2D(Collider2D other) //specifies the conditions relative to the track currently under the train, that require additional forces. Each condition is mutually exclusive.
    {
        if ((other.gameObject.tag.Contains("rotated90") && other.gameObject.name.Contains("straight")) || (other.gameObject.tag.Contains("rotated45") && other.gameObject.name.Contains("up")))
        {
            onVertical = true;
            onInverted = false;
            PlayerController.movingRight = false;
            PlayerController.movingUp = true;
        }

        else if (other.gameObject.tag.Contains("rotated135") || other.gameObject.tag.Contains("rotated180"))
        {
            onVertical = false;
            onInverted = true;
            PlayerController.movingRight = false;
            PlayerController.movingUp = false;
            PlayerController.movingLeft = true;
        }

        else if (other.gameObject.tag.Contains("rotated270") && other.gameObject.name.Contains("up"))
        {
            onVertical = false;
            onInverted = false;
            onSteepDown = true;
            onAssistedAsc = false;
            onAssistedDesc = false;
            PlayerController.movingRight = false;
            PlayerController.movingUp = false;
            PlayerController.movingLeft = false;
            PlayerController.movingDown = true;
        }

        else if (other.gameObject.tag.Contains("rotated225") && other.gameObject.name.Contains("up"))
        {
            onVertical = false;
            onInverted = false;
            onSteepDown = false;
            onAssistedAsc = false;
            onAssistedDesc = true;
            //for keeping the train on track during top left part of loopdeloop
           
        }

       

        else if (other.gameObject.name.Contains("down") && other.gameObject.tag.Contains("rotated45") && UIDropDownManager.Normal == true)
        {
            onVertical = false;
            onInverted = false;
            onSteepDown = false;
            onAssistedAsc = true;
            onAssistedDesc = false;
            //this assisted ascension is to get across steep hills
        }

        else if (other.gameObject.name.Contains("straight") && other.gameObject.tag.Contains("track"))
        {
            onVertical = false;
            onInverted = false;
            onSteepDown = false;
            onAssistedAsc = false;
            onAssistedDesc = false;
        }
    }


    
    void Update()
    {
        UpdateForce();
    }


}
