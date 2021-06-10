using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected Sprite[] sprites;
    [SerializeField] protected float health;

    [HideInInspector] public Vector3 dir;

    private Vector3 lastPosition;
    private SpriteRenderer spriteRender;
    private Animator anim;
    private BulletController bullet;
    private Rigidbody2D rb;
    private WeaponDrops gameManager;

    void Awake()
    {
        lastPosition = Vector3.zero;
        spriteRender = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        gameManager = GameObject.Find("GameManager").GetComponent<WeaponDrops>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (health <= 0)
        {
            gameManager.deadEnemy(transform);
            Destroy(gameObject);
        }
        Animate();
    }

    void Animate()
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
}
