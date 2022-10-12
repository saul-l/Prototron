using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    
    public GameObject pool;
    public GameObject[] enemyTypes;
    public int[] enemyAmounts;
    public bool randomPositions = true;

    private void OnValidate()
    {
     // Only allow ISpawnables to be spawned
        for (int i = 0; i < enemyTypes.Length; i++)
        {        
            if (!enemyTypes[i].TryGetComponent(typeof(ISpawnable), out var component))
                enemyTypes[i] = null;
        }
    }
    void Start()
    {
       for(int i = 0; i < enemyTypes.Length; i++)
        {
            GameObject tmpGO = GameObject.Find(pool.name);

            if (tmpGO = null)
            { 
                tmpGO = Instantiate(pool);
                ObjectPool tmpOP = tmpGO.GetComponent<ObjectPool>();
                tmpOP.objectToPool = enemyTypes[i];
                tmpOP.amountToPool = enemyAmounts[i];
               
            }
            else
            {
                
            }


            testSpawn();
        }
    }

    void testSpawn()
    {
        GameObject newEnemy = ObjectPool.SharedInstance.GetPooledObject();
        
            if (newEnemy!= null)
            {
                Vector3 pos = new Vector3(1.0f, 1.0f, 1.0f);
                newEnemy.transform.position = new Vector3(Random.Range(-1.0f,1.0f), 0, Random.Range(-1.0f, 1.0f));
                newEnemy.transform.rotation = Quaternion.identity;
                newEnemy.SetActive(true);

            }
    }

}
