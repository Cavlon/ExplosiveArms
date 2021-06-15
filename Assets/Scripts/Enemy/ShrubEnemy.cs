using UnityEngine;

public class ShrubEnemy : EnemyController
{

    public float stoppingDistance;
    public float retreatDistance;

    public void Awake()
    {
        GetVariables();
        SetState(new ShrubAttack(this));
        currentStateS = "Attack";
    }

    public void Update()
    {
        currentState.Tick();
        FindState();
        Animate();
    }

    private void FindState()
    {
        if (CanSeePlayerCollision(trans) && currentStateS != "Attack")
        {
            transTime -= 1;
            if (transTime <= 0)
            {
                SetState(new ShrubAttack(this));
                currentStateS = "Attack";
            }           
        }
        
        if (!CanSeePlayerCollision(trans) && currentStateS != "Pathfind")
        {
            transTime -= 1;
            if (transTime <= 0)
            {
                SetState(new PathfindState(this));
                currentStateS = "Pathfind";
            }              
        }
    }
}
