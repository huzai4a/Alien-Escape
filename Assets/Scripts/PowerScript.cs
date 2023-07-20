//this script is for the power ups to detect when it is picked up

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerScript : MonoBehaviour
{
    //boost timer
    public float boostTime;

    //refrences to other scripts
    public TimerScript timer;

    //different types of boosts
    public float speedBoost;
    public bool shield;
    public bool ghost;
    public bool tracker;
    


    // Update is called once per frame
    void Update()
    {
        //once the boost time has ended, reset the player back to normal
        if (boostTime < timer.time)
        {
            speedBoost = 0;
            shield = false;
            ghost = false;
            tracker = false;
        }
    }


    

}
