using System.Collections.Generic;
using UnityEngine;

public class SpawnLevel : MonoBehaviour
{
    [SerializeField] int levelRequirement;
    [SerializeField] Effect spawnEffect;
    public List<LevelInfo> levels = new List<LevelInfo>();
    public LevelInfo bossLevel;
    public int levelNo;
    [HideInInspector] public Transform player;
    private PlayerMovement playerMove;
    private LevelInfo currentLevel;

    void Start()
    {
        levelNo = 0;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerMove = player.GetComponent<PlayerMovement>();
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
            currentLevel = Instantiate(bossLevel);
            GetComponent<EnemySpawning>().boss = true;
        }
        playerMove.Stop();
        playerMove.knockback = true;
        Instantiate(spawnEffect, (Vector2)player.position + (Vector2.up * 2.25f), Quaternion.identity);
        Invoke("ReScan", 1f);
    }

    private void ReScan()
    {
        AstarPath.active.Scan();
    }

    private void NormalLevel()
    {
        int randVal = Random.Range(0, levels.Count);
        currentLevel = Instantiate(levels[randVal]);
        levels.RemoveAt(randVal);
    }

    public void DeleteLevel()
    {
        Destroy(currentLevel.gameObject);
    }
}
