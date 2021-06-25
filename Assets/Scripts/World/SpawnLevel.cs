using System.Collections.Generic;
using UnityEngine;

public class SpawnLevel : MonoBehaviour
{
    [SerializeField] int levelRequirement;
    public List<LevelInfo> levels = new List<LevelInfo>();
    public LevelInfo bossLevel;
    private int levelNo;
    private Transform player;

    void Start()
    {
        levelNo = 0;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        NewLevel();
    }


    public void NewLevel()
    {
        if (levelNo < levelRequirement)
        {
            NormalLevel();
            levelNo += 1;
        }
        else
        {
            player.position = new Vector2(0, -15);
            Instantiate(bossLevel);
            GetComponent<EnemySpawning>().boss = true;
        }  
        Invoke("ReScan", 1f);
    }

    private void ReScan()
    {
        AstarPath.active.Scan();
    }

    private void NormalLevel()
    {
        int randVal = Random.Range(0, levels.Count);
        Instantiate(levels[randVal]);
        levels.RemoveAt(randVal);
    }
}
