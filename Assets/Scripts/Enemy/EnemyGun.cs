using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public BulletController bullet;
    public Transform firePoint;
    public bool canShoot;

    [SerializeField] protected float initialShotDelay;
    [SerializeField] protected float bulletSpeed;
    [SerializeField] protected int bulletDamage;
    [SerializeField] protected float bulletThrust;

    [HideInInspector] public Transform trans;
    private GameObject player;
    [HideInInspector] public float shotDelay;
    private SpriteRenderer gunSprite;
    private SpriteRenderer enemySprite;
    private float angle;

    // Start is called before the first frame update
    void Start()
    {
        GetVariables();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        Rotate();
        if (canShoot)
        {
            if (shotDelay <= 0)
            {
                Shoot();
            }
            else
            {
                shotDelay -= Time.deltaTime;
            }
        }
        SortingLayer();
    }

    public void Rotate()
    {
        Vector2 pos = new Vector2(trans.position.x, trans.position.y);
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
        Vector2 lookDir = playerPos - pos;
        angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        if (angle > 90 || angle < -90)
        {
            trans.localScale = new Vector3(1, -1, 1);
            enemySprite.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            trans.localScale = new Vector3(1, 1, 1);
            enemySprite.transform.localScale = new Vector3(-1, 1, 1);
        }
        Quaternion rotate = Quaternion.Euler(0, 0, angle);
        trans.rotation = rotate;
    }

    public void SortingLayer()
    {
        if (angle > 0)
        {
            gunSprite.sortingOrder = enemySprite.sortingOrder - 1;
        } else
        {
            gunSprite.sortingOrder = enemySprite.sortingOrder + 1;
        }
    }

    public virtual void GetVariables()
    {
        trans = GetComponent<Transform>();
        player = GameObject.FindWithTag("Player");
        shotDelay = initialShotDelay;
        gunSprite = GetComponentInChildren<SpriteRenderer>();
        enemySprite = trans.parent.GetComponentInChildren<SpriteRenderer>();
    }

    public virtual void Shoot()
    {
        BulletController newBullet = Instantiate(bullet, firePoint.position, trans.rotation);
        newBullet.tag = "EnemyBullet";
        newBullet.GetComponent<SpriteRenderer>().color = newBullet.colour = new Color(255, 0, 0, 255);
        newBullet.speed = bulletSpeed;
        newBullet.damage = bulletDamage;
        newBullet.thrust = bulletThrust;
        shotDelay = initialShotDelay;
    }
}
