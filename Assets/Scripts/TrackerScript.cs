//this script controls the tracker to help guide the player

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackerScript : MonoBehaviour
{
    public PowerScript power;
    public GameObject player;
    public GameObject parentShip;
    public PlayerMovement parts;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (power.tracker && (parts.partsFound < 5))
        {
            var child = parentShip.transform.GetChild(0);
            Vector3 locate = (child.position - player.transform.position).normalized;

            // rotate towards target

            // angle to player
            float angle = Mathf.Atan2(locate.y, locate.x) * Mathf.Rad2Deg - 270;
            //assigning this rotation to the player
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 20);

            transform.position = player.transform.position + (locate);

        }
        else
        {
            transform.position = new Vector2(0, -20);
        }
    }
}
