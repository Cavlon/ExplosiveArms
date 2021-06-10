using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PathfindState : State
{
    private EnemyController enemy;
    private Transform target;
    private Transform trans;
    private float speed;
    private float nextWaypointDistance = 1f;
    private readonly float initialPathUpdateDelay = 0.5f;
    private float pathUpdateDelay;
    private Path path;
    private int currentWaypoint;
    private float distance;
    private EnemyGun gun;
    private Seeker seeker;
    private bool hasGun;

    public PathfindState(EnemyController enemy) 
    {
        this.enemy = enemy;
    }

    public override void Tick()
    {
        if (path == null)
            return;

        if (currentWaypoint < path.vectorPath.Count)
        {
            trans.position = Vector2.MoveTowards(trans.position, path.vectorPath[currentWaypoint], speed * Time.deltaTime);
            distance = Vector2.Distance(trans.position, path.vectorPath[currentWaypoint]);
        }

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
        if (pathUpdateDelay <= 0)
        {
            pathUpdateDelay = initialPathUpdateDelay;
            if (seeker.IsDone())
                seeker.StartPath(trans.position, target.position, OnPathComplete);
        }

        pathUpdateDelay -= Time.deltaTime;

        if (hasGun)
        {
            gun.canShoot = enemy.CanSeePlayerAttack(trans);
        }       

    }

    public override void OnStateEnter()
    {
        enemy.transTime = 20;
        seeker = enemy.seeker;
        speed = enemy.speed;
        trans = enemy.trans;
        target = enemy.playerTrans;
        hasGun = enemy.hasGun;
        if (hasGun)
        {
            gun = enemy.gun;
            gun.canShoot = false;
        }
        seeker.StartPath(trans.position, target.position, OnPathComplete);
        pathUpdateDelay = initialPathUpdateDelay;
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
}
