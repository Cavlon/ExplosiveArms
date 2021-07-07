using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBossSummon : MonoBehaviour
{

    private TreeBoss enemy;
    private int counter;
    private int destroyCounter;
    private EnemyController[] summons = new EnemyController[2];
    private bool canSpawn;
    private Vector2[] spawnerPos = {new Vector2(-7, -4), new Vector2(7, -4)};
    private Vector2 spawnPos;
    private Effect spawnWarning;
    private Animator anim;
    private Transform level;

    public void Update()
    {
        if (counter > 0 && canSpawn)
        {
            counter -= 1;
            canSpawn = false;
            int randVal = Random.Range(0, 2);
            spawnPos = spawnerPos[randVal];
            randVal = Random.Range(0, 70);
            spawnPos.y += randVal / 10;
            anim.SetTrigger("Summon");
            Instantiate(spawnWarning, spawnPos, Quaternion.identity);
            StartCoroutine(SpawnEnemy(spawnPos));
            Invoke("Timer", 0.5f);
        }

        if (counter == 0 && destroyCounter == 0)
        {
            enemy.FindState();
            Destroy(this);
        }
    }

    public void Awake()
    {
        enemy = GetComponent<TreeBoss>();
        level = transform.parent;
        counter = enemy.summonAmount;
        destroyCounter = enemy.summonAmount;
        summons = enemy.enemies;
        canSpawn = true;
        spawnWarning = enemy.spawnWarning;
        anim = enemy.anim;
    }

    private void Timer()
    {
        canSpawn = true;
    }

    private IEnumerator SpawnEnemy(Vector2 pos)
    {
        yield return new WaitForSeconds(1f);
        Instantiate(summons[Random.Range(0, 2)], pos, Quaternion.identity, level);
        destroyCounter -= 1;
    }
}
