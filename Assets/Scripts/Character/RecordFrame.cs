using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RecordFrame
{
    public Vector3 position;
    public Quaternion rotation;
    public bool isShooting;
    public int weaponIndex;
    public bool hasWeapon; //part of weapon index 0
    public Vector2 velocity;
    public bool isOnGround;
}
