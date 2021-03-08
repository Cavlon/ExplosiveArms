using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    private float distance;
    private float stoppingDistance;
    private float retreatDistance;
    private Transform playerTrans;
    private Transform trans;
    private float speed;
    private int circleDir;
    private EnemyGun gun;

    public AttackState(Enemy enemy) : base(enemy) { }

    public override void Tick()
    {
        distance = Vector2.Distance(trans.position, playerTrans.position);
        if (distance > stoppingDistance)
        {
            trans.position = Vector2.MoveTowards(trans.position, playerTrans.position, speed * Time.deltaTime);
        }
        else if (distance < stoppingDistance && distance > retreatDistance)
        {
            if (circleDir != 0)
            {
                var initialRotation = trans.rotation;
                trans.RotateAround(playerTrans.position, Vector3.forward, speed * 4 * circleDir * Time.deltaTime);
                trans.rotation = initialRotation;
            }
        }
        else if (distance < retreatDistance)
        {
            trans.position = Vector2.MoveTowards(trans.position, playerTrans.position, -speed * Time.deltaTime);
        }
        if (enemy.CanSeePlayerCollision(trans) == false)
        {
            enemy.SetState(new ChaseState(enemy));
        } else
        {
            gun.canShoot = true;
        }

    }

    public override void OnStateEnter()
    {
        stoppingDistance = enemy.stoppingDistance;
        retreatDistance = enemy.retreatDistance;
        playerTrans = enemy.playerTrans;
        trans = enemy.transform;
        speed = enemy.speed;
        circleDir = Random.Range(-1, 2);
        gun = enemy.gun;
        gun.canShoot = true;
    }
}
