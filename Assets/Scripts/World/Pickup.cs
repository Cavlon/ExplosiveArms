using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    protected PlayerController player;
    protected EnemyDeath enemyDeath;
    protected bool actionDone;

    public virtual void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        enemyDeath = GameObject.Find("GameManager").GetComponent<EnemyDeath>();
        actionDone = false;
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!actionDone)
            {
                Action();
                actionDone = true;
            }           
            Destroy(gameObject);
        }
    }

    public abstract void Action();
}
