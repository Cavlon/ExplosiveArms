using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int health;
    public int maxHealth;
    private HealthUI healthUI;

    private PlayerMovement movement;
    private BulletController bullet;
    private Rigidbody2D rb;
    private bool invincible;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        movement = GetComponent<PlayerMovement>();
        healthUI = GameObject.Find("GameManager").GetComponent<HealthUI>();
        healthUI.playerCont = this;
        healthUI.getImages();
        healthUI.UpdateHealth();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyBullet")
        {
            movement.knockback = true;
            bullet = collision.gameObject.GetComponent<BulletController>();
            if (!invincible)
            {
                health -= bullet.damage;
                healthUI.UpdateHealth();
                rb.AddForce(bullet.dir * bullet.thrust, ForceMode2D.Impulse);
                invincible = true;
            }
            BulletController[] bullets = FindObjectsOfType<BulletController>();
            foreach(var bullet in bullets)
            {
                Destroy(bullet.gameObject);
            }           
            Invoke("ResetInvincibility", 1f);
        }
        if (collision.CompareTag("Explosion"))
        {
            movement.knockback = true;
            Explosion explosion = collision.gameObject.GetComponent<Explosion>();
            if (!invincible)
            {
                health -= 1;
                healthUI.UpdateHealth();
                invincible = true;
            }           
            Vector2 dir = transform.position - collision.gameObject.transform.position;
            dir = dir.normalized;
            rb.AddForce(dir * explosion.thrust, ForceMode2D.Impulse);        
            Invoke("ResetInvincibility", 1f);
        }
    }

    void ResetInvincibility(){
        invincible = false;
    }
}
