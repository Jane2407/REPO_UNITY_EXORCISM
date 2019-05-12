using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBullet : MonoBehaviour
{

    public AudioClip sound;

    void Start()
    {
        GetComponent<AudioSource>().PlayOneShot(sound);
        //Destroy destroy after 1.5
        Destroy(gameObject,1.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Checking collision tag
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            collision.GetComponent<PlayerData>().Die();
        }
        else if(collision.gameObject.tag == "Ghost")
        {
            //When collision is ghost just destroy this ghost
            Destroy(collision.gameObject);

        }
    }
}
