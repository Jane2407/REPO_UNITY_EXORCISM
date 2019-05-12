using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    GameObject shotgunBullet;
    int bulletForce = 10;
    public Transform firePosition;

    private void Start()
    {
        firePosition = gameObject.transform.parent.GetChild(0).transform;
    }


    public void AddBullet(GameObject bullet)
    {
        shotgunBullet = bullet;
    }

    public void Shoot()
    {
        if (!GameManager.isPause)
        {
            for (int i = 0; i < 5; i++)
            {
                GameObject go = Instantiate(shotgunBullet, firePosition.position, firePosition.transform.parent.rotation);
                go.GetComponent<Rigidbody2D>().AddRelativeForce(Quaternion.AngleAxis((2 - i) * 5, new Vector3(0, 0, 1)) * go.transform.right * bulletForce, ForceMode2D.Impulse);

            }
        }
    }

    private void DestroyWeapon()
    {
        Destroy(gameObject);
    }
}
