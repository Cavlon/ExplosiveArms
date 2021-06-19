using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : Pickup
{
    private Player_Shooting playerShooting;
    [SerializeField] protected Transform weapon;

    public override void Awake()
    {
        base.Awake();
        playerShooting = player.GetComponent<Player_Shooting>();
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" & player.GetComponent<Player_Shooting>().hasGun == false)
        {
            Action();
            Destroy(gameObject);
        }
    }

    public override void Action()
    {
        playerShooting.DestroyMelee();
        Instantiate(weapon, player.transform.position, player.transform.rotation, player.transform);
        enemyDeath.droppedGuns -= 1;
    }
}
