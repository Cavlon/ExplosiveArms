using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunThrown : MonoBehaviour
{

    public float speed;

    [SerializeField] protected float rotateSpeed;
    [SerializeField] protected Explosion Explosion;
    [SerializeField] protected LayerMask ignoreLayers;
    private Transform sprite;
    private Rigidbody2D rb;

    private void Awake()
    {
        sprite = transform.GetChild(0);
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 3);
    }
    void Update()
    {
        sprite.Rotate(Vector3.back, rotateSpeed * Time.deltaTime, Space.World);
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.right * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!ignoreLayer(collision.gameObject.layer) & collision.tag != "Explosion")
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        Instantiate(Explosion, (Vector2)transform.position + Explosion.offset, Quaternion.identity);
    }

    private bool ignoreLayer(int layer)
    {
        return ignoreLayers == (ignoreLayers | (1 << layer));
    }
}
