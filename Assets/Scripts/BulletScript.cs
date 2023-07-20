//this script controls the bullets that are being fired from the enemies

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    //variable to control the bullet speed and can be changed in editor
    public float BulletSpeed;

    //references to other components
    public GameObject player;
    public PlayerMovement playerScript;
    public Rigidbody2D rb;
    public PowerScript power;

    //private vector to aim the bullet
    private Vector2 aim;


    // Start is called before the first frame update
    void Start()
    {
        //when a new bullet is created, it will aim towards the player

        //needs to make sure the original object is not affected so it can clone properly
        if (!(gameObject.name == "Bullet"))
        {
            //assigns a direction and velocity
            aim = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y).normalized;
            rb.velocity = aim * BulletSpeed;

            //make the bullet point towards the player, 90 at the end is the offset

            float dir = Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(Vector3.forward * (dir - 90));

            Invoke("deleteBullet", 10);
        }

    }


    //this function detects when the bullet has collided with specific objects and what it will do
    private void OnTriggerEnter2D(Collider2D other)
    {
        //if it hits a part of the map or the player, just delete this clone
        if (other.gameObject.tag == "Level" || other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject, 0);
        }
    }

    //this function auto deletes a bullet if it hasnt hit anything in a long time (bug fixes)
    void deleteBullet()
    {
        Destroy(this.gameObject, 0);
    }
}
