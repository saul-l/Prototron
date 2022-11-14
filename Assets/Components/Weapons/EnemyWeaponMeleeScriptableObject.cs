using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/EnemyWeaponMelee", order = 1)]
public class EnemyWeaponMeleeScriptableObject : ScriptableObject
{
    public float attackRate;
}