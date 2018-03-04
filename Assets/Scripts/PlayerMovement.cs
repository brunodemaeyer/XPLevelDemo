using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GbangaExperienceLevelAPI_basic;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

    public float speed;
    public Text XPLabel;
    public Text LevelLabel;
    public Text TotXPLabel;
    public Text XP2NextLabel;
    public Text PercProgLabel;
    float xMovement;
    float zMovement;

    private Rigidbody rbPlayer;

    ExperienceLevel playerExp = new ExperienceLevel(); //no need for initialisation in Start(), all done in constructor

    private void Start()
    {
        rbPlayer = GetComponent<Rigidbody>();
        speed = 10;
        SetHUD();
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        /*float xMovement = Input.GetAxis("Horizontal");
        float zMovement = Input.GetAxis("Vertical");*/

        xMovement = Input.acceleration.x;
        zMovement = Input.acceleration.y;
        


        Vector3 move = new Vector3(xMovement, 0.0f, zMovement);

        rbPlayer.AddForce(move * speed);
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bad Target"))
        {
            long points = (long)(100f / collision.gameObject.transform.localScale.x);
            playerExp.SubtractExperience(points);
            print(points + " XP taken from you!");
        }

        if (collision.gameObject.CompareTag("Good Target"))
        {
            long points = (long)(100f / collision.gameObject.transform.localScale.x);
            playerExp.AddExperience(points);
            print(points + " XP added!");
        }

        SetHUD();        
    }

    void SetHUD()
    {
        XPLabel.text = "Current XP: " + playerExp.GetCurrentExperience();
        LevelLabel.text = "Current Level: " + playerExp.GetLevel();
        TotXPLabel.text = "Total XP: " + playerExp.GetTotalExperience();
        XP2NextLabel.text = "XP till Level " + (playerExp.GetLevel() + 1) + ": " + playerExp.ExperienceTillLevelUp();
        PercProgLabel.text = "Percentage of Level " + playerExp.GetLevel() + " completed: " + playerExp.ProgressToNextLevel();
    }

}
