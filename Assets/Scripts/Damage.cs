using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;
using Abertay.Analytics;

public class Damage : MonoBehaviour
{
    public GameObject Player; //assigning a player game object for calling a function in one of its scripts
    public ParticleSystem blood; //to link to the blood particle system
    public GameObject OhNo;//to link to the AudioSource of the 'Oh No' sound
    //the scripts are included below for accessing their public variables
    private PlayerController playerController;
    private InventoryManager inventoryManager;
    private UIButtonManager uIButtonManager;
    private TrackForce trackForce;
    private TrackForceCargo trackForceCargo;
    private TrackForcePassenger trackForcePassenger;
    // Start is called before the first frame update


    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.name.Contains("level")) //when the player collides with the raw terrain rather than a track piece they lose.
        {
            
            blood.Play();
            //ref: https://www.youtube.com/watch?v=0NCTAuP3BgU for the basics of playing a particle effect upon collision. This was adapted for a trigger event
            //the particle effect (FX_BloodSplatter) comes from the free downloadable asset package: SimpleFX.
            StartCoroutine(PassengerHurt()); //coroutine is used to give time for the blood splatter and sound effect to play before restarting the level
            OhNo.GetComponent<SFX>().OhNo.Play();


        }
    }

    public void PlayerDied(string cause)
    {
        Dictionary<string, object> deathParameters = new Dictionary<string, object>()
            {
                {"level", SceneManager.GetActiveScene().buildIndex},
                {"playerName", PlayerPrefs.GetString("nickname")},
                {"time", Timer.currentTime},
            {"cause", cause },
             {"restarts", UIButtonManager.Restarts},
             {"playerSpeed", PlayerController.playerSpeed},
            {"playerRotation", PlayerController.playerRotation },
                {"position", Mathf.RoundToInt(transform.position.x / 5f)}

            };
        AnalyticsManager.SendCustomEvent("PlayerDeath", deathParameters);
    }
    IEnumerator PassengerHurt()
    {
        PlayerDied("hit rock");
        PlayerController.levelDeaths += 1;
        yield return new WaitForSeconds(1f); //enough time for the hurt animation to play
        PlayerController.Stop = true;
        PlayerController.movingLeft = false;
        PlayerController.movingRight = false;
        //the above is so that the player is not moving upon restart and so that they can move. 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //reloads the current scene.
        Time.timeScale = 1; //ensures time is active, not paused.
        PlayerController.camUp = false;
        PlayerController.camDown = false;
        PlayerController.camCent = false;
        PlayerController.camCentOpp = false;

        UIButtonManager.StrBut = false;
        UIButtonManager.UpBut = false;
        UIButtonManager.DowBut = false;
        UIButtonManager.PauBut = true;

        Timer.currentTime = 0;
        PlayerController.nextTrack = "none";

        TrackForce.onVertical = false;
        TrackForce.onInverted = false;
        TrackForceCargo.onVertical = false;
        TrackForceCargo.onInverted = false;
        TrackForcePassenger.onVertical = false;
        TrackForcePassenger.onInverted = false;
        //the above ensures the train is not in loopdeloop mode upon restarting a level.
        Player.GetComponent<InventoryManager>().RefreshTracks(); //this reloads the tracks available in the inventory upon restarting a level.
    }
    
}
