using System.Numerics;
using UnityEngine;

public interface IWeapon 
{
    void Attack(UnityEngine.Vector3 attackDirection);

    float RateOfFire
    {
        get;
        set;
    }
}

