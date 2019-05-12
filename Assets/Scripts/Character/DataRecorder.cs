using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataRecorder : MonoBehaviour
{
    public List<RecordFrame> record;
    Rigidbody2D rb2d;
    PlayerData pd;
    PlayerController pc;

    bool isPlayer1;             //Is current player 1?

    private void Start()
    {
        if (GetComponent<PlayerData>().playerIndex == 0)
        {
            isPlayer1 = true;
        }
        rb2d = GetComponent<Rigidbody2D>();
        pd = GetComponent<PlayerData>();
        record = new List<RecordFrame>();
        pc = GetComponent<PlayerController>();
    }

    //Recordng all values every frame
    private void FixedUpdate()
    {
        RecordFrame recordFrame = new RecordFrame();
        recordFrame.position = transform.position;
        recordFrame.rotation = transform.rotation;
        if (isPlayer1)
        {
            recordFrame.isShooting = Input.GetKeyDown(KeyCode.Space);
        }
        else
        {
            recordFrame.isShooting = Input.GetKeyDown(KeyCode.Keypad0);
        }
        recordFrame.weaponIndex = pd.weaponIndex;
        recordFrame.hasWeapon = pd.hasWeapon;
        recordFrame.velocity = rb2d.velocity;
        recordFrame.isOnGround = pc.isOnGround;
        record.Add(recordFrame);
    }
}
