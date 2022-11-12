
using System.Collections.Generic;
using UnityEngine;


public class Pool : MonoBehaviour
{
    static int pooledObjectCount;
    public List<GameObject> pooledObjects;

    public void PopulatePool(GameObject objectToPool, int amount, PopulateStyle populateStyle)
    {
        int amountToPool = 0;

        if(pooledObjects == null) pooledObjects = new List<GameObject>();

        GameObject tmp;
        if (populateStyle == PopulateStyle.Add)
        {
            amountToPool = amount;
        }
        else if (pooledObjects.Count < amount)
        {
            amountToPool = amount - pooledObjects.Count;
        }
                
        if(amountToPool > 0)
        {
            for (int i = 0; i < amountToPool; i++)
            {
                tmp = Instantiate(objectToPool);
                pooledObjectCount++;
                tmp.name = tmp.name + " " + pooledObjectCount;
                tmp.transform.parent = this.transform;
                tmp.SetActive(false);
                pooledObjects.Add(tmp);
            }
        }        
    }

    public void PopulatePool(GameObject objectToPool, int amount)
    {
        PopulatePool(objectToPool, amount, PopulateStyle.Normal);
    }

    public virtual GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        return null;
    }
}

public enum PopulateStyle
{
    Normal, Add
}