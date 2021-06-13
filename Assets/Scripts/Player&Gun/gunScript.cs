using Cinemachine;
using UnityEngine;

public class gunScript : MonoBehaviour
{
    [SerializeField] protected BulletController bullet;
    public GunThrown gunThrown;
    public Transform firePoint;
    public bool isFiring;
    public bool isAuto;
    public bool canFire;
    public bool throwGun;
    public bool useController;

    [SerializeField] protected float bulletSpeed;
    [SerializeField] protected int bulletDamage;
    [SerializeField] protected float bulletThrust;
    [SerializeField] protected float shotDelay;
    [SerializeField] protected float throwDistance;
    [SerializeField] protected float camShakeIntensity;
    [SerializeField] protected float camShakeTime;

    [HideInInspector] public CinemachineVirtualCamera slideCam;

    private CameraShake playerCamShake; 
    private CinemachineVirtualCamera[] vCams;
    private CameraShake slideCamShake;
    private Camera cam;
    private Animator anim;
    private int moveDir;   
    private Vector2 mousePos;
    private Transform trans;
    private float angle;
    private SpriteRenderer gunSprite;
    private SpriteRenderer playerSprite;
    private float shotCounter;
    private Vector2 lookDir;

    void Awake()
    {
        gunSprite = GetComponentInChildren<SpriteRenderer>();
        trans = GetComponent<Transform>();
        playerSprite = trans.parent.GetComponentInChildren<SpriteRenderer>();
        anim = trans.parent.GetComponentInChildren<Animator>();
        cam = Camera.main;
        vCams = FindObjectsOfType<CinemachineVirtualCamera>();
        slideCam = vCams[0];
        slideCamShake = vCams[0].GetComponent<CameraShake>();
        playerCamShake = vCams[1].GetComponent<CameraShake>();
        canFire = true;
    }
    void Update()
    {
        //Use Mouse
        if (!useController)
        {
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            lookDir = mousePos - (Vector2)trans.position;
            Rotate();
        } else
        {
            lookDir = Vector2.right * Input.GetAxisRaw("R Horizontal") + Vector2.up * Input.GetAxisRaw("R Vertical");
            if (lookDir.sqrMagnitude > 0.0f)
            {
                Rotate();
            }
        }

        moveDir = anim.GetInteger("Dir");
        if (isFiring)
        {
            slideCam.Priority = 2;
            if (angle > 0)
            {
                anim.SetInteger("Dir", 1);
                moveDir = 1;
            } else
            {
                anim.SetInteger("Dir", 0);
                moveDir = 0;
            }
            if (shotCounter <= 0 & canFire)
            {
                shotCounter = shotDelay;
                BulletController newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
                newBullet.tag = "Bullet";
                newBullet.speed = bulletSpeed;
                newBullet.damage = bulletDamage;
                newBullet.thrust = bulletThrust;
                slideCamShake.ShakeCamera(camShakeIntensity, camShakeTime);
                playerCamShake.ShakeCamera(camShakeIntensity, camShakeTime);
                if (!isAuto)
                {
                    canFire = false;
                }
            }
        } else
        {          
            slideCam.Priority = 0;
        }
        if (shotCounter > 0)
        {
            shotCounter -= Time.deltaTime;
        }

        if (throwGun)
        {          
            Vector2 dir = (firePoint.position - trans.position) * throwDistance;
            GunThrown thrown = Instantiate(gunThrown, (Vector2)trans.position + dir, firePoint.rotation);
            thrown.speed = bulletSpeed;
            thrown.sprite.GetComponent<SpriteRenderer>().sprite = gunSprite.sprite;
            throwGun = false;
            Destroy(gameObject);
        }
        //OffsetCalculation();
    }

    void Rotate()
    {
        angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        if (angle > 90 || angle < -90)
        {
            trans.localScale = new Vector3(1, -1, 1);
        } else
        {
            trans.localScale = new Vector3(1, 1, 1);
        }
        Quaternion rotate = Quaternion.Euler(0, 0, angle);
        trans.rotation = rotate;
    }

    void OffsetCalculation()
    {
        switch (moveDir) 
        {
            case 0://Down
                gunSprite.sortingOrder = playerSprite.sortingOrder + 1;
                break;
            case 1://Up
                gunSprite.sortingOrder = playerSprite.sortingOrder - 1;
                break;
        }
    }
}
