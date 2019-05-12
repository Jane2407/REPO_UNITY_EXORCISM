using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPlayer : MonoBehaviour
{
    GameObject player;
    SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        player = transform.parent.gameObject;    
    }

    private void Update()
    {
        
        if (player.transform.position.y < transform.position.y)
        {
            sr.enabled = false;
        }
        else
        {
            sr.enabled = true;
        }
        transform.position = new Vector3(player.transform.position.x, 3, transform.position.z);

    }
}
