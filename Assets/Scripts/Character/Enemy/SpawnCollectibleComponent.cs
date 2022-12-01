using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnCollectibleComponent : MonoBehaviour
{
    bool collectibleManagerAssigned = false;
    void Start()
    {
        if (!collectibleManagerAssigned)
        {
            GetComponent<IDamageable>().EventDead += spawnCollectibleOnDeath;
            collectibleManagerAssigned = true;
        }
    }

    void spawnCollectibleOnDeath()
    {
        GameObjectDependencyManager.instance.GetGameObject("CollectibleManager").GetComponent<CollectibleManager>().SpawnCollectible(transform.position);
    }

}
