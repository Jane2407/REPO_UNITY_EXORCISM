using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    GameObject bullet;
    int bulletForce=10;
    public Transform firePosition;

    private void Start()
    {
        firePosition = gameObject.transform.parent.GetChild(0).transform;
    }

    public void AddBullet(GameObject _bullet)
    {
        bullet = _bullet;
    }

    public void Shoot()
    {
        if (!GameManager.isPause)
        {
            GameObject go = Instantiate(bullet, firePosition.position, firePosition.rotation);
            go.GetComponent<Rigidbody2D>().AddRelativeForce(transform.right * bulletForce, ForceMode2D.Impulse);
        }
        
    }

    private void DestroyWeapon()
    {
        Destroy(gameObject);
    }
}
