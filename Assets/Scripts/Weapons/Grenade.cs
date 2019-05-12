using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    GameObject grenadeBullet;
    int grenadeForce = 5;

    public Transform firePosition;

    private void Start()
    {
        firePosition = gameObject.transform.parent.GetChild(0).transform;
    }

    public void AddGrenadeBullet(GameObject _grenadeBullet)
    {
        grenadeBullet = _grenadeBullet;
    }

    void Shoot()
    {
        if (!GameManager.isPause)
        {
            GameObject go = Instantiate(grenadeBullet, firePosition.position, firePosition.rotation);
            go.GetComponent<Rigidbody2D>().AddForce(transform.right * grenadeForce, ForceMode2D.Impulse);
        }
    }

    private void DestroyWeapon()
    {
        Destroy(gameObject);
    }
    
}
