using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeBullet : MonoBehaviour
{
    Animator anim;

    float damageRadius = 1;         //Grenade damage radius

    public AudioClip sound;

    void Start()
    {
        
        anim = GetComponent<Animator>();
        //Calling Explode grenade after 1.5
        Invoke("OnGrenadeExplode", 1.5f);
    }

    void OnGrenadeExplode()
    {
        GetComponent<AudioSource>().PlayOneShot(sound);
        //Freezing velocity and rotation  
        GetComponent<Rigidbody2D>().velocity=Vector3.zero;
        GetComponent<Rigidbody2D>().freezeRotation=true;

        //Calling explosion animation 
        anim.SetTrigger("Explode");

        //Creating overlap circle collider and adding all colliders to array
        Vector2 explosionPos = transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPos, damageRadius);
        //Checking every collider in array for tag "player" or "ghost"
        foreach (Collider2D hit in colliders)
        {
            if (hit.gameObject.tag == "Player")
            {
                hit.GetComponent<PlayerData>().Die();
            }
            else if (hit.gameObject.tag == "Ghost")
            {
                Destroy(hit.gameObject);
            }
        }

        //Destroy grenade after 1
        Destroy(gameObject,0.6f);
    }
}
