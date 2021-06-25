using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBoss : EnemyController
{

    public EnemyController[] enemies;
    public Effect spawnWarning;
    public Explosion explosion;
    public int explosionAmount;
    public int summonAmount;
    private EnemySpawning enemySpawning;

    void Awake()
    {
        GetVariables();
        enemySpawning = gameManager.GetComponent<EnemySpawning>();
        gameObject.AddComponent<TreeBossSummon>();
        currentStateS = "Summon";
    }

    public override void FindState()
    {
        int randVal = Random.Range(0, 2);
        if (randVal == 0 && currentStateS != "Summon")
        {
            gameObject.AddComponent<TreeBossSummon>();
            currentStateS = "Summon";
        } else
        {
            gameObject.AddComponent<TreeBossExplosions>();
            currentStateS = "Explosions";
        }
    }

    public override void TakeDamage(float damage, double scoreMultiplier)
    {
        health -= damage;
        if (health <= 0)
        {
            score = (int)(score * scoreMultiplier);
            gameManager.deadEnemy(trans, score);
            enemySpawning.boss = false;
            EffectDestroy instance = Instantiate(deathEffect, trans.position, Quaternion.identity);
            instance.transform.localScale = trans.localScale;
            Destroy(gameObject);
        }
    }
}
