using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeAttack : weaponScript
{
    [SerializeField] protected float range;
    [SerializeField] protected LayerMask attackLayers;
    [SerializeField] protected Effect meleeEffect;
    private GameObject gameManager;

    public override void Awake()
    {
        base.Awake();
        gameManager = GameObject.Find("GameManager");
    }

    public override void Attack()
    {
        Vector3 rotation = attackPoint.rotation.eulerAngles;
        rotation.z -= 90;

        Instantiate(meleeEffect, attackPoint.position, Quaternion.Euler(rotation));
        gameManager.GetComponent<DetectControlMethod>().GetWeapon();

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, range, attackLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                EnemyController enemyCont = enemy.GetComponent<EnemyController>();
                enemyCont.TakeDamage(attackDamage);
                Vector2 dir = enemy.gameObject.transform.position - transform.position;
                enemyCont.rb.AddForce(dir * attackThrust, ForceMode2D.Impulse);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, range);
    }
}
