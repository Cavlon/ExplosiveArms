using UnityEngine;

public class TreeEnemy : EnemyController
{

    public float slamDistance;
    [HideInInspector] public bool slam;
    [HideInInspector] public bool endSlam;
    private AnimVariable slamChecker;
    [SerializeField] protected int slamDelay;
    private float time;
    private bool startTimer;
    [HideInInspector] public bool canSlam;

    public void Awake()
    {
        GetVariables();
        SetState(new TreeAttack(this));
        currentStateS = "Attack";
        slamChecker = GetComponentInChildren<AnimVariable>();
        canSlam = true;
        startTimer = true;
    }

    void Update()
    {

        //Debug.Log(canSlam);
        

        if (!slam)
        {
            currentState.Tick();
            FindState();
        } else
        {
            anim.SetTrigger("Slam");
            
            endSlam = slamChecker.variable;
        }

        if (!canSlam)
        {
            print(time);
            if (startTimer == true)
            {
                time = slamDelay;
                startTimer = false;
            }
            Timer();
        }
        
        if (health <= 0)
        {
            gameManager.deadEnemy(trans, score);
            Destroy(gameObject);
        }
        Animate();

        if (endSlam)
        {
            anim.SetBool("canSlam", false);
            slam = false;
            canSlam = false;
            
        }
    }

    private void FindState()
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
        time -= Time.deltaTime;

        

        if (Mathf.CeilToInt(time) == 0)
        {
            //print(time);
            endSlam = false;
            canSlam = true;
            startTimer = true;
            anim.SetBool("canSlam", true);
        }
    }
}
