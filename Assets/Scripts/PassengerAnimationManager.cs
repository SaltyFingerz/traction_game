using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PassengerAnimationManager : MonoBehaviour
{
    Rigidbody2D rb2d;
    SpriteRenderer spriteRenderer;
    Animator passengerAnimator;

    enum PassengerState { Static, Moving };
    int passengerState = 0; //the passenger carriage has these two animation states. 

    string Animation_Parameter = "Animation_Passenger_State";
    private PlayerController playerController;
  
    public ParticleSystem blood; //this is to link the blood splatter effect to the passenger carriage game object.




    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        passengerAnimator = GetComponent<Animator>();

       

    }

   

    // Update is called once per frame
    void Update()
    {
        if (!PlayerController.movingRight && !PlayerController.movingLeft)
        {
            passengerState = (int)PassengerState.Static;
            passengerAnimator.SetInteger(Animation_Parameter, passengerState);
            //if the train is not moving the passenger carriage wheels do not turn and the passenger does not blink but looks out impatiently.
        }

        else if (PlayerController.movingRight || PlayerController.movingLeft)
        {
            passengerState = (int)PassengerState.Moving;
            passengerAnimator.SetInteger(Animation_Parameter, passengerState);
           //if the train is moving the wheels of the passenger carriage turn and the passenger goes through an animation loop of blinking.

        }

        


    }


}
