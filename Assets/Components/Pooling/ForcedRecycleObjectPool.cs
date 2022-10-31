using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcedRecycleObjectPool : Pool
{

    public override GameObject GetPooledObject()
    {

        GameObject tmp;

        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                tmp = pooledObjects[i];
                pooledObjects.RemoveAt(i);
                pooledObjects.Add(tmp);
                tmp.transform.parent = this.transform.parent;
                return tmp;
            }
        }

        tmp = pooledObjects[0];
        pooledObjects.RemoveAt(0);
        pooledObjects.Add(tmp);
        tmp.transform.parent = this.transform.parent;
        return tmp;
    }

}
