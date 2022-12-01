/*  Collectible weapon component.
   
    Requires collision, which only reacts to player.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleWeaponComponent : MonoBehaviour , ICollectible
{
    public List<GameObject> myCollectibleList;
    [SerializeField] private WeaponRangedScriptableObject weaponType;

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<IShooting>().weaponType = weaponType;
        ReturnToList();
    }

    public List<GameObject> CollectibleList
    { 
        set => myCollectibleList = value;
    }

    public void ReturnToList()
    {
        myCollectibleList.Add(gameObject);
        gameObject.SetActive(false);
    }

}
