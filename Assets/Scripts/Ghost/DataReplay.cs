using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataReplay : MonoBehaviour
{
    Rigidbody2D rb2d;
    GhostData gd;
    PlayerController pc;
    GhostAnimationController ac;

    public List<RecordFrame> record;            //List with all frame records from previous player's round
    int index;                                  //Index of current record frame                         

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        gd = GetComponent<GhostData>();
        ac = GetComponent<GhostAnimationController>();
    }

    private void FixedUpdate()
    {
        //Checking for any available record frames
        if (index > record.Count - 1)
        {
            //Destroy ghost
            transform.parent.gameObject.GetComponent<PlayersManager>().ghosts.Remove(gameObject);
            Destroy(gameObject);
        }
        else
        {
            //Setting ghost position and rotation from record
            transform.position = record[index].position;
            transform.rotation = record[index].rotation;

            //Setting ghost weapon
            gd.weaponIndex = record[index].weaponIndex;
            gd.hasWeapon = record[index].hasWeapon;

            //Simulating fire button press when player is attacking
            if (record[index].isShooting)
            {
                //Sending massage to Shot to any weapon on ghost
                BroadcastMessage("Shoot", SendMessageOptions.DontRequireReceiver);
            }

            //Setting velocity and isOnGround for animation
            ac.velocity = record[index].velocity;
            ac.isOnGround = record[index].isOnGround;

            //Adding index for next frame
            index++;
        }
    }
}
