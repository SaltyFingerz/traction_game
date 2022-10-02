using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIDropDownManager : MonoBehaviour
{
   
    public Dropdown dropdown; 
    //the following boolean variables are public as they determine the speed at which the train moves in the PlayerController script. 
    public static bool Normal = true;
    public static bool Hard = false;



    //ref for the following function comes from https://www.youtube.com/watch?v=5onggHOiZaw. I adapted it to the simple Dropdown rather than TextMeshPro and changed it by using bool variables in each instance instead of output.text
    public void HandleInputData(int val) //val denotes that the options in the menu will be refered to by numerical values and int that these are of type integer. 
    {

        if (val == 0)
        {
            Normal = true;
            Hard = false;

        }
        if (val == 1)
        {
            Normal = false;
            Hard = true;
        }
    }


}
