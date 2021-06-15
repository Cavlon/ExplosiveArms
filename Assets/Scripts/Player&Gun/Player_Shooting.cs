using Cinemachine;
using UnityEngine;

public class Player_Shooting : MonoBehaviour
{
    [SerializeField] WeaponTimer timer;
    [HideInInspector] public weaponScript weapon;
    private CinemachineVirtualCamera slideCam;
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
            if ((Input.GetButtonDown("Fire1") || Input.GetAxisRaw("Fire1") > 0.0f) & weapon.canAttack)
            {
                weapon.isAttacking = true;
            }

            if (Input.GetButtonUp("Fire1") || Input.GetAxisRaw("Fire1") == 0.0f)
            {
                weapon.isAttacking = false;
                weapon.canAttack = true;
            }

            if (Input.GetButtonDown("Throw"))
            {
                weapon.throwWeapon = true;
                hasGun = false;
            }

            if (Input.GetButtonUp("Throw"))
            {
                weapon.throwWeapon = false;
            }         
        }
        else
        {
            slideCam.Priority = 0;
        }
    }

    public void GetGun()
    {
        weapon = transform.GetChild(1).GetComponent<weaponScript>();
        slideCam = weapon.slideCam;
        hasGun = true;
        timer.startTimer = true;
    }
}
