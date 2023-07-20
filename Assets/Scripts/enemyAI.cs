//this script controls the enemy AI

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAI : MonoBehaviour
{
    //variables that can be changed to customize the enemies
    //if you want to change the base stats, scroll down to enemyStrength()
    private float AISpeed;
    private float AIVision;
    private float shootDelay;

    //components to refer to when finding player and making bullets
    public Rigidbody2D rb;
    public GameObject player;
    public GameObject bullet;
    public PowerScript power;
    public PlayerMovement parts;

    //private variables - distance to player, timer for shooting
    private float distance;
    private float timer;


    //movement vector as well as the direction that the enemy is facing
    private Vector2 movement;


    // Update is called once per frame
    void Update()
    {
        FindPlayer();
        enemyStrength();
    }

    void FixedUpdate()
    {
        //NOTE: add a game loop here, if (game = true) {move();}
        move();
    }

    //this function locates the player and assigns a vector to move towards the player
    void FindPlayer()
    {
        //finds the x and y components of distance and puts into vector
        float Dx = (player.transform.position.x - transform.position.x);
        float Dy = (player.transform.position.y - transform.position.y);
        distance = Mathf.Sqrt(Mathf.Pow(Dx,2) + Mathf.Pow(Dy, 2));

        //aims the enemy towards the player
        movement = new Vector2(Dx,Dy);
        
    }

    //this function moves the enemy towards the player
    void move()
    {
        //if the AI can see the player based on its vision distance, move towards player
        if (distance < AIVision && !(power.ghost))
        {
            rb.velocity = new Vector2(movement.x, movement.y) * AISpeed / distance;

            // rotate towards player
            Vector3 vectorToTarget = player.transform.position - transform.position;
            // angle to player
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 360;
            //assigning this rotation to the player
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 10);

            //if enemy is customized to shoot, shoot player
            if (shootDelay > 0)
            {
                shoot();
            }
            
        }
        //if the AI is too far from the player, stop moving
        else
        {
            rb.velocity = Vector2.zero;
        }
        
    }

    //this function makes the enemy shoot towards the player
    void shoot()
    {
        //timer to track the shooting delay
        timer += Time.deltaTime;
        if (timer > shootDelay)
        {
            //creates a clone of the bullet
            //go to the bullet script to see how it moves
            Instantiate(bullet, transform.position, transform.rotation);
            
            //resets the timer
            timer -= shootDelay;
        }
        
    }

    //this function will make the enemies stronger as the game progresses and more parts are found
    //you can also change the base stats of the enemy by changing the constants
    void enemyStrength()
    {
        AISpeed = 2 + (parts.partsFound / 7.5f);
        AIVision = 5 + (parts.partsFound / 5f);
        shootDelay = 1 - (parts.partsFound / 15);
    }


}
