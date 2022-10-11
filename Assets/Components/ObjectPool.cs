using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : MonoBehaviour
{
    // Start is called before the first frame update
    public static ObjectPool SharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;
    public LinkedList<GameObject> pooledObj;
    
    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }


    }

    // If object is used it goes to the bottom of the list
    // top of the list is used if no free object
    // Optimization idea: running number which is current iteration%amountToPool

    public GameObject GetPooledObject()
    {
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