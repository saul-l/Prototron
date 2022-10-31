using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    
    public Pool[] myPool;
    public Transform spawnPerimeter;
    public GameObject[] enemyType;
    public float interval = 3.0f;
    public int[] enemyAmounts;
    public bool randomPositions = true;
    public Vector2 worldSize = Vector2.one;

    private float prevTime = 0.0f;
    private float spawnPerimeterX;
    private float spawnPerimeterZ;
    private float spawnPerimeterY;
    private Vector3 spawnPerimeterCenter;
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
            myPool[i] = PoolHandler.instance.GetPool(enemyType[i].gameObject.name, PoolTypes.PoolType.NormalPool);
            myPool[i].PopulatePool(enemyType[i], enemyAmounts[i]);
        }

        spawnPerimeterCenter = spawnPerimeter.position;
        spawnPerimeterX = spawnPerimeter.localScale.x;
        spawnPerimeterY = spawnPerimeter.position.y;
        spawnPerimeterZ = spawnPerimeter.localScale.z;

    }



    void Update()
    {

        if(Time.time>=prevTime+interval)
        {
            prevTime=Time.time;
            testSpawn();
        }
    }
   
    Vector3 CalculatePointOnSpawnPerimeter()
    {
        // Calculates random position between 0 and rectangle edge size and then "folds the rectangle" around the position

        float spawnPerimeterSize = (spawnPerimeterX + spawnPerimeterZ) * 2;
        float randomNumber = Random.Range(0,spawnPerimeterSize);
        float spawnPointX;
        float spawnPointZ;

        if (randomNumber <= spawnPerimeterX)
        { 
            spawnPointX = randomNumber - 0.5f * spawnPerimeterX;
            spawnPointZ = -spawnPerimeterZ * .5f;
        }
        else if (randomNumber <= spawnPerimeterX + spawnPerimeterZ)
        {
            spawnPointX = -spawnPerimeterX * .5f;
            spawnPointZ = randomNumber - (0.5f * spawnPerimeterZ + spawnPerimeterX);            
        }
        else if (randomNumber <= 2 * spawnPerimeterX + spawnPerimeterZ)
        {
            spawnPointX = randomNumber - (1.5f * spawnPerimeterX + spawnPerimeterZ);
            spawnPointZ = spawnPerimeterZ * .5f;
        }
        else
        {
            spawnPointX = spawnPerimeterX * .5f;
            spawnPointZ = randomNumber - (1.5f * spawnPerimeterZ + 2 * spawnPerimeterX);
        }

        return new Vector3(spawnPointX, spawnPerimeterY, spawnPointZ) + spawnPerimeterCenter;


    }
    void testSpawn()
    {
        Vector3 spawnPosition = CalculatePointOnSpawnPerimeter();
        

        GameObject newEnemy = myPool[0].GetPooledObject();
        
            if (newEnemy!= null)
            {
                Vector3 pos = new Vector3(1.0f, 1.0f, 1.0f);
                newEnemy.transform.position = spawnPosition;
                newEnemy.transform.rotation = Quaternion.identity;
                newEnemy.SetActive(true);
                
            }
    }

}
