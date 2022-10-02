﻿using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    Rigidbody2D rb2d;
    SpriteRenderer spriteRenderer;
    Animator playerAnimator;

    //below are other scripts from which variables get called in this one
    private TrackForce trackForce;
    private SFX sFX; //to get the AudioSources and play these to instances of player interaction with the level
    private Score score; //to access score variables and add to these when picking up a crate or reaching the goal of a level
    private UIDropDownManager uIDropDownManager; //to get the difficulty from the dropdown menu and apply this to the speed of the train.

    //below are variables in this script that get called in other scripts, but not changed there. 
    public static bool movingRight = false;
    public static bool movingLeft = false;
    public static bool movingUp = false;
    public static bool movingDown = false;
    public static bool Stop = false;

    private bool showNow = false; //this is to debug certain instances where adding a track piece did not result in it becoming visible.

    private string nextTrack; //this variable denotes the next type of track piece that the player tells the train to add. (it takes values of "straight" "up" "down"
    //public float movementSpeed = 0.1f;
    public Text highScore; //this is the text that displays the high score at the centre top of the screen in level 2. It is public to be linked to the HUD's HighScore game object that displays this text in the HUD UI.
   
    //below are the types of tracks that public to be linked to their respective prefabs in the unity editor, in order for them to be instantiated.
    public GameObject straight_track; 
    public GameObject up_track;
    public GameObject down_track;

  
    
    bool isOnRawTerrain = false;//this denotes whether the train is on the level's raw terrain as opposed to track pieces. 
    bool addingTrack = false;//this denotes whether the train is in the process of adding a track piece. It becomes true when instantiatin a track piece and subsequently activates the animation of adding a track piece.
    enum PlayerState { Static, Moving, Adding }; //0, 1, 2 are the respective integer parameters used for switching between the three different animations of the train_front i.e. engine (static, moving, adding).
    int playerState = 0; //this sets the starting player state to static i.e. the train is not moving till the player tells it to.
    string Animation_Parameter = "Animation_Player_State"; //this is the name of the parameter in the unity editor's animator window for the train_front. 
    
    //UI stuff here. The below link to the sound FX and menu screens that are accessible from within the gameplay scenes.
    public GameObject Player;
    public GameObject pauseMenu;
    public GameObject Victory;
    public GameObject Steam;
    public GameObject PickUp;
    public GameObject Music;
    public GameObject Engine;
    //the sounds were thus added using this tutorial (https://youtu.be/JnbDxG04i7c by Jimmy Vegas) I changed the sound allocation so each sound has an individual game object, because I want them to play simultaneously





    void Start()
    {
  

        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerAnimator = GetComponent<Animator>();
        highScore.text = "HIGH SCORE  " + PlayerPrefs.GetInt("HighScore", 0).ToString();
        //the 0 here denotes the initial highscore before any have been stored using playerprefs. 
        //this text is displayed in scene 2 at the centre top of the screen, using the HighScore game object in the UI HUD parent.


    }

    

    void Update()
    {
        /*
       Transform childTransform = transform.Find("Mechanism");
        //print(Input.GetAxis("Horizontal")); //GetAxisRaw shows -1, 0, 1 whereas GetAxis shows -1, 0, 1 and decimal numbers in between. 
        //MarioController();
        this was for flipping the track adding mechanism as a child, but it is instead part of the train front sprites.
        */

        UpdateTracks(); //this function is called every frame so the train is updated on which track the player next wants to add at all times. This may change between the time of the last track and the next track, giving the player some leeway in which they may change their mind. 

        //ref: this tutorial (https://www.youtube.com/watch?v=44djqUTg2Sg&t=2240s) and the DES105 lectorial on player controllers were used for developing the following controls. 
        //Instead of making the arrow keys directly correspond with a vector of movement, they alter intermediate variables that start or stop a constant movement to the right and determine the next track that is to be added. 
        //Thus I contributed the mechanism through which the player movement is controlled, no other game where the player adds elements to move along was found or copied.


        if (movingRight) //this gives the train a constant movement when it is told to move right, so the player doesn't have to hold the right key down in order to move, but uses the keys to control the track pieces that are added instead. 
        {
            if (UIDropDownManager.Normal == true)
            {
                rb2d.velocity = new Vector2(1, rb2d.velocity.y); //this sets the speed at which the train moves when the dificutly is set to normal.
            }

            else if (UIDropDownManager.Hard == true)
            {
                rb2d.velocity = new Vector2(2, rb2d.velocity.y); //this sets the speed to double that of normal, when the difficulty is set to hard. 
            }

                spriteRenderer.flipX = false; // Sprite renderer is used for flipping instead of transform so the child camera does not get flipped too. However this is not currently used as the train only moves rightwards.

           
        }


        //movingLeft (below) is not used currently as the train only moves rightwards, it may be further developed for later versions of the game. 

        /*else if (movingLeft)
        {
            rb2d.velocity = new Vector2(-1, rb2d.velocity.y);
            spriteRenderer.flipX = true;

           // childTransform.eulerAngles = new Vector3(0, 180, 0);
            //parent.transform.GetChild(0).gameObject.scaleX = -1;
        }*/

        

        else if (Stop)

        { rb2d.velocity = new Vector2(0, 0); //this sets the train's movement to 0 when it is told to stop.
        }



        if (Input.GetKey("right") && movingLeft == false) //to tell the train to move right when the right arrow key is pressed. movingLeft must be false, in case the train is currently moving left in the worldspace during a loopdeloop in which case this should overide moving right.
        {
            movingRight = true;
            movingLeft = false;
            Stop = false;
            Steam.GetComponent<SFX>().Steam.Play(); //moving right starts the engine so the sound effect is thus played.
            

        }
       

        else if (movingLeft == true)
        {

            movingRight = false;
            Stop = false;

        } //currentlty in the game the player can only move from left to right, up and down because the train needs u-turn-track to turn around on horizontal axis. This may be added in further developments. 

        else if (isOnRawTerrain || Input.GetKey("space")) //when to stop is determined by the player pressing space or landing on raw terrain, which cannot be traversed directly, thus the train requires tracks to move and can voluntarily stop at any point.
        { movingLeft = false;
            movingRight = false;
            Stop = true;
            Steam.GetComponent<SFX>().Steam.Stop(); //upon stopping, the train stops playing the steam engine sound.

        }



        UpdateAnimators(); //animators are updated every frame to ensure the train is doing the correct animation at all times. 

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            //pressing the escape key will pause the game and open the pause menu UI game object
        }






       

      
    }

   


    void UpdateAnimators()
    {
        if (!movingLeft && !movingRight && !movingUp && !movingDown) //if not moving at all
        {
            playerState = (int)PlayerState.Static; //the player state gets defined as the integer of the static player state in the array i.e. 0.
            playerAnimator.SetInteger(Animation_Parameter, playerState); //update the parameter to play the relevant i.e. static animation i.e. 0. This as defined in the animator window in unity editor, plays the static train animation.
        }

        else if (addingTrack) //if the train is currently adding a track piece
        {
            playerState = (int)PlayerState.Adding;
            playerAnimator.SetInteger(Animation_Parameter, playerState); //set the animation parameter to 2 by accessing the player state array, where adding is third in line.
           
            Engine.GetComponent<SFX>().Engine.Play();
            //upon adding a track piece play the sound of the engine, as this has a suitable clicking sound.
            
        }


        else if (movingLeft || movingRight || movingUp || movingDown)
        {
            playerState = (int)PlayerState.Moving;
            playerAnimator.SetInteger(Animation_Parameter, playerState);
            //set the animation parameter to that for moving as defined in the array as 1, thus playing the moving animation as defined in the animator window in unity editor. 
            

        }

        
    }

   


    void UpdateTracks()
    {


        /*adding a track manually - only for testing - not in game
        if (Input.GetKeyDown(KeyCode.T))
        {
            Instantiate(straight_track, transform.position + new Vector3(0.8f, -0.71f, 0), transform.rotation);
            tracksAvailable--;
            print("tracksAvailable are" + tracksAvailable);
        }
        */


        //adding track automatically - game mechanic - using input from wasd or arrow keys to tell the train which track to add next. 
        //the code below was learnt from following this tutorial: https://www.youtube.com/watch?v=44djqUTg2Sg&t=2240s and the player controller lectorial of DES105. 
        //I contributed to the basic controller by making the resulting variable a nextTrack rather than a vector of movement per se. The next track indirectly defines the subsequent movement of the player. 
        if (Input.GetKey("d") || Input.GetKey("right")) 
            {
                nextTrack = "straight";
            }    

            else if (Input.GetKey("w") || Input.GetKey("up"))
                {
                    nextTrack = "up";
                }

            else if (Input.GetKey("s") || Input.GetKey("down"))
            {
                nextTrack = "down";
            }

            else if (Input.GetKey("f"))
            {
                nextTrack = "none";
            }

        //this functions to keep the next track as that of the last pressed key so the player doesn't need to worry about pressing the same key again and again if they are adding he same piece type. 
        

    }

    




    private void OnCollisionEnter2D(Collision2D theOther) //collision enter is used to detect when the train touches raw terrain.
    {
        
        if (theOther.gameObject.name.Contains("Raw")) //this identifies the raw terrain game objects in each level as they have "Raw" included in their names. 
        {
    
            isOnRawTerrain = true; //boolean variable for controling train movement to stop it when it is on raw terrain. In most cases the player will want to restart upon this happening.

        }
    }

    private void OnCollisionExit2D(Collision2D theOther)
    {
        if (theOther.gameObject.name.Contains("Raw"))
        {
            isOnRawTerrain = false;
            //this is for the possibility that the train exits raw terrain. This can happen by pressing the right or 'd' key which gives the train a small burst of rightward movement. 

        }
    }

    GameObject newTrack; 
    //this denotes the last added track and is used to give it a tag that describes its rotation and is used to control its visibility while the train is carrying out the adding track animation, in order for the new track to be added before it becomes visible.


    public void Reset()
    {
        PlayerPrefs.DeleteKey("HighScore");
        highScore.text = "0";
        //from Brackeys High Score tutorial video (https://www.youtube.com/watch?v=vZU51tbgMXk.) this function is for resetting the highscore.
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("Crate")) //upon the train colliding with the crate trigger. The crate is a trigger because it is a pickup not an obstacle.
        {
            Score.CrateScore += 20; //the crate awards the player with points 
            Player.GetComponent<InventoryManager>().PickUpTracks(); //the crate awards the player with extra track pieces too. This is carried out using the PickUpTrack function in the Inventory manager script. 
            PickUp.GetComponent<SFX>().PickUp.Play(); //upon picking up the crate, the sound of a crate opening is played.
            



        }

        if (other.gameObject.name.Contains("Goal1")) //this is upon colliding with the trigger at the end of level 1, marking the level's completion.
        {


            Score.BaseScore += 100; //the player is awarded 100 points for completing this level. However a highscore is not yet recorded, until the second level is completed at which point the score accumulated in level one is included.

            PlayerController.Stop = true;
            PlayerController.movingLeft = false;
            PlayerController.movingRight = false;
            //the above is to ensure the player stops moving upon reaching the goal as this is the end of the level.


            StartCoroutine(VictoryEnsemble());
            //a coroutine is initiated to ensure enough time to play the victorious music allowing the player to celebrate briefly before embarking on the next level.
            
        }

        IEnumerator VictoryEnsemble()
        {
            Music.GetComponent<SFX>().Music.Stop();
            //ref: https://docs.unity3d.com/ScriptReference/AudioSource.Stop.html. Thus the soundtrack stops to be replaced by the Victory sound for 1.5 seconds. 

            Victory.GetComponent<SFX>().Victory.Play(); //Thus the victory sound is played.
            
            yield return new WaitForSeconds (1.5f); //Thus one and a half seconds is provided for the victory to be celebrated.
            Time.timeScale = 1; //this ensures the time is flowing normally into the next level.
            SceneManager.LoadScene(2); //this loads the next level 
            Player.GetComponent<InventoryManager>().RefreshTracks(); //this reloads the track pieces available to the player, repleneshing any lost in level one.
            Music.GetComponent<SFX>().Music.Play(); //this replays the game's background music as this was stopped for the victory music. 

           
        }

        if (other.gameObject.name.Contains("Goal2"))
        {
            //for the high score, playerprefs is used to save the score even after the game is closed and reopened.
            //This tutorial by Brackeys provided the basic code https://www.youtube.com/watch?v=vZU51tbgMXk. 
            //I modified the content of this to record the high score only at the end of level2, including the score gathered from level 1. 
            //The system of high score works by rewarding players who completed level 1 in the same go, who did so on the difficult mode, without dying or resetting and who took the most direct path. 
            if (Score.CurrentScore > PlayerPrefs.GetInt("HighScore", 0)) //this is the condition at which a new highscore is recorded i.e. when the current score is greater than the current high score, the current score becomes the new high score
            {
                PlayerPrefs.SetInt("HighScore", Score.CurrentScore);
                highScore.text = Score.CurrentScore.ToString(); //the new highscore is linked to the HighScore game object in the unity editor, which has a text component accepting the high score value as a string variable.
            }
            
            //below tells the train to stop moving upon reaching the end of the level.
            PlayerController.Stop = true;
            PlayerController.movingLeft = false;
            PlayerController.movingRight = false;
            StartCoroutine(VictoryEnsemble2()); //this plays the victory sound in a coroutine similarly to that at the end of level 1

        }

        IEnumerator VictoryEnsemble2()
        {
            Music.GetComponent<SFX>().Music.Stop();
            Victory.GetComponent<SFX>().Victory.Play();
            yield return new WaitForSeconds(1.5f);
            Time.timeScale = 1;
            SceneManager.LoadScene(4); // upon finishing level 2 the scene with credits is loaded as this is currently the end of the game.
            Player.GetComponent<InventoryManager>().RefreshTracks();
            Music.GetComponent<SFX>().Music.Play();
        }


        //to add each type of next track upon reaching each trigger point of the track currently under the train, according to its type. 
        //Thus there are a lot of combinations, each with their own requirements, specified below:

        if (other.gameObject.name.Contains("straight_track")) //if the train is currently on a straight track, i.e. adding a track to a straight track piece.
        {

            if (nextTrack == "straight" && InventoryManager.straightTracksAvailable >= 1 && other.gameObject.tag.Contains("track")) //if the track piece to be added at this point is another straight track, only add it if there are such pieces available in the inventory. The tag "track" is the default, denoting that the current track under the train is not rotated. 
            {
                addingTrack = true; //this calls the animation of the train adding a track piece to be played. 

                showNow = false; //this specifies the new track piece to appear in time with the adding track animation in accordance with the showTrack() and hideTrack() functions that are written below and called in the animator. 


                newTrack = Instantiate(straight_track, other.transform.position + new Vector3(-0.57f, 0f, 0), other.transform.rotation); // instead of using rotation of player, use rotation of current track to ensure tracks connecting. need to callibrate position in relation to rotated track or use object position instaed of world position.
                InventoryManager.straightTracksAvailable--;
                //the above code for instantiating a track piece is based on the in-class example for isntantiating a bullet. However it is modified to instantiate the position and rotation in relation to the track object under the train rather than the train itself as the new track must be positioned to attach to the current track. 


                hideTrack();

            }

            else if (nextTrack == "straight" && InventoryManager.straightTracksAvailable >= 1 && other.gameObject.tag.Contains("rotated45")) //similar to the above instance except the current straight track below is rotated by 45 degrees, meaning the new track must also be rotated. 
            {
                addingTrack = true;


                showNow = false;

                newTrack = Instantiate(straight_track, other.transform.position + new Vector3(-0.4f, -0.4f, 0), other.transform.rotation); //the rotation of the new track is defined by that of the track it is being added to. The position of the new track is different from when it is not rotated, and was found by trial and error. 
                                                                                                                                           // instead of using rotation of player, use rotation of current track to ensure tracks connect.
                InventoryManager.straightTracksAvailable--;
                newTrack.tag = "rotated45"; //this is so the track following the rotated track is targged as rotated by 45 degrees so any track added to it can be rotated likewise.



                hideTrack();

            }

            else if (nextTrack == "straight" && InventoryManager.straightTracksAvailable >= 1 && other.gameObject.tag.Contains("rotated90"))
            {
                addingTrack = true;


                showNow = false;

                newTrack = Instantiate(straight_track, other.transform.position + new Vector3(0f, -0.55f, 0), other.transform.rotation);
                InventoryManager.straightTracksAvailable--;
                newTrack.tag = "rotated90"; 

                


                hideTrack();

            }

            else if (nextTrack == "straight" && InventoryManager.straightTracksAvailable >= 1 && other.gameObject.tag.Contains("rotated-45"))
            {
                addingTrack = true;

                showNow = false;


                newTrack = Instantiate(straight_track, other.transform.position + new Vector3(-0.4f, 0.4f, 0), other.transform.rotation); 
                InventoryManager.straightTracksAvailable--;
                newTrack.tag = "rotated-45"; 




                hideTrack();

            }

            else if (nextTrack == "up" && InventoryManager.upTracksAvailable >= 1 && other.gameObject.tag.Contains("track")) //this is for adding an upwards track to a non-rotated straight track.
            {
                addingTrack = true;


                showNow = false;

                newTrack = Instantiate(up_track, other.transform.position + new Vector3(1.46f, 0.6f, 0), other.transform.rotation);
                InventoryManager.upTracksAvailable--;
                //a tag for the rotation of the new track here is unecessary is it contains "track" by default.
                hideTrack();

            }

            else if (nextTrack == "up" && InventoryManager.upTracksAvailable >= 1 && other.gameObject.tag.Contains("rotated45"))
            {
                addingTrack = true;

                showNow = false;


                newTrack = Instantiate(up_track, other.transform.position + new Vector3(0.58f, 1.41f, 0), other.transform.rotation);
                InventoryManager.upTracksAvailable--;
                newTrack.tag = "rotated45"; //the rotation of the up track  is the same as that of the straight track it is added to.

                hideTrack();

            }

            else if (nextTrack == "up" && InventoryManager.upTracksAvailable >= 1 && other.gameObject.tag.Contains("rotated90"))
            {
                addingTrack = true;


                showNow = false;

                newTrack = Instantiate(up_track, other.transform.position + new Vector3(-0.6f, 1.5f, 0), Quaternion.Euler(0, 0, 90));

                //ref: https://docs.unity3d.com/ScriptReference/Quaternion.Euler.html manual for Quaternion.Euler for defining the rotation of the track game object. 
                //specifying the angle of the new track using Quaternion.Euler in this instance is interchangeble with using the rotation of the track it is being added to (other.transform.rotation).

                InventoryManager.upTracksAvailable--;
                newTrack.tag = "rotated90";

                hideTrack();

            }

            else if (nextTrack == "up" && InventoryManager.upTracksAvailable >= 1 && other.gameObject.tag.Contains("rotated-45"))
            {
                addingTrack = true;




                newTrack = Instantiate(up_track, other.transform.position + new Vector3(1.5f, -0.6f, 0), other.transform.rotation);
                InventoryManager.upTracksAvailable--;
                newTrack.tag = "rotated-45";

                hideTrack();

            }


            else if (nextTrack == "down" && InventoryManager.downTracksAvailable >= 1 && other.gameObject.tag.Contains("track"))
            {
                addingTrack = true;




                newTrack = Instantiate(down_track, other.transform.position + new Vector3(1.5f, -0.2f, 0), other.transform.rotation);
                InventoryManager.downTracksAvailable--;
                hideTrack();

            }

            else if (nextTrack == "down" && InventoryManager.downTracksAvailable >= 1 && other.gameObject.tag.Contains("rotated45"))
            {
                addingTrack = true;




                newTrack = Instantiate(down_track, other.transform.position + new Vector3(1.2f, 0.9f, 0), other.transform.rotation);
                InventoryManager.downTracksAvailable--;
                newTrack.tag = "rotated45";
                hideTrack();

            }

            else if (nextTrack == "down" && InventoryManager.downTracksAvailable >= 1 && other.gameObject.tag.Contains("rotated90"))
            {
                addingTrack = true;




                newTrack = Instantiate(down_track, other.transform.position + new Vector3(0.2f, 1.48f, 0), other.transform.rotation);
                InventoryManager.downTracksAvailable--;
                newTrack.tag = "rotated90";
                hideTrack();

            }

            else if (nextTrack == "down" && InventoryManager.upTracksAvailable >= 1 && other.gameObject.tag.Contains("rotated-45"))
            {
                addingTrack = true;




                newTrack = Instantiate(down_track, other.transform.position + new Vector3(0.85f, -1.15f, 0), other.transform.rotation);
                InventoryManager.downTracksAvailable--;
                newTrack.tag = "rotated-90";
                hideTrack();

            }

           

            

        }

        else if (other.gameObject.name.Contains("up_track")) //the following instances are for adding track pieces to upwards turning track pieces. 
        {
            if (nextTrack == "straight" && InventoryManager.straightTracksAvailable >= 1 && other.gameObject.tag.Contains("track"))
            {
                addingTrack = true;




                newTrack = Instantiate(straight_track, other.transform.position + new Vector3(-0.3f, -1.1f, 0), Quaternion.Euler(0, 0, 45));  
                //adding a straight track to a non-rotated upwards track, requires rotating the straight track by 45 degrees as this is how much the upwards track turns up. 
                //In this instance, Quaternion.Euler is used to specify the rotation of the new track piece, i.e. using other.transform.rotation would not work here because the rotation changes. 
                InventoryManager.straightTracksAvailable--;
                newTrack.tag = "rotated45"; //the new straight track added to the up track is thus tagged as rotated by 45 degrees for the next track to also be rotated by this amount.


                hideTrack();

            }

            else if (nextTrack == "straight" && InventoryManager.straightTracksAvailable >= 1 && other.gameObject.tag.Contains("rotated45"))
            {
                addingTrack = true;




                newTrack = Instantiate(straight_track, other.transform.position + new Vector3(0.55f, -1f, 0), Quaternion.Euler(0, 0, 90)); //a rotated by 45 degrees up track results in the track pointing 90 degrees upwards, so this is the required rotation for the straight track that is added to it. 
                InventoryManager.straightTracksAvailable--;
                newTrack.tag = "rotated90";

                

                

            }

            else if (nextTrack == "straight" && InventoryManager.straightTracksAvailable >= 1 && other.gameObject.tag.Contains("rotated90"))
            {
                addingTrack = true;




                newTrack = Instantiate(straight_track, other.transform.position + new Vector3(1.1f, -0.3f, 0), Quaternion.Euler(0, 0, 135)); //this is an improbably condition but is included to cover all instances and in cases where the gravity is modified, like when a loopdeloop is attempted.
                InventoryManager.straightTracksAvailable--;
                newTrack.tag = "rotated135";


                hideTrack();



            }

            else if (nextTrack == "straight" && InventoryManager.straightTracksAvailable >= 1 && other.gameObject.tag.Contains("rotated135"))
            {
                addingTrack = true;




                newTrack = Instantiate(straight_track, other.transform.position + new Vector3(1, 0.55f, 0), Quaternion.Euler(0, 0, 180)); //this is essentially an upside down track, not necessarily traversible.
                InventoryManager.straightTracksAvailable--;
                newTrack.tag = "rotated180";


                hideTrack();



            }

            else if (nextTrack == "straight" && InventoryManager.straightTracksAvailable >= 1 && other.gameObject.tag.Contains("rotated-45")) //adding a straight track to an up track that is 45 degrees rotated downwards.
            {
                addingTrack = true;




                newTrack = Instantiate(straight_track, other.transform.position + new Vector3(-1f, -0.55f, 0), Quaternion.Euler(0, 0, 0));  //the resulting track points straight forwards.
                InventoryManager.straightTracksAvailable--;
                newTrack.tag = "track not to hide"; 
                //this is an instance where the tracks get added too fast for them to be hidden and shown with the hideTrack() and showTrack() functions, so this tag is used to bypass them and show the new track piece immediately. 
                //This tag name is important to include the word "track" because this is the word refered to in the default tag of non-rotated track pieces.

               

            }

            else if (nextTrack == "up" && InventoryManager.upTracksAvailable >= 1 && other.gameObject.tag.Contains("track"))
            {
                addingTrack = true;




                newTrack = Instantiate(up_track, other.transform.position + new Vector3(0.7f, 0.77f, 0), Quaternion.Euler(0, 0, 45)); //adding an up track to a non-rotated up track must be added at 45 degrees.
                InventoryManager.upTracksAvailable--;
                newTrack.tag = "rotated45";
                hideTrack();

            }

            else if (nextTrack == "up" && InventoryManager.upTracksAvailable >= 1 && other.gameObject.tag.Contains("rotated45"))
            {
                addingTrack = true;




                newTrack = Instantiate(up_track, other.transform.position + new Vector3(-0.05f, 1f, 0), Quaternion.Euler(0, 0, 90)); 
                InventoryManager.upTracksAvailable--;
                newTrack.tag = "rotated90";
              
                

            }

            else if (nextTrack == "up" && InventoryManager.upTracksAvailable >= 1 && other.gameObject.tag.Contains("rotated90"))
            {
                addingTrack = true;




                newTrack = Instantiate(up_track, other.transform.position + new Vector3(-0.75f, 0.7f, 0), Quaternion.Euler(0, 0, 135)); //this improbable situation suggest a loopdeloop
                InventoryManager.upTracksAvailable--;
                newTrack.tag = "rotated135";
                hideTrack();



            }

            else if (nextTrack == "up" && InventoryManager.upTracksAvailable >= 1 && other.gameObject.tag.Contains("rotated135"))
            {
                addingTrack = true;




                newTrack = Instantiate(up_track, other.transform.position + new Vector3(-1.1f, -0.05f, 0), Quaternion.Euler(0, 0, 180));
                InventoryManager.upTracksAvailable--;
                newTrack.tag = "rotated180";
                hideTrack();



            }

            else if (nextTrack == "up" && InventoryManager.upTracksAvailable >= 1 && other.gameObject.tag.Contains("rotated180"))
            {
                addingTrack = true;




                newTrack = Instantiate(up_track, other.transform.position + new Vector3(-0.75f, -0.8f, 0), Quaternion.Euler(0, 0, 225));
                InventoryManager.upTracksAvailable--;
                newTrack.tag = "rotated225";
                hideTrack();



            }

            else if (nextTrack == "up" && InventoryManager.upTracksAvailable >= 1 && other.gameObject.tag.Contains("rotated225"))
            {
                addingTrack = true;




                newTrack = Instantiate(up_track, other.transform.position + new Vector3(0.05f, -1.05f, 0), Quaternion.Euler(0, 0, 270));
                InventoryManager.upTracksAvailable--;
                newTrack.tag = "rotated270";
                hideTrack();



            }

            else if (nextTrack == "up" && InventoryManager.upTracksAvailable >= 1 && other.gameObject.tag.Contains("rotated270"))
            {
                addingTrack = true;




                newTrack = Instantiate(up_track, other.transform.position + new Vector3(0.85f, -0.7f, 0), Quaternion.Euler(0, 0, 315));
                InventoryManager.upTracksAvailable--;
                newTrack.tag = "rotated-45";
              //this track represents the last track piece of a loop de loop, as the next track piece will be 360 degrees rotated i.e. not rotated at all.
                hideTrack();



            }

            else if (nextTrack == "up" && InventoryManager.upTracksAvailable >= 1 && other.gameObject.tag.Contains("rotated-45"))
            {
                addingTrack = true;

                showNow = true;


                newTrack = Instantiate(up_track, other.transform.position + new Vector3(1.05f, 0.05f, 0), Quaternion.Euler(0, 0, 0)); 
                InventoryManager.upTracksAvailable--;
                newTrack.tag = "track";
                

            }

            else if (nextTrack == "down" && InventoryManager.upTracksAvailable >= 1 && other.gameObject.tag.Contains("track"))
            {
                addingTrack = true;




                newTrack = Instantiate(down_track, other.transform.position + new Vector3(1.33f, 0.24f, 0), Quaternion.Euler(0, 0, 45));
                InventoryManager.downTracksAvailable--;
                newTrack.tag = "rotated45";
                hideTrack();

            }

            else if (nextTrack == "down" && InventoryManager.upTracksAvailable >= 1 && other.gameObject.tag.Contains("rotated45"))
            {
                addingTrack = true;




                newTrack = Instantiate(down_track, other.transform.position + new Vector3(0.79f, 1.04f, 0), Quaternion.Euler(0, 0, 90));
                InventoryManager.downTracksAvailable--;
                newTrack.tag = "rotated90";
                hideTrack();

            }

           

            else if (nextTrack == "down" && InventoryManager.upTracksAvailable >= 1 && other.gameObject.tag.Contains("rotated-45"))
            {
                addingTrack = true;




                newTrack = Instantiate(down_track, other.transform.position + new Vector3(1.05f, -0.75f, 0), Quaternion.Euler(0, 0, 0));
                InventoryManager.downTracksAvailable--;
                newTrack.tag = "track";
                hideTrack();

            }



        }

        else if (other.gameObject.name.Contains("down_track"))
        {
            if (nextTrack == "straight" && InventoryManager.straightTracksAvailable >= 1 && other.gameObject.tag.Contains("track"))
            {
                addingTrack = true;




                newTrack = Instantiate(straight_track, other.transform.position + new Vector3(-0.19f, 0.68f, 0), Quaternion.Euler(0, 0, -45));
                InventoryManager.straightTracksAvailable--;
                newTrack.tag = "rotated-45";


                hideTrack();

            }

            else if (nextTrack == "straight" && InventoryManager.straightTracksAvailable >= 1 && other.gameObject.tag.Contains("rotated45"))
            {
                addingTrack = true;




                newTrack = Instantiate(straight_track, other.transform.position + new Vector3(-0.6f, 0.32f, 0), Quaternion.Euler(0, 0, 0)); 
                InventoryManager.straightTracksAvailable--;
                newTrack.tag = "track";


                hideTrack();

            }

            else if (nextTrack == "straight" && InventoryManager.straightTracksAvailable >= 1 && other.gameObject.tag.Contains("rotated90"))
            {
                addingTrack = true;




                newTrack = Instantiate(straight_track, other.transform.position + new Vector3(-0.65f, -0.2f, 0), Quaternion.Euler(0, 0, 45)); 
                InventoryManager.straightTracksAvailable--;
                newTrack.tag = "rotated45";


                hideTrack();

            }



            else if (nextTrack == "straight" && InventoryManager.straightTracksAvailable >= 1 && other.gameObject.tag.Contains("rotated-45"))
            {
                addingTrack = true;




                newTrack = Instantiate(straight_track, other.transform.position + new Vector3(0.32f, 0.61f, 0), Quaternion.Euler(0, 0, -90f)); // instead of using rotation of player, use rotation of current track to ensure tracks connecting
                InventoryManager.straightTracksAvailable--;
                newTrack.tag = "rotated-90";


                hideTrack();

            }

            else if (nextTrack == "up" && InventoryManager.upTracksAvailable >= 1 && other.gameObject.tag.Contains("track"))
            {
                addingTrack = true;




                newTrack = Instantiate(up_track, other.transform.position + new Vector3(1.68f, -0.34f, 0), Quaternion.Euler(0, 0, -45f)); // instead of using rotation of player, use rotation of current track to ensure tracks connecting
                InventoryManager.upTracksAvailable--;
                newTrack.tag = "rotated-45";


                hideTrack();

            }

            else if (nextTrack == "up" && InventoryManager.upTracksAvailable >= 1 && other.gameObject.tag.Contains("rotated45"))
            {
                addingTrack = true;




                newTrack = Instantiate(up_track, other.transform.position + new Vector3(1.45f, 0.9f, 0), Quaternion.Euler(0, 0, 0)); // instead of using rotation of player, use rotation of current track to ensure tracks connecting
                InventoryManager.upTracksAvailable--;
                newTrack.tag = "track";


                hideTrack();

            }

            else if (nextTrack == "up" && InventoryManager.upTracksAvailable >= 1 && other.gameObject.tag.Contains("rotated-45"))
            {
                addingTrack = true;




                newTrack = Instantiate(up_track, other.transform.position + new Vector3(0.95f, -1.45f, 0), Quaternion.Euler(0, 0, -90)); // instead of using rotation of player, use rotation of current track to ensure tracks connecting
                InventoryManager.upTracksAvailable--;
                newTrack.tag = "rotated-90";


                hideTrack();

            }

            else if (nextTrack == "down" && InventoryManager.upTracksAvailable >= 1 && other.gameObject.tag.Contains("track"))
            {
                addingTrack = true;




                newTrack = Instantiate(down_track, other.transform.position + new Vector3(1.1f, -0.95f, 0), Quaternion.Euler(0, 0, -45f));
                InventoryManager.downTracksAvailable--;
                newTrack.tag = "rotated-45";
                hideTrack();

            }

            else if (nextTrack == "down" && InventoryManager.upTracksAvailable >= 1 && other.gameObject.tag.Contains("rotated45"))
            {
                addingTrack = true;




                newTrack = Instantiate(down_track, other.transform.position + new Vector3(1.45f, 0.1f, 0), Quaternion.Euler(0, 0, 0));
                InventoryManager.downTracksAvailable--;
                newTrack.tag = "track";
                hideTrack();

            }

            else if (nextTrack == "down" && InventoryManager.upTracksAvailable >= 1 && other.gameObject.tag.Contains("rotated90"))
            {
                addingTrack = true;




                newTrack = Instantiate(down_track, other.transform.position + new Vector3(0.95f, 1.13f, 0), Quaternion.Euler(0, 0, 45));
                InventoryManager.downTracksAvailable--;
                newTrack.tag = "rotated45";

              
                hideTrack();

            }

            else if (nextTrack == "down" && InventoryManager.upTracksAvailable >= 1 && other.gameObject.tag.Contains("rotated-45"))
            {
                addingTrack = true;




                newTrack = Instantiate(down_track, other.transform.position + new Vector3(0.1f, -1.4f, 0), Quaternion.Euler(0, 0, -90f));
                InventoryManager.downTracksAvailable--;
                newTrack.tag = "rotated-90";
                hideTrack();

            }

           

        }

      


    }

   


    public void animationAddingToMoving() //this function gets called in the train_front animation of Animation_Adding, at the end in order to ensure this animation doesn't repeat until a new track is being added.
    {
        
        addingTrack = false;

    }


    //the following two functions get called during the addingTrack animation via the animation controller interface in unity. 
    //This is to control the moment the track being added appears. It coincides with the animation of the track being added. The track is added before the track is revealed to not risk the train falling off the track because it is too fast. 
    //ref: for animation events the DES105 lectorial for animation presented the possibility of this within unity and suggested using the variable newTrack.GetComponent<Renderer>().enabled to control the track's visibility.
public void hideTrack() //this function is called at the start of the adding track animation before the new track can be seen.
    {
        if (TrackForce.onVertical == false && TrackForce.onInverted == false && TrackForce.onSteepDown == false &&TrackForce.onAssistedDesc == false && newTrack.tag != "track not to hide"  && showNow == false) //these are instances in which the temporary hiding of the track should be bypassed, otherwise it is not shown at all.
        {
            newTrack.GetComponent<Renderer>().enabled = false;
        }
    }

public void showTrack() //this function is called at the end of the adding track animation on the frame at which the new track is being added to the track currently underneath the train_front.
    {
      
        newTrack.GetComponent<Renderer>().enabled = true;  
    }


   


   
}

