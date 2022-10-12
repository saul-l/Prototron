using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    public List<GameObject> pooledObjects;

    public int amountToPool;

    void Start()
    {

    }

    public void PopulatePool(GameObject objectToPool, int amount)
    {
        amountToPool = amount;
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.transform.parent = this.transform.parent;
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }
    public virtual GameObject GetPooledObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        return null;
    }


}