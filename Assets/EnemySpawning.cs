using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{

    private Transform[] enemies;
    private Vector3[] spawners;
    private int[] enemyAmounts;
    private int waves;
    [HideInInspector] public int currentEnemies;
    private int waveSize;
    private int totalEnemies;
    public bool levelComplete;
    private List<int> enemyAmountsList = new List<int>();
    private List<Transform> availableEnemies = new List<Transform>();
    private List<Vector3> availableSpawners = new List<Vector3>();

    void Update()
    {
        if (waves == 0)
        {
            levelComplete = true;
        } else if (currentEnemies == 0 )
        {
            waves -= 1;
            currentEnemies = waveSize;
            List<Vector3> randomSpawners = new List<Vector3>();
            List<Transform> randomEnemies = new List<Transform>();
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
            }
            for (int i = 0; i < waveSize; i++)
            {
                Instantiate(randomEnemies[i], randomSpawners[i], Quaternion.identity);
            }
            ResetSpawners();
        }
        
    }

    public void LevelInfo(LevelInfo info)
    {
        availableSpawners = info.spawners;
        spawners = info.spawners.ToArray();
        enemyAmounts = info.enemyAmounts;
        waves = info.waves;
        enemies = info.enemies;
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
    }

    void ResetSpawners()
    {
        availableSpawners.Clear();
        for (int i = 0; i < spawners.Length; i++)
        {
            availableSpawners.Add(spawners[i]);
        }
    }
}
