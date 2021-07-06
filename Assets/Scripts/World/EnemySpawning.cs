using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{

    [HideInInspector] public int currentEnemies;
    [HideInInspector] public bool levelComplete;
    [HideInInspector] public bool boss;

    [SerializeField] protected Effect spawnEffect;

    private Transform[] enemies;
    private Vector3[] spawners;
    private int[] enemyAmounts;
    private int waves;      
    private int waveSize;
    private int totalEnemies;    
    private List<int> enemyAmountsList = new List<int>();
    private List<Transform> availableEnemies = new List<Transform>();
    private List<Vector3> availableSpawners = new List<Vector3>();   

    List<Vector3> randomSpawners = new List<Vector3>();
    List<Transform> randomEnemies = new List<Transform>();
    LevelInfo currentLevel;

    void Update()
    {
        if (waves == 0 && currentEnemies <= 0 && !boss)
        {
            levelComplete = true;
        } else if (currentEnemies <= 0 && waves != 0)
        {
            waves -= 1;
            currentEnemies = waveSize;
            randomSpawners.Clear();
            randomEnemies.Clear();
            for (int i = 0; i < waveSize; i++)
            {
                int x = Random.Range(0, availableSpawners.Count);               
                randomSpawners.Add(availableSpawners[x]);
                availableSpawners.RemoveAt(x);


                int y = Random.Range(0, enemyAmountsList.Count);               
                randomEnemies.Add(availableEnemies[y]);
                enemyAmountsList[y] -= 1;

                if (enemyAmountsList[y] == 0)
                {
                    enemyAmountsList.RemoveAt(y);
                    availableEnemies.RemoveAt(y);
                }
                Instantiate(spawnEffect, randomSpawners[i], Quaternion.identity);
            }
            Invoke("SpawnEnemies", 1f);           
        }
        
    }

    public void LevelInfo(LevelInfo info)
    {
        totalEnemies = 0;
        spawners = info.spawners.ToArray();      
        enemyAmounts = info.enemyAmounts;
        waves = info.waves;        
        enemies = info.enemies;
        currentLevel = info;
        for (int i = 0; i < enemyAmounts.Length; i++)
        {
            totalEnemies += enemyAmounts[i];
        }
        waveSize = totalEnemies / waves;
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemyAmounts[i] > 0)
            {
                enemyAmountsList.Add(enemyAmounts[i]);
                availableEnemies.Add(enemies[i]);
            }
        }       
        ResetSpawners();
    }

    void ResetSpawners()
    {
        availableSpawners.Clear();      
        for (int i = 0; i < spawners.Length; i++)
        {
            availableSpawners.Add(spawners[i]);         
        }
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < waveSize; i++)
        {
            Instantiate(randomEnemies[i], randomSpawners[i], Quaternion.identity, currentLevel.transform);
        }
        ResetSpawners();
    }
}
