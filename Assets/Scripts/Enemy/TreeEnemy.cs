using UnityEngine;

public class TreeEnemy : EnemyController
{

    public float slamDistance;
    [HideInInspector] public bool slam;
    [HideInInspector] public bool endSlam;
    private AnimVariable slamChecker;
    [SerializeField] protected double slamDelay;
    [HideInInspector] public bool canSlam;
    private bool startTimer;

    public void Awake()
    {
        GetVariables();
        SetState(new TreeAttack(this));
        currentStateS = "Attack";
        slamChecker = GetComponentInChildren<AnimVariable>();
        canSlam = true;
        startTimer = false;
    }

    void Update()
    {
        if (!slam)
        {
            currentState.Tick();
            FindState();
        } else
        {
            anim.SetTrigger("Slam");           
            endSlam = slamChecker.variable;
            startTimer = true;
        }
        
        Animate();

        if (endSlam)
        {
            anim.SetBool("canSlam", false);
            slam = false;
            canSlam = false;
            if (startTimer)
            {
                Invoke("Timer", (float)slamDelay);
                startTimer = false;
            }           
        }
    }

    public override void FindState()
    {
        if (CanSeePlayerCollision(trans) && currentStateS != "Attack")
        {
            transTime -= 1;
            if (transTime <= 0)
            {
                SetState(new TreeAttack(this));
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

    private void Timer()
    {
        endSlam = false;
        canSlam = true;
        anim.SetBool("canSlam", true);
    }
}
