using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float health;
    [SerializeField] protected Sprite[] sprites;
    [SerializeField] protected float range;
    [SerializeField] protected LayerMask myLayerMask;
    [SerializeField] protected LayerMask myLayerMaskAttack;

    public float speed;
    public float stoppingDistance;
    public float retreatDistance;
    public Transform playerTrans;
    public EnemyGun gun;

    private WeaponDrops gameManager;
    private GameObject player;
    private BulletController bullet;
    private Vector3 lastPosition;
    private SpriteRenderer spriteRender;
    private Animator anim;
    private State currentState;
    private BoxCollider2D colliderComponent;
    private Rigidbody2D rb;
    public Seeker seeker;


    public void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<WeaponDrops>();
        rb = GetComponent<Rigidbody2D>();
        colliderComponent = GetComponent<BoxCollider2D>();
        player = GameObject.FindWithTag("Player");
        playerTrans = player.transform;
        lastPosition = Vector3.zero;
        spriteRender = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        seeker = GetComponent<Seeker>();
        SetState(new AttackState(this));
    }

    // Update is called once per frame
    public void Update()
    {
        if (health <= 0)
        {
            gameManager.deadEnemy(transform);
            Destroy(gameObject);
        }
        currentState.Tick();
        Animate();
    }

    public void OnTriggerEnter2D(Collider2D collision)
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

    void Animate()
    {
        Vector3 dir = (transform.position - lastPosition) / Time.deltaTime;
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

        RaycastHit2D hit = Physics2D.Linecast((Vector2)trans.position, endPos, myLayerMaskAttack);

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
}
