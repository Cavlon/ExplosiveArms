
using UnityEngine;

public class TreeAttack : State
{

    private TreeEnemy enemy;
    private float distance;
    private Transform playerTrans;
    private Transform trans;
    private float speed;
    private EnemyGun gun;

    public TreeAttack(TreeEnemy enemy)
    {
        this.enemy = enemy;
    }

    public override void Tick()
    {
        distance = Vector2.Distance(trans.position, playerTrans.position);
        if (distance > enemy.slamDistance)
        {
            trans.position = Vector2.MoveTowards(trans.position, playerTrans.position, speed * Time.deltaTime);
        } else if (enemy.canSlam)
        {
            
            enemy.endSlam = false;
            enemy.slam = true;
        }
      
        if (enemy.CanSeePlayerCollision(trans))
        {
            gun.canShoot = true;
        }
    }

    public override void OnStateEnter()
    {
        enemy.transTime = 20;
        playerTrans = enemy.playerTrans;
        trans = enemy.trans;
        speed = enemy.speed;
        gun = enemy.gun;
        gun.canShoot = true;
    }
}
