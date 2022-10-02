using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    public Transform train;
    public string level1, level2;

    void Update()
    {
        Scene scene = SceneManager.GetActiveScene();

        //the code below adjusts the camera's position in relation to the train in each level, according to the amount of carriages present that must be included. 
        if (scene.name == level1)
        {
            Camera.main.transform.position = new Vector3(train.position.x + 4, train.position.y + 2, Camera.main.transform.position.z);
            //with only the cargo carriage and the engine the camera in level one is positioned more to the right so as not to compromise seeing what is ahead. 
            
        }
        else if(scene.name == level2)
        {
            Camera.main.transform.position = new Vector3(train.position.x + 2, train.position.y + 2, Camera.main.transform.position.z);
            //with the extra passenger carriage the camera in level2 is positioned more to the left so this carriage can be seen too, this makes the second level more difficult as the player cannot see so far ahead. 
        }
    }
}
