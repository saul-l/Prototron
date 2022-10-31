using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeComponent : MonoBehaviour, IWeapon
{

    // Update is called once per frame
    float rateOfFire = 1.0f;
    int damage = 1;
    float knockBack = 1.0f;
    private float rateOffFire = 0.3f;

    Collider Collider;

    public void Attack(Vector3 attackDirection)
    {
        
    }

    public float RateOfFire
    {
        get
        {
            return rateOffFire;
        }
        set
        {
            rateOffFire = value;
        }
    }



}
