using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeScript : weaponScript
{

    private Animator meleeAnim;
    [SerializeField] float range;
    [SerializeField] LayerMask attackLayers;

    public override void Awake()
    {
        base.Awake();
        meleeAnim = GetComponentInChildren<Animator>();
    }

    public override void Attack()
    {
        meleeAnim.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, range, attackLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                print("oof");
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
