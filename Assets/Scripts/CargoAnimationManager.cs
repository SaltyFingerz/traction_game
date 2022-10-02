using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class CargoAnimationManager : MonoBehaviour
{
    Rigidbody2D rb2d;
    SpriteRenderer spriteRenderer;
    Animator cargoAnimator;
    enum CargoState { Static, Moving };
    //the cargo has the above two animation states
    int cargoState = 0;
    //it starts in the static state
    string Animation_Parameter = "Animation_Cargo_State";
    private PlayerController playerController;




    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        cargoAnimator = GetComponent<Animator>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerController.movingRight && !PlayerController.movingLeft)
        {
            cargoState = (int)CargoState.Static;
            cargoAnimator.SetInteger(Animation_Parameter, cargoState);
            //passes the 0 integer to the animator to be used as a parameter to run the static cargo animation when the train is not moving.
        }

        else if (PlayerController.movingRight || PlayerController.movingLeft)
        {
            cargoState = (int)CargoState.Moving;
            cargoAnimator.SetInteger(Animation_Parameter, cargoState);
            //passes the 1 integer to the animator to be used as a parameter to run the moving cargo animation when the train is moving. 
        }


    }


}
