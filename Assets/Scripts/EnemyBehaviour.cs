using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class EnemyBehaviour: MonoBehaviour

{ private bool movingRight = true; 
    public float movementSpeed = 0.1f; 
    private float startX = 0;
    SpriteRenderer spriteRenderer;
    Rigidbody2D r2d;
 

    // Start is called before the first frame update     
    void Start()     
    {        startX = transform.position.x;
        spriteRenderer = GetComponent<SpriteRenderer>();
        r2d = GetComponent<Rigidbody2D>();
    }

    void IsAwake()
    {
        if (movingRight)
        {
            transform.Translate(new Vector3(movementSpeed, 0, 0));
            spriteRenderer.flipX = true;
        }
        else
        {
            transform.Translate(new Vector3(-movementSpeed, 0, 0));

            spriteRenderer.flipX = false;
        }
    }
    // Update is called once per frame  
    void Update()
    {

        IsAwake();
        
        if (movingRight)
        {
            transform.Translate(new Vector3(movementSpeed, 0, 0));
            spriteRenderer.flipX = true;
        }
        else
        {
            transform.Translate(new Vector3(-movementSpeed, 0, 0));

            spriteRenderer.flipX = false;
        }

        if (transform.position.x <= -0.5)
                movingRight = true;

            else if (transform.position.x > 5)
                movingRight = false;


        } 
}