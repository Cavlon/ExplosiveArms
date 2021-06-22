using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup
{
    private HealthUI healthUI;

    public override void Awake()
    {
        base.Awake();
        healthUI = enemyDeath.GetComponent<HealthUI>();
    }

    public override void Action()
    {
        player.health += 1;
        enemyDeath.droppedHealth -= 1;
        healthUI.UpdateHealth();
    }
}
