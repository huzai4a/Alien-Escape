//this script controls the animation of the healthbar

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarScript : MonoBehaviour
{
    //refrences to other components
    public Image healthbar;
    public PlayerMovement player;

    //chages the healthbar color according to the health left
    public Gradient grad;


    // Update is called once per frame
    void Update()
    {
        updateHealth();
    }

    void updateHealth()
    {
        //finds the health as a percentage
        float health = player.PlayerHealth / 100;

        //changes the amount of space the health bar takes up
        healthbar.fillAmount = health;

        //changes the color of the health bar based on the gradient in the editor
        healthbar.color = grad.Evaluate(health);

        //makes sure health doesnt go over 100
        if (player.PlayerHealth > 100)
        {
            player.PlayerHealth = 100;
        }
    }
}
