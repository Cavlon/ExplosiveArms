using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] protected ParticleSystem bulletBurst;
    public Color32 colour;
    public float speed;
    public int damage;
    public float thrust;
    public Vector2 dir;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 2);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.right * speed * Time.deltaTime;
        dir = rb.velocity.normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            Destroy(gameObject);
        }
    }

    public void OnDestroy()
    {
        ParticleSystem burst = Instantiate(bulletBurst, transform.position, Quaternion.identity);
        ParticleSystem.MainModule ma = burst.main;
        ma.startColor = new ParticleSystem.MinMaxGradient(colour);
        Destroy(burst.gameObject, .3f);
    }
}
