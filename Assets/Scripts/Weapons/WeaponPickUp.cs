using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{

    public PlayersManager playersManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Adding weapong to player
            playersManager.AddWeaponToPlayer(collision.GetComponent<PlayerData>().playerIndex, GetComponent<WeaponSpawn>().weaponIndex, collision.GetComponent<PlayerData>().hasWeapon);

            // Deleting this weapon
            GetComponent<WeaponSpawn>().DeleteWeapon();
        }
    }
}
