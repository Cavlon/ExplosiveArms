using UnityEngine;

public class ShrubAttack : State
{
    private Shrub enemy;
    private float distance;
    private float stoppingDistance;
    private float retreatDistance;
    private Transform playerTrans;
    private Transform trans;
    private float speed;
    private int circleDir;
    private EnemyGun gun;

    public ShrubAttack(Shrub enemy) 
    {
        this.enemy = enemy;
    }

    public override void Tick()
    {
        distance = Vector2.Distance(trans.position, playerTrans.position);
        if (distance > stoppingDistance)
        {
            trans.position = Vector2.MoveTowards(trans.position, playerTrans.position, speed * Time.deltaTime);
        }
        else if (distance < stoppingDistance && distance > retreatDistance && circleDir != 0)
        {            
            var initialRotation = trans.rotation;
            trans.RotateAround(playerTrans.position, Vector3.forward, speed * 4 * circleDir * Time.deltaTime);
            trans.rotation = initialRotation;            
        }
        else if (distance < retreatDistance)
        {
            trans.position = Vector2.MoveTowards(trans.position, playerTrans.position, -speed * Time.deltaTime);
        }
        if (enemy.CanSeePlayerCollision(trans))
        {
            gun.canShoot = true;
        }

    }

    public override void OnStateEnter()
    {
        enemy.transTime = 20;
        stoppingDistance = enemy.stoppingDistance;
        retreatDistance = enemy.retreatDistance;
        playerTrans = enemy.playerTrans;
        trans = enemy.trans;
        speed = enemy.speed;
        circleDir = Random.Range(-1, 2);
        gun = enemy.gun;
        gun.canShoot = true;
    }
}
