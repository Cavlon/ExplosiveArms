using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{
    
    
    [SerializeField] protected float range;
    [SerializeField] protected LayerMask myLayerMask;
    [SerializeField] protected LayerMask myLayerMaskAttack;
    [SerializeField] protected float health;    
    [SerializeField] protected Sprite[] sprites;
    [SerializeField] protected EffectDestroy deathEffect;
    

    protected State currentState;
    protected string currentStateS;  
    protected EnemyDeath gameManager;
    protected Vector3 lastPosition;
    protected SpriteRenderer spriteRender;   
    protected Vector3 dir;
    protected BulletController bullet; 
    
    [HideInInspector] public Animator anim;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public int transTime;
    [HideInInspector] public Transform trans;
    [HideInInspector] public Transform playerTrans;       
    [HideInInspector] public Seeker seeker;
    [HideInInspector] public EnemyGun gun;

    public bool hasGun;
    public float speed;
    public int score;   

    private GameObject player;       
    private BoxCollider2D colliderComponent;

    public void GetVariables()
    {
        colliderComponent = GetComponent<BoxCollider2D>();
        player = GameObject.FindWithTag("Player");
        playerTrans = player.transform;
        seeker = GetComponent<Seeker>();
        trans = transform;
        lastPosition = Vector3.zero;
        spriteRender = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        gameManager = GameObject.Find("GameManager").GetComponent<EnemyDeath>();
        rb = GetComponent<Rigidbody2D>();
        if (hasGun)
        {
            gun = GetComponentInChildren<EnemyGun>();
        }
    }

    public void SetState(State state)
    {
        if (currentState != null)
        {
            currentState.OnStateExit();
        }
        currentState = state;
        if (currentState != null)
        {
            currentState.OnStateEnter();
        }
    }

    public bool CanSeePlayerCollision(Transform trans)
    {
        Vector2 dir = (playerTrans.position - trans.position).normalized;
        Vector2 endPos = (Vector2)trans.position + (dir * range);

        RaycastHit2D hit = Physics2D.Linecast((Vector2)trans.position + colliderComponent.offset, endPos, myLayerMask);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                Debug.DrawLine(trans.position, endPos, Color.red);
                return true;
            }
        }
        Debug.DrawLine(trans.position, endPos, Color.blue);
        return false;        
    }

    public bool CanSeePlayerAttack(Transform trans)
    {
        Vector2 dir = (playerTrans.position - trans.position).normalized;
        Vector2 endPos = (Vector2)trans.position + (dir * range);

        RaycastHit2D hit = Physics2D.Linecast(trans.position, endPos, myLayerMaskAttack);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                Debug.DrawLine(trans.position, endPos, Color.green);
                return true;
            }
        }
        Debug.DrawLine(trans.position, endPos, Color.blue);
        return false;
    }

    public virtual void Animate()
    {
        dir = (transform.position - lastPosition) / Time.deltaTime;
        if (dir.magnitude > 0.2f)
        {
            anim.SetBool("Running", true);
            if (dir.y > 0)
            {
                spriteRender.sprite = sprites[1];
            }
            else
            {
                spriteRender.sprite = sprites[0];
            }
        }
        else
        {
            anim.SetBool("Running", false);
        }
        lastPosition = transform.position;
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            bullet = collision.gameObject.GetComponent<BulletController>();
            TakeDamage(bullet.damage, 1);
            rb.AddForce(bullet.dir * bullet.thrust, ForceMode2D.Impulse);
            Destroy(bullet.gameObject);
            
        }
        if (collision.CompareTag("Explosion"))
        {
            Explosion explosion = collision.gameObject.GetComponent<Explosion>();
            TakeDamage(explosion.damage, 2);
            Vector2 dir = transform.position - collision.gameObject.transform.position;
            rb.AddForce(dir * explosion.thrust, ForceMode2D.Impulse);
        }
    }

    public virtual void TakeDamage(float damage, double scoreMultiplier)
    {
        health -= damage;
        if (health <= 0)
        {
            score = (int)(score * scoreMultiplier);
            gameManager.deadEnemy(trans, score);
            EffectDestroy instance = Instantiate(deathEffect, trans.position, Quaternion.identity);
            instance.transform.localScale = trans.localScale;
            Destroy(gameObject);
        }
    }

    public abstract void FindState();
}
