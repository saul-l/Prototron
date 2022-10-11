using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcedRecycleObjectPool : ObjectPool
{

    public override GameObject GetPooledObject()
    {
        Debug.Log("lol");
        GameObject tmp;

        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                tmp = pooledObjects[i];
                pooledObjects.RemoveAt(i);
                pooledObjects.Add(tmp);
                return tmp;
            }
        }

        tmp = pooledObjects[0];
        pooledObjects.RemoveAt(0);
        pooledObjects.Add(tmp);
        return tmp;
    }

}
