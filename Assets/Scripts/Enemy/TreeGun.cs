using UnityEngine;

public class TreeGun : EnemyGun
{

    private bool slam;
    private bool canSlam;
    private TreeEnemy tree;

    public override void Update()
    {
        base.Update();      
        if (slam)
        {          
            Slam();
        }

        if (canSlam)
        {
            slam = tree.slam;
        } else
        {
            canSlam = tree.endSlam;
        }    
    }

    public override void Shoot()
    {
        BulletController[] newBullets = new BulletController[5];

        float offset = -60;

        for (int i = 0; i < 5; i++)
        {
            Vector3 rotation = trans.rotation.eulerAngles;
            rotation.z += offset;

            newBullets[i] = Instantiate(bullet, firePoint.position, Quaternion.Euler(rotation));
            newBullets[i].tag = "EnemyBullet";
            newBullets[i].GetComponent<SpriteRenderer>().color = newBullets[i].colour = new Color(255, 0, 0, 255);
            newBullets[i].speed = bulletSpeed;
            newBullets[i].damage = bulletDamage;
            newBullets[i].thrust = bulletThrust;
            offset += 30;
        }    
        shotDelay = initialShotDelay;
    }

    public override void GetVariables()
    {
        base.GetVariables();
        tree = trans.parent.GetComponent<TreeEnemy>();
        canSlam = true;
    }

    private void Slam()
    {
        BulletController[] newBullets = new BulletController[10];

        float offset = 0;

        for (int i = 0; i < 10; i++)
        {
            Vector3 rotation = trans.rotation.eulerAngles;
            rotation.z += offset;

            newBullets[i] = Instantiate(bullet, tree.trans.position, Quaternion.Euler(rotation));
            newBullets[i].tag = "EnemyBullet";
            newBullets[i].GetComponent<SpriteRenderer>().color = newBullets[i].colour = new Color(255, 0, 0, 255);
            newBullets[i].speed = bulletSpeed;
            newBullets[i].damage = bulletDamage;
            newBullets[i].thrust = bulletThrust;
            newBullets[i].transform.localScale = new Vector2((float)1.25, (float)1.25);
            offset += 36;
        }
        shotDelay = initialShotDelay;
        canSlam = false;
        slam = false;
    }
}
