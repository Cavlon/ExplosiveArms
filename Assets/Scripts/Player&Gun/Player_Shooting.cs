using Cinemachine;
using UnityEngine;

public class Player_Shooting : MonoBehaviour
{
    private WeaponTimer timer;
    [SerializeField] meleeAttack melee;
    [HideInInspector] public weaponScript weapon;
    private CinemachineVirtualCamera slideCam;
    public bool hasGun;
    private bool hasMelee;
    private meleeAttack meleeInstance;

    void Awake()
    {
        timer = GameObject.Find("GameManager").GetComponent<WeaponTimer>();
        GetGun();
    }

    void Update()
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

        if (hasGun)
        {
            hasMelee = false;           

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
            if (!hasMelee)
            {
                hasMelee = true;
                CreateMelee();         
            }
        }
        if (weapon == null)
        {
            GetGun();
        }
    }

    public void GetGun()
    {
        weapon = transform.GetComponentInChildren<weaponScript>();
        slideCam = weapon.slideCam;
        if (!hasMelee)
        {
            hasGun = true;
            timer.startTimer = true;
        }     
    }

    public void CreateMelee()
    {
        meleeInstance = Instantiate(melee, transform.position, Quaternion.identity, transform);
    }

    public void DestroyMelee()
    {
        if (meleeInstance != null)
        {
            Destroy(meleeInstance.gameObject);
            hasMelee = false;
            GetGun();
        }           
    }
}