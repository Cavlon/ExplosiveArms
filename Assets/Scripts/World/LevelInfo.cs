using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo : MonoBehaviour
{
    public Transform[] enemies;
    [HideInInspector] public List<Vector3> spawners;
    public int[] enemyAmounts;
    public int waves;
    private EnemySpawning spawning;
    public EndLevel endPad;
    private SpawnLevel spawnLevel;
    public bool endLevel;

    void Awake()
    {
        spawning = GameObject.Find("GameManager").GetComponent<EnemySpawning>();
        spawnLevel = spawning.GetComponent<SpawnLevel>();
        GetSpawners();
        spawning.LevelInfo(this);
        endPad = GetComponentInChildren<EndLevel>();
        endPad.gameObject.SetActive(false);
        endLevel = false;        
    }

    private void Update()
    {
        if (spawning.levelComplete)
        {
            endPad.gameObject.SetActive(true);
        }
        if (endLevel)
        {
            spawnLevel.NewLevel();
            spawning.levelComplete = false;
            Destroy(gameObject);
        }
    }

    public void GetSpawners()
    {
        Transform[] spawnertrans;
        Transform spawnerParent = transform.GetChild(0);
        spawnertrans = spawnerParent.GetComponentsInChildren<Transform>();
        for (int i = 0; i < spawnertrans.Length; i++)
        {
            spawners.Add(spawnertrans[i].position);
        }
        spawners.RemoveAt(0);
        
    }
}
