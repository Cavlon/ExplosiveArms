using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUpgrade : Pickup
{
    private HealthUI healthUI;

    public override void Awake()
    {
        base.Awake();
        healthUI = enemyDeath.GetComponent<HealthUI>();
    }

    public override void Action()
    {
        player.maxHealth += 1;
        player.health += 1;
        healthUI.UpdateHealth();
    }
}
