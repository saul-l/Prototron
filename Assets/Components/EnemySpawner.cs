using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject pool;
    public GameObject[] enemyTypes;
    public int[] enemyAmounts;

    void Start()
    {
       for(int i = 0; i < enemyTypes.Length; i++)
        {
            GameObject tmpGO = Instantiate(pool);
            ObjectPool tmpOP = tmpGO.GetComponent<ObjectPool>();
            tmpOP.objectToPool = enemyTypes[i];
            tmpOP.amountToPool = enemyAmounts[i];
        }
    }

    void testSpawn()
    {
        GameObject newEnemy = ObjectPool.SharedInstance.GetPooledObject();
    }

}
