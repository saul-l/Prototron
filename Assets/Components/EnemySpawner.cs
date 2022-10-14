using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    
    public Pool[] myPool;
    
    public GameObject[] enemyType;
    public float interval = 3.0f;
    public int[] enemyAmounts;
    public bool randomPositions = true;
    private float prevTime = 0.0f;
    
    private void OnValidate()
    {
     // Only allow ISpawnables to be spawned
     /*   for (int i = 0; i < enemyType.Length; i++)
        {        
            if (!enemyType[i].TryGetComponent(typeof(ISpawnable), out var component))
                enemyType[i] = null;
        }
        */
    }
    void Start()
    {
        myPool = new Pool[enemyType.Length];
        for(int i=0; i<enemyType.Length;i++)
        {
      
            
            myPool[i] = PoolHandler.instance.GetPool(enemyType[i].gameObject.name, PoolTypes.PoolType.ForcedRecycleObjectPool);
            myPool[i].PopulatePool(enemyType[i], enemyAmounts[i]);
        }
        Debug.Log("enemyType length " + enemyType.Length);
    }

    void Update()
    {

        if(Time.time>=prevTime+interval)
        {
            prevTime=Time.time;
            testSpawn();
        }
    }
   
    void testSpawn()
    {
        GameObject newEnemy = myPool[0].GetPooledObject();
        
            if (newEnemy!= null)
            {
                Vector3 pos = new Vector3(1.0f, 1.0f, 1.0f);
                newEnemy.transform.position = new Vector3(Random.Range(-1.0f,1.0f), 0, Random.Range(-1.0f, 1.0f));
                newEnemy.transform.rotation = Quaternion.identity;
                newEnemy.SetActive(true);

            }
    }

}
