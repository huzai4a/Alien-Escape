//this script controls the plane that will fly around and drop power ups for the player

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneScript : MonoBehaviour
{
    //variables to control the settings of the plane, plane speed is how fast the plane flies, planeDelay is how often it will arrive and drop power ups
    public float planeSpeed;
    public float planeDelay;

    //private variables that control the timer for the plane
    private float timer;

    //refrences to other components
    public Transform target;
    public Rigidbody2D rb;

    //the vector to control where the power up is dropped
    private Vector2 dropPoint;

    //the different power ups that are to be cloned
    public GameObject p1;
    public GameObject p2;
    public GameObject p3;
    public GameObject p4;
    public GameObject p5;
    public GameObject p6;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        waitPlane();
    }

    //this function waits for the timer and activates the plane when the set delay has been reached
    void waitPlane()
    {
        //timer to track the plane delay
        timer += Time.deltaTime;
        if (timer > planeDelay)
        {
            //once the timer has reached its set time, the plane will fly in and drop a random powerup
            movePlane();

            //resets the timer
            timer -= planeDelay;
        }
    }

    //this function moves the plane and drops a random powerup
    void movePlane()
    {
        //start by making the plane go to the player
        //dropPoint is where the powerup will be dropped at
        dropPoint = new Vector2(target.position.x, target.position.y);
        transform.position = dropPoint;

        //point it in a random direction
        int angle = getRanNum(0,360);

        //move back in that direction by a reasonable amount
        Vector2 displace = new Vector2(Mathf.Cos(angle),Mathf.Sin(angle))*20;
        transform.position = new Vector2(dropPoint.x - displace.x, dropPoint.y - displace.y);

        //make the plane point towards the player, 90 at the end is the offset
        Vector2 direction = target.position - transform.position;
        direction.Normalize();
        float aim = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * (aim - 90));

        //now move the plane back towards the original position so it looks like the plane flew in from a random direction
        rb.velocity = planeSpeed * displace/20;

        //drops a power up after a delay in time
        Invoke("pickRandomPower",2);
    }

    //this function drops a random powerup
    void pickRandomPower()
    {
        int rand = getRanNum(1, 8);
        switch (rand)
        {
            case 1:
                Instantiate(p1, transform.position, Quaternion.Euler(Vector3.zero));
                break;
            case 2:
                Instantiate(p2, transform.position, Quaternion.Euler(Vector3.zero));
                break;
            case 3:
                Instantiate(p3, transform.position, Quaternion.Euler(Vector3.zero));
                break;
            case 4:
                Instantiate(p4, transform.position, Quaternion.Euler(Vector3.zero));
                break;
            case 5:
                Instantiate(p5, transform.position, Quaternion.Euler(Vector3.zero));
                break;
            default:
                Instantiate(p6, transform.position, Quaternion.Euler(Vector3.zero));
                break;
        }
    }

    //This routine gets a random number between the range of
    //lowVal and highVal (both sent in), returning this value as an integer.
    private int getRanNum(int lowVal, int highVal)
    {
        System.Random randGen = new System.Random();

        //To generate a random integer value use this:
        int tempRandom = randGen.Next(lowVal, highVal);

        //returns the random integer
        return tempRandom;
    }
}
