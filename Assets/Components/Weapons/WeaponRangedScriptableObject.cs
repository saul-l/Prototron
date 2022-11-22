using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WeaponRanged", order = 1)]
public class WeaponRangedScriptableObject : ScriptableObject
{
    public float attackRate;
    public float bulletSpeed;
    public int damage;
    public float inaccuracy;
    public int pooledBullets;
    public int clipSize;
    public float reloadTime;
    public int bulletsPerShot;
}