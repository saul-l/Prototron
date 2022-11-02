using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerController;

public class PoolHandler : MonoBehaviour
{
    public static PoolHandler instance;
    Dictionary<string, Pool> poolDictionary = new Dictionary<string, Pool>();

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
                return this.gameObject.AddComponent<ForcedRecycleObjectPool>();
            case PoolTypes.PoolType.NormalPool:
                return this.gameObject.AddComponent<Pool>();
            default:
                return this.gameObject.AddComponent<Pool>();
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


