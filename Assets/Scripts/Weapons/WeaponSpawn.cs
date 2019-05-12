using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawn : MonoBehaviour
{
    SpriteRenderer imageRender;
    BoxCollider2D boxCollider;
    public List<Sprite> weaponsSprites;

    public int weaponIndex;                 //Weapon index for spawn this turn
    int timeleft = 5;                       //Time for repating weapon spawn

    private void Start()
    {
        imageRender = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        SetIndex();
    }

    //Setting random index of weapon 
    void SetIndex()
    {
        weaponIndex = Random.Range(0, 4);
        SpawnWeapon();
    }

    void SpawnWeapon()
    {
        //Enabling render and collider
        imageRender.enabled = true;
        boxCollider.enabled = true;

        //Choosing proper sprite from weapons sprites list
        imageRender.sprite = weaponsSprites[weaponIndex];
    }

    public void DeleteWeapon()
    {
        //Enabling render and collider
        imageRender.enabled = false;
        boxCollider.enabled = false;

        //Calling for timer
        InvokeRepeating("Timer", 1, 1);
    }

    //Timer for spawn new weapon
    void Timer()
    {
        if (timeleft > 0)
            timeleft--;
        else
        {
            timeleft = 5;
            CancelInvoke();
            SetIndex();
        }
    }
}
