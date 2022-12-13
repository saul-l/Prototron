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
using UnityEditor;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameManager gameManager;
    [HideInInspector] public Pool[] myPool;
    [SerializeField] private Transform spawnPerimeter;
    [SerializeField] private GameObject[] enemyType;
    [Range(1, 100)]
    [SerializeField] private int[] enemyValues;
    [SerializeField] private float totalEnemyValue;
    [SerializeField] private float interval = 3.0f;
    [SerializeField] private List<int> enemyOrder;
    [SerializeField] private int minimumEnemiesBeforeNextPhase = 0;
    [SerializeField] private int totalEnemies;
    [SerializeField] private int maxEnemiesAtTheSameTime = 100;
    [SerializeField] private int enemyCounter = 0;

    

    private float prevTime = 0.0f;

    private int requestedPoolSize = 50;
    private int enemiesKilled;
    private int enemiesAlive;

    private float spawnPerimeterX;
    private float spawnPerimeterZ;
    private float spawnPerimeterY;
    private Vector3 spawnPerimeterCenter;


    [SerializeField] GameObject nextSpawner;

    private void Awake()
    {
        gameManager = GameObjectDependencyManager.instance.GetGameObject("GameManager").GetComponent<GameManager>();
    }
    void Start()
    {
      
        Array.Sort(enemyValues, enemyType);

        spawnPerimeterCenter = spawnPerimeter.position;
        spawnPerimeterX = spawnPerimeter.localScale.x;
        spawnPerimeterY = spawnPerimeter.position.y;
        spawnPerimeterZ = spawnPerimeter.localScale.z;

        CreateEnemyOrder();
        CreatePools();
    }

    void Update()
    {
        if (gameManager.gameStarted && !gameManager.gameOver && enemyCounter < enemyOrder.Count && Time.time >= prevTime + interval && totalEnemies < maxEnemiesAtTheSameTime)
        {
            prevTime = Time.time;
            SpawnEnemy();
        }
    }


    void CreateEnemyOrder()
    {
        int[] enemyAmount = new int[enemyType.Length];

        int randomVal;
        int randomMax = enemyType.Length;
        //randomize enemies to enemyOrder[] 
        while (totalEnemyValue >= enemyValues[0] && randomMax > 0)
        {
            randomVal = UnityEngine.Random.Range(0, randomMax);

            // If enemy value exceeds total value left we need to decrease randomMax until we don't hit the ceiling

            if (enemyValues[randomVal] > totalEnemyValue)
            {

                while (enemyValues[randomMax - 1] > totalEnemyValue)
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
    }

    void CreatePools()
    { 
        // Pools for enemies
        myPool = new Pool[enemyType.Length];

        for (int i = 0; i < enemyType.Length; i++)
        {
            myPool[i] = GameObjectDependencyManager.instance.GetGameObject("PoolHandler").GetComponent<PoolHandler>().GetPool(enemyType[i].gameObject.name, PoolType.NormalPool);
            
            myPool[i].PopulatePool(enemyType[i], requestedPoolSize);
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
    void SpawnEnemy()
    {
        Vector3 spawnPosition = CalculatePointOnSpawnPerimeter();

        GameObject newEnemy = myPool[enemyOrder[enemyCounter]].GetPooledObject();
        
        if (newEnemy != null)
        {
            newEnemy.transform.position = spawnPosition;
            newEnemy.transform.rotation = Quaternion.identity;
            newEnemy.SetActive(true);
            newEnemy.GetComponent<EnemyController>().mySpawner = this;
            totalEnemies++;
            enemyCounter++;
        }
        else Debug.Log("ENEMY NULL!");
    }

    public void EnemyDied()
    {
        // Currently has only infinite spawner system active.

        // I don't like this.
        gameManager.score++;
        gameManager.UpdateUI();

        totalEnemies--;
        
        enemiesKilled++;
        totalEnemyValue += 3;


        CreateEnemyOrder(); // This is a bit hardcore maybe.

        /* Sequential spawner things.
        if (nextSpawner != null)
        {
            if (totalEnemies <= minimumEnemiesBeforeNextPhase)
            {
                nextSpawner.SetActive(true);
            }
        }
        */
    }
}
