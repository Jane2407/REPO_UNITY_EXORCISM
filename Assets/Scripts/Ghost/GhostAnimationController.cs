using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAnimationController : MonoBehaviour
{
    PlayerData playerData;
    PlayerController playerController;
    Animator anim;

    public Vector2 velocity;            //Ghost velocity
    public bool isOnGround;             //Is ghost on ground


    private void Start()
    {
        playerData = GetComponent<PlayerData>();
        playerController = GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        anim.SetBool("isOnGround", isOnGround);
        anim.SetInteger("vx", Mathf.RoundToInt(velocity.x));
        anim.SetInteger("vy", Mathf.RoundToInt(velocity.y));
    }

}
