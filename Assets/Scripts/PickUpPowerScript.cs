//this script is to detect when a power up is picked up

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPowerScript : MonoBehaviour
{
    public PowerScript power;
    public TimerScript timer;
    public PlayerMovement player;

    //change the power.boosTime + ... to be the amount of time the power up lasts 


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if the player hits the power up, apply the correct boosts
        if (collision.gameObject.tag == "Player")
        {

            if (this.gameObject.tag == "Lightning")
            {
                power.speedBoost = 3;
                power.boostTime = timer.time + 10;

            }
            else if (this.gameObject.tag == "Health")
            {
                player.PlayerHealth += 20;
            }
            else if (this.gameObject.tag == "Shield")
            {
                power.boostTime = timer.time + 10;
                power.shield = true;
            }
            else if (this.gameObject.tag == "Star")
            {
                power.boostTime = timer.time + 7;
                power.speedBoost = 3;
                power.shield = true;
            }
            else if (this.gameObject.tag == "Ghost")
            {
                power.boostTime = timer.time + 10;
                power.ghost = true;
            }
            else if (this.gameObject.tag == "Tracker")
            {
                power.tracker = true;
                power.boostTime = timer.time + 15;
            }

            player.score += 10;

            Destroy(this.gameObject);
        }
    }
}

