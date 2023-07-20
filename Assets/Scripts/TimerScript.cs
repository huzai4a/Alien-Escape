//this script controls the timer for the game

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    //this stores the time passed
    public float time;
    public float min;
    public float sec;

    //this is to change the display in the UI
    public Text timer;

    
    // Update is called once per frame
    void Update()
    {
        //updates the time
        time += Time.deltaTime;
        updateTimer();
    }

    //this function updates the timer in the UI
    void updateTimer()
    {
        min = Mathf.FloorToInt(time / 60);
        sec = Mathf.FloorToInt(time % 60);
        timer.text = string.Format("{0:00} : {1:00}",min,sec);
    }
}
