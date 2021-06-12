using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shooting : MonoBehaviour
{
    [SerializeField] WeaponTimer timer;
    [HideInInspector] public gunScript gun;
    public bool hasGun;

    void Awake()
    {
        GetGun();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasGun)
        {
            if ((Input.GetButtonDown("Fire1") || Input.GetAxisRaw("Fire1") > 0.0f) & gun.canFire)
            {
                gun.isFiring = true;
            }

            if (Input.GetButtonUp("Fire1") || Input.GetAxisRaw("Fire1") == 0.0f)
            {
                gun.isFiring = false;
                gun.canFire = true;
            }

            if (Input.GetButtonDown("Throw"))
            {
                gun.throwGun = true;
                hasGun = false;
            }

            if (Input.GetButtonUp("Throw"))
            {
                gun.throwGun = false;
            }         
        }
    }

    public void GetGun()
    {
        gun = transform.GetChild(1).GetComponent<gunScript>();
        hasGun = true;
        timer.startTimer = true;
    }
}
