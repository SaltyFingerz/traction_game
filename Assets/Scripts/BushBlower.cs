using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushBlower : MonoBehaviour
{
    private bool movingRight = true; 
    //this variable describes if the bush is seen moving to the right.
    public Transform camera;
    //links to the Main Camera in the Unity editor for determining when the bush exits the field of view and where it should restart.
    public float movementSpeed = 0.0001f; 
    //the speed of the bush can be edited in the unity editor, different levels may have different wind velocities. 
   
    
    void Update()     
    {

        

        if (movingRight && transform.position.y<=-2.2f) //for the bush to rotate counter clockwise and move upwards when it is lower than a certain point not too far above ground. 
        {
            transform.Translate(new Vector3(movementSpeed, 0, 0)); //the bush movement in terms of object space. 
            transform.Rotate(0, 0, 0.4f); // the bush's rotation as it steadily turns counter-clockwise causing it to move upwards

        }       
        else if (movingRight && transform.position.y >= -2.2f) //to move the bush along a waveform when it gets highger than a certain point it is to move downwards.
        {
            transform.Translate(new Vector3(movementSpeed, 0, 0)); //the bush continues moving at the same speed. 
            transform.Rotate(0, 0, -0.4f); //the bush's rotation changes direction to clockwise causing it to move downwards
        }
            if (transform.position.x <= -8) movingRight = true;  //the bush is to move when it is repositioned to the left of the field of view. This did not happen without this line of code. 
        
        else if(transform.position.x > camera.position.x+20 || transform.position.y>5) //when the bush leaves the field of view of the camera within a certain margin..
        {
            transform.position = new Vector3(camera.position.x -20, -3, 0); //..it is to be repositioned to the left of the camera to simulate a new bush. 
        }

               
    }}