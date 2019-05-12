using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    int playerNumber;            //Setting current player index

    private void Start()
    {
        playerNumber = GetComponent<PlayerData>().playerIndex;
    }

    private void Update()
    {
        if (playerNumber == 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                //Sending massege to shot to any weapon on this player
                BroadcastMessage("Shoot", SendMessageOptions.DontRequireReceiver);
        }
        else if (playerNumber==1)
        {
            if (Input.GetKeyDown(KeyCode.Keypad0))
                //Sending massege to shot to any weapon on this player
                BroadcastMessage("Shoot", SendMessageOptions.DontRequireReceiver);
        }
    }
}
