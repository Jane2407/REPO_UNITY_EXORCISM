using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostData : MonoBehaviour
{

    public bool hasWeapon;          //Is ghost has weapon?
    public int weaponIndex;         //Weapon index which ghost had

    private void Update()
    {
        if (hasWeapon)
        {
            //Calling for AddWeaponGhost function to equipt this weapon
            transform.parent.GetComponent<PlayersManager>().AddWeaponGhost(gameObject, weaponIndex);
        }
    }
}


