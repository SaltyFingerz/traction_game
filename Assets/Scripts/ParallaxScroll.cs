using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScroll : MonoBehaviour
{
   
    public float cloudsParallaxSpeed; //the speed the clouds move oposite to the player's movement, i.e. how close they are.
    public float rockParallaxSpeed; //the speed the rocks in the distance move oposite to the player's movement, i.e. how close they are.
    private PlayerController playerController; //to access the public variable of the player's movement (movingRight)


    void Update()
    {
        if (gameObject.name.Contains("clouds"))
        {
            if (PlayerController.movingRight)
            {
                transform.position += Vector3.right * (cloudsParallaxSpeed * Time.deltaTime);
                //factors the movement of the clouds by an amount set in the unity editor. This is between 0 and 1 and typically less than that of the rocks.
                //Time.deltaTime is used to callibrate the speed of the backgrounds movement to the speed at which each frame is rendered. 

            }
            else if (PlayerController.movingLeft)
            {
                transform.position -= Vector3.right * (cloudsParallaxSpeed * Time.deltaTime);
            }
        }
        else if (gameObject.name.Contains("rock"))
        {
            if (PlayerController.movingRight)
            {
                transform.position += Vector3.right * (rockParallaxSpeed * Time.deltaTime); 
                //factors the movement of the clouds by an amount set in the unity editor. This is between 0 and 1 and typically more than that of the clouds.
            }
            else if (PlayerController.movingLeft)
            {
                transform.position -= Vector3.right * (rockParallaxSpeed * Time.deltaTime);
            }
        }

    }
}
