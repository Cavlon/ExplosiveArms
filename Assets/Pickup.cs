using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    protected PlayerController player;
    protected EnemyDeath enemyDeath;

    public virtual void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        enemyDeath = GameObject.Find("GameManager").GetComponent<EnemyDeath>();
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Action();
            Destroy(gameObject);
        }
    }

    public abstract void Action();
}
