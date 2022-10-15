using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    public Transform train;
    public string level1, level2, level3, level4, level5, level6, level7, level8;
    public GameObject player;

    public float speed = 3f;
    private Vector2 threshold;
    public GameObject followObject;
    public Vector2 followOffset;
    private Rigidbody2D rb;



    private Vector3 calculateThreshold()
    {
        Rect aspect = Camera.main.pixelRect;
        Vector2 t = new Vector2(Camera.main.orthographicSize * aspect.width / aspect.height, Camera.main.orthographicSize);
        t.x -= followOffset.x;
        t.y -= followOffset.y;
        return t;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector2 border = calculateThreshold();
        Gizmos.DrawWireCube(transform.position, new Vector3(border.x * 2, border.y * 2, 1));

    }

    private void Start()
    {
        
        rb = followObject.GetComponent<Rigidbody2D>();

    }

    private void FixedUpdate()
    {

        
    }

    void Update()
    {

        Scene scene = SceneManager.GetActiveScene();

        if (PlayerController.camDown)
        {
            Vector2 follow = followObject.transform.position;
            float xDifference = Vector2.Distance(Vector2.right * transform.position.x, Vector2.right * follow.x);
            float yDifference = Vector2.Distance(Vector2.up * transform.position.y, Vector2.up * follow.y);

            Vector3 newPosition = transform.position;
             if(Mathf.Abs(xDifference)>= threshold.x)
             {
                 newPosition.x = follow.x + 4;

             }
            if (Mathf.Abs(yDifference) >= threshold.y)
            {
                newPosition.y = follow.y - 2;

            }
            float moveSpeed = rb.velocity.magnitude > speed ? rb.velocity.magnitude : speed;
            transform.position = Vector3.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);

            //Camera.main.transform.position = new Vector3(train.position.x + 4, train.position.y - 2, Camera.main.transform.position.z);
        }

        if (PlayerController.camCent)
        {
            Vector2 follow = followObject.transform.position;
            float xDifference = Vector2.Distance(Vector2.right * transform.position.x, Vector2.right * follow.x);
            float yDifference = Vector2.Distance(Vector2.up * transform.position.y, Vector2.up * follow.y);

            Vector3 newPosition = transform.position;
            if (Mathf.Abs(xDifference) >= threshold.x)
            {
                newPosition.x = follow.x + 4;

            }
            if (Mathf.Abs(yDifference) >= threshold.y)
            {
                newPosition.y = follow.y;

            }
            float moveSpeed = rb.velocity.magnitude > speed ? rb.velocity.magnitude : speed;
            transform.position = Vector3.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);

            //Camera.main.transform.position = new Vector3(train.position.x + 4, train.position.y - 2, Camera.main.transform.position.z);
        }

        //the code below adjusts the camera's position in relation to the train in each level, according to the amount of carriages present that must be included. 
        else if (scene.name == level1)
        {
            Camera.main.transform.position = new Vector3(train.position.x + 4, train.position.y + 2, Camera.main.transform.position.z);
            //with only the cargo carriage and the engine the camera in level one is positioned more to the right so as not to compromise seeing what is ahead. 
            
        }
        else if(scene.name == level2)
        {
            Camera.main.transform.position = new Vector3(train.position.x + 2, train.position.y + 2, Camera.main.transform.position.z);
            //with the extra passenger carriage the camera in level2 is positioned more to the left so this carriage can be seen too, this makes the second level more difficult as the player cannot see so far ahead. 
        }

        else if (scene.name == level3)
        {
            Camera.main.transform.position = new Vector3(train.position.x + 2, train.position.y + 2, Camera.main.transform.position.z);
            //with the extra passenger carriage the camera in level2 is positioned more to the left so this carriage can be seen too, this makes the second level more difficult as the player cannot see so far ahead. 
        }

    }
    
}
