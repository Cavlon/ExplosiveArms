using Cinemachine;
using UnityEngine;

public abstract class weaponScript : MonoBehaviour
{
    
    public Transform attackPoint;
    public SpriteRenderer weaponSprite;

    [HideInInspector] public bool isAttacking;   
    [HideInInspector] public bool canAttack;
    [HideInInspector] public bool throwWeapon;
    [HideInInspector] public bool useController;
    [HideInInspector] public CinemachineVirtualCamera slideCam;  

    [SerializeField] protected WeaponThrown weaponThrown;    
    [SerializeField] protected float attackDelay;
    [SerializeField] protected float camShakeIntensity;
    [SerializeField] protected float camShakeTime;
    [SerializeField] protected float attackSpeed;
    [SerializeField] protected int attackDamage;
    [SerializeField] protected float attackThrust;

    protected CameraShake playerCamShake;
    protected CameraShake slideCamShake;
    private CinemachineVirtualCamera[] vCams = new CinemachineVirtualCamera[2]; 
    private Camera cam;
    private Animator anim;
    private int moveDir;   
    private Vector2 mousePos;
    private Transform trans;
    private float angle; 
    private float attackCounter;
    private Vector2 lookDir;

    public virtual void Awake()
    {
        trans = GetComponent<Transform>();
        anim = trans.parent.GetComponentInChildren<Animator>();
        cam = Camera.main;
        vCams = FindObjectsOfType<CinemachineVirtualCamera>();
        slideCam = vCams[0];
        slideCamShake = vCams[0].GetComponent<CameraShake>();
        playerCamShake = vCams[1].GetComponent<CameraShake>();
        canAttack = true;
    }
    public void Update()
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
        if (isAttacking)
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
            if (attackCounter <= 0 & canAttack)
            {
                attackCounter = attackDelay;
                Attack();
            }
        } else
        {          
            slideCam.Priority = 0;
        }
        if (attackCounter > 0)
        {
            attackCounter -= Time.deltaTime;
        }

        if (throwWeapon && weaponThrown != null)
        {
            Throw();
        }
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

    public abstract void Attack();

    void Throw()
    {
        Vector2 dir = (attackPoint.position - trans.position) * 2;
        WeaponThrown thrown = Instantiate(weaponThrown, (Vector2)trans.position + dir, attackPoint.rotation);
        thrown.speed = 1500;
        thrown.sprite.GetComponent<SpriteRenderer>().sprite = weaponSprite.sprite;
        throwWeapon = false;
        Destroy(gameObject);
    }
}
