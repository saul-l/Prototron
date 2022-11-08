/* Pooling system PoolHandler
 * 
 * Required in scene for pooling system to work
 * 
 * PoolHandler.instance.GetPool(String GameObject name, PoolTypes.PoolType poolType)
 * Gives you a pool of GameObject type. If none exist already a new one is created.
 *
 * PoolTypes:
 * PoolTypes.PoolType.ForcedRecycleObjectPool - Regular pool, which will give nothing, if there are no inactive objects in pool
 * PoolTypes.PoolType.ForcedRecycleObjectPool - Pool which will automatically grab oldest active object, if there are no inactive objects in pool
 * 
 * In Pool class:
 * Pool.Populatepool(String GameObject name, int Amount)
 * Adds Amount objects of type GameObject to pool
 * 
 * Pool.GetPooledObject()
 * Gives object from pool, if one is available
 * 
 * InActive pooled objects are automatically considered available.
 * 
 * Usage example (in other game object):
 *       // GameObject used in pool
 *       public GameObject myPooledGameObject;
 *       // reference to pool
 *       public Pool myPool;
 *       // Create new ForcedRecycleObjectPool for 
 *       myPool = PoolHandler.instance.GetPool(myPooledGameObject.gameObject.name, PoolTypes.PoolType.ForcedRecycleObjectPool);
 *       // Add 20 instances of myPooledGameObject to pool
 *       myPool.PopulatePool(myPooledGameObject, 20);
 *       // Get one instance of myPooledGameObject from pool
 *       GameObject myPooledGameObjectInstance = myPool.GetPooledObject();
 *       // Assign transform position to it
 *       myPooledGameObjectInstante.transform.position = Vector3.zero;
 * */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerController;

public class PoolHandler : MonoBehaviour
{
    public static PoolHandler instance;
    [SerializeField] Dictionary<string, Pool> poolDictionary = new Dictionary<string, Pool>();

    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;      
    }


    public Pool GetPool(string tmpGameObject, PoolTypes.PoolType poolType)
    {

        if (poolDictionary.TryGetValue(tmpGameObject, out Pool tmpPool))
        {
            return poolDictionary[tmpGameObject];
        }


        switch(poolType)
        {
            case PoolTypes.PoolType.ForcedRecycleObjectPool:
                {
                    Pool tempPool = this.gameObject.AddComponent<ForcedRecycleObjectPool>();
                    poolDictionary.Add(tmpGameObject, tempPool);
                    return tempPool;
                }
            case PoolTypes.PoolType.NormalPool:
                {
                    Pool tempPool = this.gameObject.AddComponent<Pool>();
                    poolDictionary.Add(tmpGameObject, tempPool);
                    return tempPool;
                }
            default:
                {
                    Pool tempPool = this.gameObject.AddComponent<Pool>();
                    poolDictionary.Add(tmpGameObject, tempPool);
                    return tempPool;
                }
        }
        

    }

}

public class PoolTypes
{
    public enum PoolType
    {
        NormalPool, ForcedRecycleObjectPool
    }

}


