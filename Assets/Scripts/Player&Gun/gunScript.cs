using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunScript : weaponScript
{

    [SerializeField] protected bool isAuto;
    [SerializeField] protected BulletController bullet;

    public override void Attack()
    {
        BulletController newBullet = Instantiate(bullet, attackPoint.position, attackPoint.rotation);
        newBullet.tag = "Bullet";
        newBullet.speed = attackSpeed;
        newBullet.damage = attackDamage;
        newBullet.thrust = attackThrust;
        slideCamShake.ShakeCamera(camShakeIntensity, camShakeTime);
        playerCamShake.ShakeCamera(camShakeIntensity, camShakeTime);
        if (!isAuto)
        {
            canAttack = false;
        }
    }
}
