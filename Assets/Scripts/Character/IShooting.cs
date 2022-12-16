using UnityEngine;
public interface IShooting
{
    Vector3 shootingDirection
    {
        get;
        set;
    }

    bool fire
    {
        get;
        set;
    }
    WeaponRangedScriptableObject weaponType
    {
        get;
        set;
    }

    int bulletsLeft
    {
        get;
    }
}