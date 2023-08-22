﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;
using Abertay.Analytics;
using UnityEngine.Rendering.PostProcessing;
public class UIButtonManager : MonoBehaviour
{
    public GameObject pauseMenu; //to access the pause menu's buttons
    public GameObject Player; //to call functions from the player's inventory manager script and refresh the tracks available when loading a level.
    private PlayerController playerController; //to access the player's movement and stop it when starting a level.
    public UISpriteAnimation spriteAnim;
    private InventoryManager inventoryManager; //to reset the tracks available when loading levels.
    private TrackForce trackForce; //included here and below to eliminate any additional forces that were present at the moment of pressing restart or pause for the next scene that is loaded.
    private TrackForceCargo trackForceCargo; //included here and below to eliminate prior additional forces onto the cargo carriage upon loading a scene.
    private TrackForcePassenger trackForcePassenger; //included here and below to eliminate prior additional forces onto the passenger carriage upon loading a scene.
    private Score score; //this is to access the CrateScore and nullify it when restarting a level to prevent players from cheating by gathering crates and restarting in a loop of endless point accumulation before completing the levels.
    public static bool StrBut = false;
    public static bool UpBut = false;
    public static bool DowBut = false;
    public static bool StaBut = false;
    public static bool PauBut = true;
    public static bool BooBut = false;
    public static bool MenuDirect = false;
    private bool TuteOn = true;
    private string playerName;
    private bool Accepted = false;
    private bool Declined = false;
    public static int Restarts = 0;
    public GameObject TutePrompt1;
    public GameObject TutePrompt2;
    public GameObject TutePrompt3;
    public GameObject TutePrompt4;
    public GameObject TutePrompt5;
    public GameObject TutePrompt6;
    public GameObject TutePrompt7;
    public GameObject TutePrompt8;
    public GameObject TutePrompt9;
    public GameObject TutePromptGoal;
    public GameObject TutePromptFinal;
    public GameObject TutePromptCrate;
    public GameObject Shade;
    public GameObject Waiver;
    public GameObject Nickname;
    public GameObject ChooseHandedness;
    public GameObject NoInput;
    public GameObject Train;
    public static bool TutorialRight = true;
    public PostProcessVolume ppVol;
   // private DepthOfField DoF;
   // public Camera mainCamera;
   
    
    private void Start()
    {
      //  mainCamera = Camera.main;
        

         if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            if(MenuDirect)
            {
                if(Waiver != null)
                Waiver.SetActive(false);
                if(Shade != null)
                Shade.SetActive(false);
                if(Nickname != null)
                Nickname.SetActive(false);
            }
        }
    }
    public void StraightTrackButtonClicked()
    {
     
       
            StrBut = true;
            UpBut = false;
            DowBut = false;
        PauBut = false;
        
    }


 

    public void RightHandedOption()
    {


        Dictionary<string, object> HandedParameters = new Dictionary<string, object>()
            {

                {"playerName", PlayerPrefs.GetString("nickname")},
            {"handedness", "rightHanded" }
            };

        AnalyticsManager.SendCustomEvent("Handedness", HandedParameters);

        if (SceneManager.GetActiveScene().buildIndex == 1 || SceneManager.GetActiveScene().buildIndex == 11)
        { PauBut = true;
            TutePrompt2.SetActive(false);
            TutePrompt3.SetActive(false);
            TutePrompt4.SetActive(false);
            TutePrompt5.SetActive(false);
            TutePrompt6.SetActive(false);
            TutePrompt7.SetActive(false);
            TutePrompt8.SetActive(false);
        }

        if (PlayerPrefs.GetInt("hand") != 1)
        PlayerPrefs.SetInt("hand", 1);
        if (SceneManager.GetActiveScene().buildIndex == 11)
        {
          
            SceneManager.LoadScene(1);
          

            ResetVariables();
            ChooseHandedness.SetActive(false);
           
        }
        ChooseHandedness.SetActive(false);
        Time.timeScale = 1;
    }
    public void LeftHandedOption()
    {

        Dictionary<string, object> HandedParameters = new Dictionary<string, object>()
            {

                {"playerName", PlayerPrefs.GetString("nickname")},
            {"handedness", "leftHanded" }
            };

        AnalyticsManager.SendCustomEvent("Handedness", HandedParameters);

        if (SceneManager.GetActiveScene().buildIndex == 1 || SceneManager.GetActiveScene().buildIndex == 11)
        {
            TutePrompt2.SetActive(false);
            TutePrompt3.SetActive(false);
            TutePrompt4.SetActive(false);
            TutePrompt5.SetActive(false);
            TutePrompt6.SetActive(false);
            TutePrompt7.SetActive(false);
            TutePrompt8.SetActive(false);
        }
        if (PlayerPrefs.GetInt("hand") != 2)
            PlayerPrefs.SetInt("hand", 2);
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
         
            SceneManager.LoadScene(11);
            PauBut = true;
            
            ResetVariables();
           
        }
        ChooseHandedness.SetActive(false);
        Time.timeScale = 1;
    }

    public void OptionsButton()
    {
        ChooseHandedness.SetActive(true);
        if(SceneManager.GetActiveScene().buildIndex != 0)
        pauseMenu.SetActive(false);
    }

    public void ReadStringInput(string input)
    {
        playerName = input;
        
        PlayerPrefs.SetString("nickname", playerName);
        
        print(PlayerPrefs.GetString("nickname"));
    }

   void OnAcceptedTerms(bool Accepted)
    {
        Dictionary<string, object> parameters = new Dictionary<string, object>()
        {
            { "Accepted", Accepted}
        };
      
        AnalyticsManager.SendCustomEvent("waiverResponse", parameters);
    }


    void OnDeclinedTerms(bool Declined)
    {
        Dictionary<string, object> parameters = new Dictionary<string, object>()
        {
            { "Declined", Declined}
        };
      //  Analytics.CustomEvent("waiverResponse", parameters);
      AnalyticsManager.SendCustomEvent("waiverResponse", parameters);

    }

   

    public void AcceptButton()
    {
        
        Waiver.SetActive(false);
        Nickname.SetActive(true);
        OnAcceptedTerms(true);
    }

    public void DeclineButton()
    {
        Application.Quit();
        OnDeclinedTerms(true);
    }

    public void DoneButton()
    {
       
        Shade.SetActive(false);
        Nickname.SetActive(false);
    }

    public void StartButtonClicked()
    {
        if (StartPauseButtonManager.ButtonIsStart)
        {

           /* StrBut = true;
            UpBut = false;
            DowBut = false; */
            PauBut = false;
            if (!DowBut && !UpBut)
                StrBut = true;
        }
        else
        {
            PauBut = true;
          /*  StrBut = false;
            UpBut = false;
            DowBut = false; */
        }

    }

      
    public void BoostButtonClicked()
    {
        BooBut = true;
        if (SceneManager.GetActiveScene().buildIndex == 1 || SceneManager.GetActiveScene().buildIndex == 11)
        {
            StartCoroutine(CheckBoost());
        }
    }

    public void UpTrackButtonClicked()
    {
        StrBut = false;
        UpBut = true;
        DowBut = false;
       // PauBut = false;
    }

   

    public void DownTrackButtonClicked()
    {
        StrBut = false;
        UpBut = false;
        DowBut = true;
       // PauBut = false;
    }


    public void NoButtonClicked()
    {
       /* StrBut = false;
        UpBut = false;
        DowBut = false;
        */
    }

    public void BoostButtonUnclicked()
    {
        BooBut = false;
       
    }

    IEnumerator CheckBoost()
    {
        yield return new WaitForSeconds(1f);
        if (BooBut)
            ClosePrompt6();
    }
    public void ResumeIsClicked()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        //this function is called by the resume button in unity editor. It ensure the pause menu is deactivated and time is running upon clicking resume.
    }

    public void PauseButtonClicked()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void StartisClicked()
    {
        SceneManager.LoadScene(1);

        //ref: using the manual and material covered in the DES105 lecture on 'UI & Menus' (https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.html) 
        
        Time.timeScale = 1;//this is to ensure the time is running when the gameplay starts.
    }

    public void ExitButtonClicked() 
    { 
        Application.Quit(); 
        //Application.Quit was found in the Unity Manual, demonstrated here: https://docs.unity3d.com/ScriptReference/Application.Quit.html
        //this function is called upon clicking on the exit button in the main menu and causes the application to close when running a build, or to display "quit is pressed" if running in the unity editor.
    }


    public void Restarted(string situation)
    {
        Dictionary<string, object> restartParameters = new Dictionary<string, object>()
            {
                {"level", SceneManager.GetActiveScene().buildIndex},
                {"playerName", PlayerPrefs.GetString("nickname")},
                {"time", Timer.currentTime},
            {"situation", situation},
            //{"medal", PlayerController.medal},
                {"position", Mathf.RoundToInt(transform.position.x / 5f)}

            };
        AnalyticsManager.SendCustomEvent("Restarts", restartParameters);
    }



    public void TutorialSkipped(bool skip)
    {
        Dictionary<string, object> tutorialParameters = new Dictionary<string, object>()
            {
               
                {"playerName", PlayerPrefs.GetString("nickname")},
            {"skip", skip },

            };
        AnalyticsManager.SendCustomEvent("TutorialResponse", tutorialParameters);
    }

    public void PromptClosed(int prompt)
    {
        Dictionary<string, object> promptParameters = new Dictionary<string, object>()
            {

                {"playerName", PlayerPrefs.GetString("nickname")},
            {"promptNumber", prompt },
            {"closedPrompt", true}

            };
        AnalyticsManager.SendCustomEvent("PromptClosed", promptParameters);
    }

    public void RestartButtonClicked()
    {
        Restarted("completion");

       

        PlayerController.Stop = true;
        PlayerController.movingLeft = false;
        PlayerController.movingRight = false;
        Timer.stop = false;
        Timer.currentTime = 0f;
        PlayerController.camDown = false;
        PauBut = true;
        //the above is to reinitialise the player movement so that the player can move after restart.
        if (SceneManager.GetActiveScene().buildIndex == 1 || SceneManager.GetActiveScene().buildIndex == 11)
        {
            SceneManager.LoadScene(12);
            ResetVariables();
        }
        else 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //the above line of code reloads the current scene, which I learnt from: https://www.youtube.com/watch?v=ZmjYw8Z51mg.      
        ResetVariables();
        //this function, that is coded at the bottom of this script, resets the tracks available, eliminates additional forces applied from the TrackForce scripts, and sets the Time.time to 1 i.e. the time is ensured to be running.




    }

    public void RestartButtonClickedDuringLevel()
    {
        

        Restarts += 1;
        Restarted("during level");
        PlayerController.Stop = true;
        PlayerController.movingLeft = false;
        PlayerController.movingRight = false;
        Timer.stop = false;
        Timer.currentTime = 0f;
        PlayerController.camDown = false;
        PauBut = true;
        //the above is to reinitialise the player movement so that the player can move after restart.
        if (SceneManager.GetActiveScene().buildIndex == 1 || SceneManager.GetActiveScene().buildIndex == 11)
        {
            SceneManager.LoadScene(12);
            ResetVariables();
        }
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //the above line of code reloads the current scene, which I learnt from: https://www.youtube.com/watch?v=ZmjYw8Z51mg.      
        ResetVariables();
        //this function, that is coded at the bottom of this script, resets the tracks available, eliminates additional forces applied from the TrackForce scripts, and sets the Time.time to 1 i.e. the time is ensured to be running.




    }

    public void RestartButtonOnDeath()
    {
        Restarts += 1;
        Restarted("on death");
        PlayerController.Stop = true;
        PlayerController.movingLeft = false;
        PlayerController.movingRight = false;
        Timer.stop = false;
        Timer.currentTime = 0f;
        PlayerController.camDown = false;
        PauBut = true;
        //the above is to reinitialise the player movement so that the player can move after restart.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //the above line of code reloads the current scene, which I learnt from: https://www.youtube.com/watch?v=ZmjYw8Z51mg.      
        ResetVariables();
        //this function, that is coded at the bottom of this script, resets the tracks available, eliminates additional forces applied from the TrackForce scripts, and sets the Time.time to 1 i.e. the time is ensured to be running.


    }

    public void MainMenuButtonClicked()
    {
        PlayerController.Stop = true;
        PlayerController.movingLeft = false;
        PlayerController.movingRight = false;
        MenuDirect = true;
        //the above ensure the train is not moving when the main menu is opened.
        SceneManager.LoadScene(0); //this ensures that time in the game does not pass when the main menu is open.
        
    }


    public void LoadButtonClicked()
    {
        SceneManager.LoadScene(9);
        //this function is activated upon clicking onto the levels panel at the bottom left of the main menu. It loads the scene that displays each level from where they can be loaded. 
    }

    public void StartTutorialButton()
    {
        TutorialSkipped(false);
        TutePrompt1.SetActive(false);
        TuteOn = true;
        TutePrompt2.SetActive(true);
    }

    public void SkipTutorialButton()
    {
        TutorialSkipped(true); 
        TuteOn = false;
        TutePrompt1.SetActive(false);
        SceneManager.LoadScene(2);
        //this function is called when the level 1 button is clicked from within the levels menu. It thus loads level one and resets the variables. 
        ResetVariables();

    }

    public void ClosePrompt3()
    {
        PromptClosed(3);
        Time.timeScale = 1;
        TutePrompt3.SetActive(false);
        TutePrompt4.SetActive(true);
        AudioListener.pause = false;
    }

    public void ClosePrompt4()
    {
        PromptClosed(4);
        TutePrompt4.SetActive(false);
        StartCoroutine(DownPrompt());
        
    }

    IEnumerator DownPrompt()
    {
        yield return new WaitForSeconds(1f);
        TutePrompt5.SetActive(true);
    }

    public void ClosePrompt2()
    {
        if (TutePrompt2.activeSelf)
        {
            PromptClosed(2);
            TutePrompt2.SetActive(false);
           
            NoInput.SetActive(true);
            StartCoroutine(TuteGoal());
        }
    }

    IEnumerator TuteGoal()
    {
        yield return new WaitForSeconds(1);
        TutePromptGoal.SetActive(true);
        PauBut = true;
        spriteAnim.Func_PlayUIAnim();
        DepthOfField dph;
        if(ppVol.profile.TryGetSettings<DepthOfField>(out dph))
        {
            dph.active = true;
        }
       // Time.timeScale = 0;
        AudioListener.pause = true;
    }

    public void ClosePrompt5()
    {
        PromptClosed(5);
        TutePrompt5.SetActive(false);
        Invoke("OnPrompt6", 1f);
       
    }

    void OnPrompt6()
    {
        TutePrompt6.SetActive(true);
    }

    public void ClosePrompt6()
    {
        PromptClosed(6);
        TutePrompt6.SetActive(false);
        TutePrompt7.SetActive(true);
    }

    public void ClosePromptGoal()
    {
        TutePromptGoal.SetActive(false);
        TutePromptCrate.SetActive(true);
        spriteAnim.Func_StopUIAnim();
        DepthOfField dph;
        if (ppVol.profile.TryGetSettings<DepthOfField>(out dph))
        {
            dph.active = false;
        }
        PauBut = false;
        //Time.timeScale = 1;
        AudioListener.pause = false;
    }
    public void Level1MenuButtonClicked()
    {
        SceneManager.LoadScene(1);
        //this function is called when the level 1 button is clicked from within the levels menu. It thus loads level one and resets the variables. 
        ResetVariables();
        Restarts = 0;
        PlayerController.levelDeaths = 0;
    }

    public void Level2MenuButtonClicked()
    {
        SceneManager.LoadScene(2);
        Timer.currentTime = 0;
        //this function is called when the level 1 button is clicked from within the levels menu. It thus loads level one and resets the variables. 
        ResetVariables();
        Restarts = 0;
        PlayerController.levelDeaths = 0;
    }

    public void Level3MenuButtonClicked()
    {
        SceneManager.LoadScene(3);
        Timer.currentTime = 0;
        //this function is called when the level 1 button is clicked from within the levels menu. It thus loads level one and resets the variables. 
        ResetVariables();
        Restarts = 0;
        PlayerController.levelDeaths = 0;
    }

    public void Level4MenuButtonClicked()
    {
        SceneManager.LoadScene(4);
        Timer.currentTime = 0;
        //this function is called when the level 1 button is clicked from within the levels menu. It thus loads level one and resets the variables. 
        ResetVariables();
        Restarts = 0;
        PlayerController.levelDeaths = 0;
    }

    public void Level5MenuButtonClicked()
    {
        SceneManager.LoadScene(5);
        Timer.currentTime = 0;
        //this function is called when the level 1 button is clicked from within the levels menu. It thus loads level one and resets the variables. 
        ResetVariables();
        Restarts = 0;
        PlayerController.levelDeaths = 0;
    }

    public void Level6MenuButtonClicked()
    {
        SceneManager.LoadScene(6);
        Timer.currentTime = 0;
        //this function is called when the level 1 button is clicked from within the levels menu. It thus loads level one and resets the variables. 
        ResetVariables();
        Restarts = 0;
        PlayerController.levelDeaths = 0;
    }

    public void Level7MenuButtonClicked()
    {
        SceneManager.LoadScene(7);
        Timer.currentTime = 0;
        //this function is called when the level 1 button is clicked from within the levels menu. It thus loads level one and resets the variables. 
        ResetVariables();
        Restarts = 0;
        PlayerController.levelDeaths = 0;
    }
    public void Level1ButtonClicked()
    {
        SceneManager.LoadScene(2);
        //this function is called when the level 1 button is clicked from within the levels menu. It thus loads level one and resets the variables. 
        ResetVariables();
        Restarts = 0;
        PlayerController.levelDeaths = 0;
    }

    public void Level2ButtonClicked()
    {
        
        SceneManager.LoadScene(4);
        //this function is called when the level 2 button is clicked from within the levels menu. It thus loads level two and resets the variables. 
        //Score.BaseScore = Score.BaseScore + (int)Time.time; //when starting from level 2 the score is reset to 100. However greater scores can be accumulated by succesfully completing level 1 in the same go.
        ResetVariables();
        Restarts = 0;
        PlayerController.levelDeaths = 0;
    }

    public void Level3ButtonClicked()
    {

        SceneManager.LoadScene(5);
        //this function is called when the level 2 button is clicked from within the levels menu. It thus loads level two and resets the variables. 
        //Score.BaseScore = Score.BaseScore + (int)Time.time; //when starting from level 2 the score is reset to 100. However greater scores can be accumulated by succesfully completing level 1 in the same go.
        ResetVariables();
        Restarts = 0;
        PlayerController.levelDeaths = 0;
    }

    public void Level4ButtonClicked()
    {

        SceneManager.LoadScene(3);
        //this function is called when the level 2 button is clicked from within the levels menu. It thus loads level two and resets the variables. 
        //Score.BaseScore = Score.BaseScore + (int)Time.time; //when starting from level 2 the score is reset to 100. However greater scores can be accumulated by succesfully completing level 1 in the same go.
        ResetVariables();
        Restarts = 0;
        PlayerController.levelDeaths = 0;
    }

    public void Level5ButtonClicked()
    {

        SceneManager.LoadScene(6);
        //this function is called when the level 2 button is clicked from within the levels menu. It thus loads level two and resets the variables. 
        //Score.BaseScore = Score.BaseScore + (int)Time.time; //when starting from level 2 the score is reset to 100. However greater scores can be accumulated by succesfully completing level 1 in the same go.
        ResetVariables();
        Restarts = 0;
        PlayerController.levelDeaths = 0;
    }

    public void Level6ButtonClicked()
    {

        SceneManager.LoadScene(9);
        //this function is called when the level 2 button is clicked from within the levels menu. It thus loads level two and resets the variables. 
        //Score.BaseScore = Score.BaseScore + (int)Time.time; //when starting from level 2 the score is reset to 100. However greater scores can be accumulated by succesfully completing level 1 in the same go.
        ResetVariables();
        Restarts = 0;
        PlayerController.levelDeaths = 0;
    }

    public void Level7ButtonClicked()
    {

        SceneManager.LoadScene(7);
        //this function is called when the level 2 button is clicked from within the levels menu. It thus loads level two and resets the variables. 
        //Score.BaseScore = Score.BaseScore + (int)Time.time; //when starting from level 2 the score is reset to 100. However greater scores can be accumulated by succesfully completing level 1 in the same go.
        ResetVariables();
        Restarts = 0;
        PlayerController.levelDeaths = 0;
    }

    public void SaltyButtonClicked()
    {
        SceneManager.LoadScene(6); //credits scene.
       
    }

    public void ResetVariables() //the variables that get rest are refactored into this function which is called at multiple instances above.
    {
        
        Score.CrateScore = 0; //this is to prevent players from cheating by gathering crates and restarting to accumulate extra points.
        Time.timeScale = 1;
        if (Player != null)
        Player.GetComponent<InventoryManager>().RefreshTracks(); //ref: https://forum.unity.com/threads/calling-function-from-other-scripts-c.57072/ for calling a function from another script.
        Timer.currentTime = 0f;
        Timer.stop = false;

        PlayerController.camUp = false;
        PlayerController.camDown = false;
        PlayerController.camCent = false;
        PlayerController.camCentOpp = false;
        PlayerController.flipped = false;


        PlayerController.Stop = true;
        PlayerController.movingLeft = false;
        PlayerController.movingRight = false;

        StrBut = false;
        UpBut = false;
        DowBut = false;
        PauBut = true;

        PlayerController.nextTrack = "none";

        TrackForce.onVertical = false;
        TrackForce.onInverted = false;
        TrackForceCargo.onVertical = false;
        TrackForceCargo.onInverted = false;
        TrackForcePassenger.onVertical = false;
        TrackForcePassenger.onInverted = false;

        if(SceneManager.GetActiveScene().buildIndex == 1 | SceneManager.GetActiveScene().buildIndex == 11)
        {
            TutePrompt2.SetActive(false);
            TutePrompt3.SetActive(false);
            TutePrompt4.SetActive(false);
            TutePrompt5.SetActive(false);
            TutePrompt6.SetActive(false);
            TutePrompt7.SetActive(false);
            TutePrompt8.SetActive(false);
            ChooseHandedness.SetActive(false);
        }

    

    }

}

