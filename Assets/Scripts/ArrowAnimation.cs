using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowAnimation : MonoBehaviour
{
    private bool grow = true;
    private bool shrink;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((transform.localScale.x <= 1.2f) && grow)
        {
            transform.localScale +=  new Vector3(1, 1, 0) * Time.deltaTime;
            print("grow");
        }

        if (transform.localScale.x >= 1.2f)
        {
            grow = false;
            shrink = true;
            print("should shrink");
        }

         if (transform.localScale.x >= 0.5f && shrink)
        {
            transform.localScale -= new Vector3(1, 1, 0) * Time.deltaTime;
            print("shrink");
        }

      

         if (transform.localScale.x <= 0.5f)
        {
            grow = true;
            shrink = false;
        }
    }
}
