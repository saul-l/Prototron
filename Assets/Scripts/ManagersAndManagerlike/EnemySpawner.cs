/* Enemy Spawner for a wave of enemies
 * 
 * NOTE: 
 * Work in progress monolith. Consider making abstract and creating different types of spawners, like infinite and sequence.
 * Currently contains elements of both and it's messy.
 * 
 * Creates pools for enemies and list of enemy types based on their values and total value specified.
 *
 * 
 * Usage (in inspector): OUT OF DATE
 * -spawnPerimeter needs gameobject with Box Collider for spawn area
 * -Add enemy types you wish to spawn into enemyType array
 * -Add enemy point value into enemyValue array
 * -Add total enemy value to totalEnemyValue array
 * -Add spawn interval between different enemies into interval
 * */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameManager gameManager;
    public Pool[] myPool;
    [SerializeField] private Transform spawnPerimeter;
    [SerializeField] private GameObject[] enemyType;
    [Range(1, 100)]
    [SerializeField] private int[] enemyPointValues;
    private List<int> enemySpawnAmounts = new();
    private List<int> enemyMaxAmounts = new();
    [SerializeField] private float totalEnemyPointValue;
    [SerializeField] private float interval = 3.0f;
    [SerializeField] private List<int> enemyOrder;
    [SerializeField] private int totalEnemies;
    [SerializeField] private int maxEnemiesAtTheSameTime = 100;
    
    [SerializeField] private int valueSpawned;
    [SerializeField] private float prevTime = 0.0f;
    [SerializeField] private bool active;

    [SerializeField] private int enemyPointsLeft;
    [SerializeField] private int phasePointValue = 100;

    private int currentPhaseEnemyValue;
    private int currentEnemyValueAddition = 2;
    
    [SerializeField] private int enemiesAlive;

    private float spawnPerimeterX;
    private float spawnPerimeterZ;
    private float spawnPerimeterY;
    private Vector3 spawnPerimeterCenter;



    private void Awake()
    {
        gameManager = GameObjectDependencyManager.instance.GetGameObject("GameManager").GetComponent<GameManager>();        
    }
    void Start()
    {
      
        Array.Sort(enemyPointValues, enemyType);

        spawnPerimeterCenter = spawnPerimeter.position;
        spawnPerimeterX = spawnPerimeter.localScale.x;
        spawnPerimeterY = spawnPerimeter.position.y;
        spawnPerimeterZ = spawnPerimeter.localScale.z;

        CreatePools();
    }

    void Update()
    {
        // this is horrible
        if (active &&
            gameManager.gameStarted &&
            !gameManager.gameOver &&
            Time.time >= prevTime + interval &&
            totalEnemies < maxEnemiesAtTheSameTime)
            {
                prevTime = Time.time;
                SpawnRandomEnemy();
            }

        if(enemyPointsLeft <= 0)
        {
            active = false;
        }

        if (!active && totalEnemies <= 0)
        {
            active = true;
            currentPhaseEnemyValue = currentPhaseEnemyValue + phasePointValue;
            currentEnemyValueAddition++;
            enemyPointsLeft = currentPhaseEnemyValue;
            valueSpawned = 0;
            interval *= .9f;
        }
    }

    void SpawnRandomEnemy()
    {
        int randomVal;
        int randomMax = enemyType.Length;
        randomVal = UnityEngine.Random.Range(0, randomMax);
        Vector3 spawnPosition = CalculatePointOnSpawnPerimeter();
        GameObject newEnemy = myPool[randomVal].GetPooledObject();

        if (newEnemy != null)
        {
            newEnemy.transform.position = spawnPosition;
            newEnemy.transform.rotation = Quaternion.identity;
            newEnemy.SetActive(true);
            newEnemy.GetComponent<EnemyController>().mySpawner = this;
            totalEnemies++;
            enemyPointsLeft -= enemyPointValues[randomVal];
        }

    }

    void CreatePools()
    { 
        // Pools for enemies
        myPool = new Pool[enemyType.Length];
        for (int i = 0; i < enemyType.Length; i++)
        {
            enemyMaxAmounts.Add(enemyType[i].GetComponent<EnemyController>().maximumAmount);
            enemySpawnAmounts.Add(0);

            myPool[i] = GameObjectDependencyManager.instance.GetGameObject("PoolHandler").GetComponent<PoolHandler>().GetPool(enemyType[i].gameObject.name, PoolType.NormalPool);        
            myPool[i].PopulatePool(enemyType[i], enemyMaxAmounts[i]);
        }
    }

    Vector3 CalculatePointOnSpawnPerimeter()
    {
        // Calculates random position between 0 and rectangle edge size and then "folds the rectangle" around the position

        float spawnPerimeterSize = (spawnPerimeterX + spawnPerimeterZ) * 2;
        float randomNumber = UnityEngine.Random.Range(0, spawnPerimeterSize);
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

    public void EnemyDied()
    {
        // Currently has only infinite spawner system active.

        // I don't like this.
        gameManager.score++;
        gameManager.UpdateUI();

        totalEnemies--;
        

        totalEnemyPointValue += currentEnemyValueAddition;       
    }
}
