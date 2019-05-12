using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public bool isAttacking;        //Is sword can attack?

    public void Shoot()
    {
        //Setting isAttacking to true and calling invoke to set isAttacking to false after 1
        isAttacking = true;
        Invoke("EndShoot", 1);
    }
    public void EndShoot()
    {
        isAttacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Checking if sword has collision when attack
        if (isAttacking&&!GameManager.isPause)
        {
            //Checking for collision tag
            if (collision.gameObject.tag == "Player")
            {
                collision.GetComponent<PlayerData>().Die();
            }
            else if (collision.gameObject.tag == "Ghost")
            {
                Destroy(collision.gameObject);
            }
        }
    }

    //Destroy weapon when it's called
    private void DestroyWeapon()
    {
        Destroy(gameObject);
    }
}
