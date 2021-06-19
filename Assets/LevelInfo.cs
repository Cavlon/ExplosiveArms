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

    void Awake()
    {
        spawning = GameObject.Find("GameManager").GetComponent<EnemySpawning>();
        GetSpawners();
        spawning.LevelInfo(this);
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
