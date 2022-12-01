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
 *       // Request ForcedRecycledObjectPool from PoolHandler (through GameObjectDependencyManager)
 *       myPool = GameObjectDependencyManager.instance.GetGameObject("PoolHandler")
                  .GetComponent<PoolHandler>().GetPool(collectible.gameObject.name, PoolType.ForcedRecycleObjectPool);
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

    [SerializeField] Dictionary<string, Pool> poolDictionary = new Dictionary<string, Pool>();

    public Pool GetPool(string tmpGameObject, PoolType poolType)
    {

        if (poolDictionary.TryGetValue(tmpGameObject, out Pool tmpPool))
        {
            return poolDictionary[tmpGameObject];
        }


        switch(poolType)
        {
            case PoolType.ForcedRecycleObjectPool:
                {
                    Pool tempPool = this.gameObject.AddComponent<ForcedRecycleObjectPool>();
                    poolDictionary.Add(tmpGameObject, tempPool);
                    return tempPool;
                }
            case PoolType.NormalPool:
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


public enum PoolType
{
    NormalPool, ForcedRecycleObjectPool
}



