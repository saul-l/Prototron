/* Collectible Manager handles collectible lists
 *
 * Currently only handles weapon spawning with temp solution.
 * Save system needed before final implementation
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{

    [SerializeField] float collectibleProbability = 0.5f;
    [SerializeField] GameObject[] weaponCollectible;
    public List<GameObject> weaponCollectibleList = new List<GameObject>();
    void Start()
    {
        for(int i = 0; i < weaponCollectible.Length; i++)
        {
            GameObject listedCollectible = Instantiate(weaponCollectible[i]);
            listedCollectible.GetComponent<ICollectible>().CollectibleList = weaponCollectibleList;
            weaponCollectibleList.Add(listedCollectible);
            listedCollectible.SetActive(false);

        }
    }


    // Update is called once per frame
    public void SpawnCollectible(Vector3 position)
    {
        if(Random.Range(0.0f,1.0f)>collectibleProbability)
        { 
            if(weaponCollectibleList.Count > 0)
            {
                int spawnedWeaponIndex = Random.Range(0, weaponCollectibleList.Count);
                GameObject spawnedCollectible = weaponCollectibleList[spawnedWeaponIndex];
                spawnedCollectible.transform.position = position + spawnedCollectible.transform.localPosition;
                spawnedCollectible.SetActive(true);
                weaponCollectibleList.RemoveAt(spawnedWeaponIndex);
            }
        }
    }
}
