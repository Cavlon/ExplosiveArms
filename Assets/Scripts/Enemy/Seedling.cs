using UnityEngine;

public class Seedling : EnemyController
{

    public float rageDistance;
    public int rageTime;
    [HideInInspector] public bool enrage;
    private AnimVariable rageChecker;
    [HideInInspector] public bool rage;
    [SerializeField] protected Explosion Explosion;
    private bool enrageAnim;

    public void Awake()
    {
        GetVariables();
        enrage = false;
        rage = false;
        SetState(new SeedlingChase(this));
        currentStateS = "Chase";
        rageChecker = GetComponentInChildren<AnimVariable>();
    }
 
    void Update()
    {
        if (!enrage)
        {
            currentState.Tick();
            FindState();           
        } else
        {
            if (!enrageAnim)
            {
                anim.SetTrigger("Enrage");
                enrageAnim = true;
            }           
            rage = rageChecker.variable;
        }

        if (rage)
        {
            enrage = false;
            anim.SetBool("Rage", true);
        }        
        Animate();
    }

    private void FindState()
    {
        if (CanSeePlayerCollision(trans) && currentStateS != "Chase")
        {
            transTime -= 1;
            if (transTime <= 0)
            {
                SetState(new SeedlingChase(this));
                currentStateS = "Chase";
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

    public override void Animate()
    {
        dir = (transform.position - lastPosition) / Time.deltaTime;
        if (dir.magnitude > 0.2f)
        {
            anim.SetBool("Running", true);
            if (dir.y > 0)
            {
                anim.SetBool("FacingDown", false);            
            }
            else
            {
                anim.SetBool("FacingDown", true);                              
            }
        }
        else
        {
            anim.SetBool("Running", false);
        }
        lastPosition = transform.position;
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    public override void OnDestroy()
    {
        gameManager.deadEnemy(trans, score);
        Instantiate(Explosion, (Vector2)trans.position + Explosion.offset, Quaternion.identity);
    }   
}
