using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    PlayerData playerData;
    PlayerController playerController;
    Rigidbody2D rb;
    Animator anim;

    private void Start()
    {
        playerData = GetComponent<PlayerData>();
        playerController = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        //Setting bool IsOnGround for walking and jumping transition
        anim.SetBool("isOnGround", playerController.isOnGround);
        //Setting velocity for transition between clips
        anim.SetInteger("vx",  Mathf.RoundToInt(rb.velocity.x));
        anim.SetInteger("vy",  Mathf.RoundToInt(rb.velocity.y));
    }

    public void SetPlayerDead()
    {
        //Setting trigger isDead for die animation
        anim.SetTrigger("isDead");
    }
}
