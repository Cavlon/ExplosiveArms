using UnityEngine;

public class SeedlingChase : State
{

    private Seedling enemy;
    private float distance;
    private float rageDistance;
    private Transform playerTrans;
    private Transform trans;
    private float speed;

    public SeedlingChase(Seedling enemy) 
    {
        this.enemy = enemy;
    }

    public override void Tick()
    {
        distance = Vector2.Distance(trans.position, playerTrans.position);
        if (distance > rageDistance || enemy.rage)
        {
            trans.position = Vector2.MoveTowards(trans.position, playerTrans.position, speed * Time.deltaTime);
        } else
        {
            if (!enemy.rage)
            {
                enemy.enrage = true;
                enemy.speed += 2;
            }             
        }
    }

    public override void OnStateEnter()
    {
        enemy.transTime = 20;
        rageDistance = enemy.rageDistance;
        playerTrans = enemy.playerTrans;
        trans = enemy.trans;
        speed = enemy.speed;
    }
}
