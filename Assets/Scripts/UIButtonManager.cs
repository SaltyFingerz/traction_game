using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIButtonManager : MonoBehaviour
{
    public GameObject pauseMenu; //to access the pause menu's buttons
    public GameObject Player; //to call functions from the player's inventory manager script and refresh the tracks available when loading a level.
    private PlayerController playerController; //to access the player's movement and stop it when starting a level.
    private InventoryManager inventoryManager; //to reset the tracks available when loading levels.
    private TrackForce trackForce; //included here and below to eliminate any additional forces that were present at the moment of pressing restart or pause for the next scene that is loaded.
    private TrackForceCargo trackForceCargo; //included here and below to eliminate prior additional forces onto the cargo carriage upon loading a scene.
    private TrackForcePassenger trackForcePassenger; //included here and below to eliminate prior additional forces onto the passenger carriage upon loading a scene.
    private Score score; //this is to access the CrateScore and nullify it when restarting a level to prevent players from cheating by gathering crates and restarting in a loop of endless point accumulation before completing the levels.


   

    public void ResumeIsClicked()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        //this function is called by the resume button in unity editor. It ensure the pause menu is deactivated and time is running upon clicking resume.
    }

    public void StartIsClicked()
    {
        SceneManager.LoadScene(1);
        //ref: using the manual and material covered in the DES105 lecture on 'UI & Menus' (https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.html) 
        
        Time.timeScale = 1;//this is to ensure the time is running when the gameplay starts.
    }

    public void ExitButtonClicked() 
    { 
        Application.Quit(); print("quit pressed");
        //Application.Quit was found in the Unity Manual, demonstrated here: https://docs.unity3d.com/ScriptReference/Application.Quit.html
        //this function is called upon clicking on the exit button in the main menu and causes the application to close when running a build, or to display "quit is pressed" if running in the unity editor.
    }

    public void RestartButtonClicked()
    {
        PlayerController.Stop = true;
        PlayerController.movingLeft = false;
        PlayerController.movingRight = false;
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
        //the above ensure the train is not moving when the main menu is opened.
        SceneManager.LoadScene(0); //this ensures that time in the game does not pass when the main menu is open.
        
    }

    public void LoadButtonClicked()
    {
        SceneManager.LoadScene(3);
        //this function is activated upon clicking onto the levels panel at the bottom left of the main menu. It loads the scene that displays each level from where they can be loaded. 
    }

    public void Level1ButtonClicked()
    {
        SceneManager.LoadScene(1);
        //this function is called when the level 1 button is clicked from within the levels menu. It thus loads level one and resets the variables. 
        ResetVariables();
    }

    public void Level2ButtonClicked()
    {
        
        SceneManager.LoadScene(2);
        //this function is called when the level 2 button is clicked from within the levels menu. It thus loads level two and resets the variables. 
        //Score.BaseScore = Score.BaseScore + (int)Time.time; //when starting from level 2 the score is reset to 100. However greater scores can be accumulated by succesfully completing level 1 in the same go.
        ResetVariables();
    }

    public void SaltyButtonClicked()
    {
        SceneManager.LoadScene(4); //credits scene.
       
    }

    public void ResetVariables() //the variables that get rest are refactored into this function which is called at multiple instances above.
    {
        Score.CrateScore = 0; //this is to prevent players from cheating by gathering crates and restarting to accumulate extra points.
        Time.timeScale = 1;
        Player.GetComponent<InventoryManager>().RefreshTracks(); //ref: https://forum.unity.com/threads/calling-function-from-other-scripts-c.57072/ for calling a function from another script.
        TrackForce.onVertical = false;
        TrackForce.onInverted = false;
        TrackForceCargo.onVertical = false;
        TrackForceCargo.onInverted = false;
        TrackForcePassenger.onVertical = false;
        TrackForcePassenger.onInverted = false;
    }

}

