/* Enemy Spawner for a wave of enemies
 * 
 * Creates pools for enemies and list of enemy types based on their values and total value specified.
 *
 * 
 * Usage (in inspector):
 * -spawnPerimeter needs gameobject with Box Collider for spawn area
 * -Add enemy types you wish to spawn into enemyType array
 * -Add enemy point value into enemyValue array
 * -Add total enemy value to totalEnemyValue array
 * -Add spawn interval between different enemies into interval
 * */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    
    [HideInInspector] public Pool[] myPool;
    public Transform spawnPerimeter;
    public GameObject[] enemyType;
    [Range(1,100)]
    public int[] enemyValues;
    public int totalEnemyValue;
    private int enemyCount = 0;
    public float interval = 3.0f;
    public List<int> enemyOrder;
    private Vector2 worldSize = Vector2.one;
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
        CreateEnemyOrderAndPools();

        spawnPerimeterCenter = spawnPerimeter.position;
        spawnPerimeterX = spawnPerimeter.localScale.x;
        spawnPerimeterY = spawnPerimeter.position.y;
        spawnPerimeterZ = spawnPerimeter.localScale.z;

    }



    void Update()
    {
     
        if (enemyCount<enemyOrder.Count)
        {
            prevTime=Time.time;
            SpawnEnemy();
            enemyCount++;
        }
    }


    
    void CreateEnemyOrderAndPools()
    {

        

        Array.Sort(enemyValues, enemyType);
      
        int[] enemyAmount = new int[enemyType.Length];

        int randomVal;
        int randomMax = enemyType.Length;
        //randomize enemies to enemyOrder[] 
        while (totalEnemyValue >= enemyValues[0] && randomMax > 0)
        {
            randomVal = UnityEngine.Random.Range(0, randomMax);

            // If enemy value exceeds total value left we need to decrease randomMax until we don't hit the ceiling
            
            if(enemyValues[randomVal]>totalEnemyValue)
            {
           
                while (enemyValues[randomMax-1] > totalEnemyValue)
                {
                    randomMax--;
              
                    if (randomMax < 0) break;
                }              
            }
            else
            { 
                enemyOrder.Add(randomVal);
                enemyAmount[randomVal]++;
                totalEnemyValue -= enemyValues[randomVal];
    
            }
        }

        

    // Pools for enemies
    myPool = new Pool[enemyType.Length];

        for (int i = 0; i < enemyType.Length; i++)
        {
            myPool[i] = PoolHandler.instance.GetPool(enemyType[i].gameObject.name, PoolTypes.PoolType.NormalPool);
            myPool[i].PopulatePool(enemyType[i], enemyAmount[i]);
        }

    }

    Vector3 CalculatePointOnSpawnPerimeter()
    {
        // Calculates random position between 0 and rectangle edge size and then "folds the rectangle" around the position

        float spawnPerimeterSize = (spawnPerimeterX + spawnPerimeterZ) * 2;
        float randomNumber = UnityEngine.Random.Range(0,spawnPerimeterSize);
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
    void SpawnEnemy()
    {
        Vector3 spawnPosition = CalculatePointOnSpawnPerimeter();
       
        GameObject newEnemy = myPool[enemyOrder[enemyCount]].GetPooledObject();
        
            if (newEnemy!= null)
            {
                Vector3 pos = new Vector3(1.0f, 1.0f, 1.0f);
                newEnemy.transform.position = spawnPosition;
                newEnemy.transform.rotation = Quaternion.identity;
                newEnemy.SetActive(true);
                
            }
    }

}
