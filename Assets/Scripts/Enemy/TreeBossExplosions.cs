using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBossExplosions : MonoBehaviour
{
    private Transform playerTrans;
    private TreeBoss enemy;
    private Effect spawnWarning;
    private Explosion explosion;
    private bool canSpawn;
    private int counter;
    private int destroyCounter;
    private Vector2 initialPos;
    private Animator anim;

    private void Update()
    {
        if (canSpawn && counter > 0)
        {
            counter -= 1;
            canSpawn = false;
            anim.SetTrigger("Explosions");
            initialPos = playerTrans.position;
            Instantiate(spawnWarning, initialPos, Quaternion.identity);
            StartCoroutine(SpawnExplosion(initialPos));
            Invoke("Timer", .5f);
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
        playerTrans = enemy.playerTrans;
        spawnWarning = enemy.spawnWarning;
        explosion = enemy.explosion;
        canSpawn = true;
        counter = enemy.explosionAmount;
        destroyCounter = enemy.explosionAmount;
        anim = enemy.anim;
    }

    private IEnumerator SpawnExplosion(Vector2 pos)
    {
        yield return new WaitForSeconds(.5f);
        Instantiate(explosion, pos, Quaternion.identity);
        destroyCounter -= 1;
    }

    private void Timer()
    {
        canSpawn = true;
    }
}
