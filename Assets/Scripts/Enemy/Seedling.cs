using UnityEngine;

public class Seedling : EnemyController
{

    public float rageDistance;
    public int rageTime;
    [HideInInspector] public bool enrage;
    private Enrage rageChecker;
    [HideInInspector] public bool rage;
    [SerializeField] protected Explosion Explosion;

    public void Awake()
    {
        GetVariables();
        enrage = false;
        rage = false;
        SetState(new SeedlingChase(this));
        currentStateS = "Chase";
        rageChecker = GetComponentInChildren<Enrage>();
    }
 
    void Update()
    {
        if (!enrage)
        {
            currentState.Tick();
            FindState();           
        } else
        {
            anim.SetBool("Enrage", true);
            rage = rageChecker.rage;
        }

        if (health <= 0)
        {
            gameManager.deadEnemy(trans);
            Destroy(gameObject);
        }

        if (rage)
        {
            enrage = false;
            anim.SetBool("Enrage", false);
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
        if (collision.CompareTag("Bullet"))
        {
            bullet = collision.gameObject.GetComponent<BulletController>();
            health -= bullet.damage;
            rb.AddForce(bullet.dir * bullet.thrust, ForceMode2D.Impulse);
            Destroy(bullet.gameObject);
        }
        if (collision.CompareTag("Explosion"))
        {
            Explosion explosion = collision.gameObject.GetComponent<Explosion>();
            health -= explosion.damage;
            Vector2 dir = transform.position - collision.gameObject.transform.position;
            rb.AddForce(dir * explosion.thrust, ForceMode2D.Impulse);
        }
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        Instantiate(Explosion, (Vector2)trans.position + Explosion.offset, Quaternion.identity);
    }
}
