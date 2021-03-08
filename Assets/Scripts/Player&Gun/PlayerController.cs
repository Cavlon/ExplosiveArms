using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float health;
    public Camera cam;
    private PlayerMovement movement;
    private BulletController bullet;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        movement = GetComponent<PlayerMovement>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyBullet")
        {
            movement.knockback = true;
            bullet = collision.gameObject.GetComponent<BulletController>();
            health -= bullet.damage;
            rb.AddForce(bullet.dir * bullet.thrust, ForceMode2D.Impulse);
            Destroy(bullet.gameObject);
            BulletController[] bullets = FindObjectsOfType<BulletController>();
            foreach(var bullet in bullets)
            {
                Destroy(bullet.gameObject);
            }
        }
        if (collision.CompareTag("Explosion"))
        {
            movement.knockback = true;
            Explosion explosion = collision.gameObject.GetComponent<Explosion>();
            health -= explosion.damage;
            Vector2 dir = transform.position - collision.gameObject.transform.position;
            rb.AddForce(dir * explosion.thrust, ForceMode2D.Impulse);
        }
    }
}
