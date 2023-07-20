using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    //variable to control player speed and can be set from editor as well as health
    public float PlayerHealth;
    public float moveSpeed = 5f;

    //score variable
    //NOTE: when the player dies, convert the time they survived to seconds and subtract from score
    //Score system might need to be balanced
    public float score = 1000;
    

    //ship parts found
    public int partsFound;

    //reference to components
    public Rigidbody2D rb;
    public PowerScript power;
    public Animator animator;
    public SpriteRenderer ghosteffect;
    public GameObject shield;
    public TimerScript timeReceiver;

    //score texts
    public Text scoreText;
    public Text onScreen;
    public Text WinScore;

    Vector2 movement;

    //refrences to the ship parts progression
    public Image s1;
    public Image s2;
    public Image s3;
    public Image s4;
    public Image s5;

    public AdjustTimes leaderboard;

    private bool isDead;
    private float time;
    
    void Start()
    {
        PlayerHealth = 100;

        s1.color = new Color(1, 1, 1, 0);
        s2.color = new Color(1, 1, 1, 0);
        s3.color = new Color(1, 1, 1, 0);
        s4.color = new Color(1, 1, 1, 0);
        s5.color = new Color(1, 1, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;

        //updates the animation parameters to let it know that its moving and in which direction
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (PlayerHealth <= 0 && !isDead)
        {
            isDead = true;
            //subtract score for death before calculating score based on time
            score -= 500;
            scoreCalculator();
            scoreText.text = "Score: " + (int)score;
            //check if score should be on the leaderboard
            leaderboard.LeaderBOnGameEnd((int)score, false);
        }

        visualEffects();

        //updates on screen score every frame
        onScreen.text = "Score: " + score;
    }

    // updates movement at a fixed rate, helps smooth out the physics of the game
    private void FixedUpdate()
    {
        //movement
        rb.MovePosition(rb.position + movement * (moveSpeed + power.speedBoost) * Time.fixedDeltaTime);
    }

    //this function detects when the player has collided with specific objects and what it will do
    private void OnTriggerEnter2D(Collider2D other)
    {
        //if the player hits an enemy or bullet, damage the player
        if ((other.gameObject.tag == "Bullet" || other.gameObject.tag == "Enemy") && !power.shield)
        {
            PlayerHealth -= 5;
            if (score >= 10)
            {
                score -= 50;
            }
        }

        //if the player gets a piece of the ship, increase the number of parts found
        if (other.gameObject.tag == ("ShipParts"))
        {
            Destroy(other.gameObject);
            //checks to see which piece was picked up and update in the UI
            if (other.gameObject.name == "Cockpit")
            {
                s1.color = new Color(1, 1, 1, 1);
            }
            else if (other.gameObject.name == "Right Rutter")
            {
                s2.color = new Color(1, 1, 1, 1);
            }
            else if (other.gameObject.name == "Left Rutter")
            {
                s3.color = new Color(1, 1, 1, 1);
            }
            else if (other.gameObject.name == "Left Wing")
            {
                s4.color = new Color(1, 1, 1, 1);
            }
            else if (other.gameObject.name == "Right Wing")
            {
                s5.color = new Color(1, 1, 1, 1);
            }

            partsFound++;
            score += 100;
            

            winCondition();
        }
    }

    //this function changes the visual effects when boosts are applied
    private void visualEffects()
    {
        //ghost mode
        if (power.ghost)
        {
            ghosteffect.color = new Color(1, 1, 1, 0.5f);
        }
        else
        {
            ghosteffect.color = new Color(1, 1, 1, 1);
        }

        //shield
        if (power.shield)
        {
            shield.transform.position = transform.position;
        }
        else
        {
            shield.transform.position = new Vector2(0, -20);
        }
    }

    //calculates the score based on a time multiplier
    private void scoreCalculator()
    {
        //gets time in seconds
        time = timeReceiver.min * 60 + timeReceiver.sec;
        //balances based on time with multiplier
        if (time <= 300)
        {
            score = score - (time *3/4);
        } else if (time <= 600)
        {
            score = score - (time * 5 / 4);
        } else
        {
            score = score - (time * 3 / 2);
        }

        //makes sure score isn't negative
        if(score  < 0)
        {
            score = 0;
        }
        
    }

    private void winCondition()
    {
        //you won
        if (partsFound >= 5)
        {
            scoreCalculator();
            //extra score for winning
            score += 500;
            WinScore.text = "Score: " + (int)score;
            //check if score should be on the leaderboard
            leaderboard.LeaderBOnGameEnd((int)score, true);
            
        }
    }
}
