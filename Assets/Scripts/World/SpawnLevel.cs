using System.Collections.Generic;
using UnityEngine;

public class SpawnLevel : MonoBehaviour
{
    [HideInInspector] public Transform player;

    public List<LevelInfo> levels = new List<LevelInfo>();
    public LevelInfo bossLevel;
    public int levelNo;

    [SerializeField] int levelRequirement;
    [SerializeField] Effect spawnEffect;
   
    private PlayerMovement playerMove;
    private LevelInfo currentLevel;
    private GameOver gameOver;
    private bool boss;

    void Start()
    {
        levelNo = 0;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerMove = player.GetComponent<PlayerMovement>();
        gameOver = GetComponent<GameOver>();
        NewLevel();
    }


    public void NewLevel()
    {
        if (levelNo < levelRequirement && !boss)
        {
            NormalLevel();
            levelNo += 1;
        }
        else if (!boss)
        {
            player.position = new Vector2(0, -15);
            currentLevel = Instantiate(bossLevel);
            GetComponent<EnemySpawning>().boss = true;
            boss = true;
        } else
        {
            gameOver.EndGame(false);
            boss = false;
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
