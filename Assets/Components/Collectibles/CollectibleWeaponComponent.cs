/*  Collectible weapon component.
   
    Requires collision, which only reacts to player.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleWeaponComponent : MonoBehaviour
{
    [SerializeField] private WeaponRangedScriptableObject weaponType;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("aa");
        other.gameObject.GetComponent<IShooting>().weaponType = weaponType;
        gameObject.SetActive(false);
    }
}
