using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector] public bool gameOver;

    [SerializeField] protected float movementBaseSpeed;
    [SerializeField] protected float destroyTime;
    [SerializeField] protected float dashSpeed;
    [SerializeField] protected float startDashTime;
    [SerializeField] protected float startDashCooldown;
    [SerializeField] protected EffectDestroy dashParticles;
    [SerializeField] protected float startKnockTime;

    private Animator animator;
    private Vector2 movementDir;
    private Vector2 movementDirRaw;
    private float movementSpeed;
    private Rigidbody2D rb;
    private float dashTime;
    private float knockTime;
    private float dashCooldown;
    private bool canDash = true;
    private bool dodge;
    private Vector2 initialVelocity;
    private bool canKnock = true;

    [HideInInspector] public bool knockback;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (canDash & movementDirRaw.sqrMagnitude > .02f)
        {
            if (Input.GetButtonDown("Dash") || Input.GetAxisRaw("Dash") > 0f)
            {
                dodge = true;
            }
        }
        if (canKnock & knockback)
        {
            knockTime = startKnockTime;
            canKnock = false;
        }
        if (!knockback)
        {
            DashTime();
        }
        DashCooldown();
        KnockedBack();
    }

    void FixedUpdate()
    {
        ProcessInputs();
        Move();
        Animate();
        Dash();
    }

    void ProcessInputs()
    {
        if (!gameOver)
        {
            movementDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            movementDirRaw = movementDir;
            movementSpeed = Mathf.Clamp(movementDir.magnitude, 0f, 1f);
            movementDir.Normalize();
        }      
    }

    void Move()
    {
        if (!knockback & dashTime <= 0)
        {
            rb.velocity = movementDir * movementSpeed * movementBaseSpeed * 100 * Time.deltaTime;           
        }
    }
       

    void Animate()
    {
        if (movementDir != Vector2.zero)
        {
            animator.SetFloat("Vertical", movementDir.y);
            if (movementDir.y > 0)
            {
                animator.SetInteger("Dir", 1);
            }
            else if (movementDir.y < 0)
            {
                animator.SetInteger("Dir", 0);
            }
        }
        animator.SetFloat("Speed", movementSpeed);
    }

    void Dash()
    {
        if (dodge & !knockback)
        {
            float angle = Mathf.Atan2(-movementDir.y, -movementDir.x) * Mathf.Rad2Deg;
            Quaternion instanceRotation = Quaternion.Euler(0, 0, angle);
            EffectDestroy instance_ = Instantiate(dashParticles, transform.position, instanceRotation);
            instance_.destroyTime = destroyTime;
            initialVelocity = rb.velocity;
            rb.velocity *= dashSpeed;
            dashTime = startDashTime;
            dashCooldown = dashTime + startDashCooldown;
            dodge = false;
            canDash = false;
        }
    }

    void DashTime()
    {
        if (dashTime > 0)
        {
            dashTime -= Time.deltaTime;
        }
        else
        {
            rb.velocity = initialVelocity;
        }
    }

    void DashCooldown()
    {
        if (dashCooldown > 0)
        {
            dashCooldown -= Time.deltaTime;

        } else
        {
            canDash = true;
        }
    }

    void KnockedBack()
    {
        if (knockTime > 0)
        {
            knockTime -= Time.deltaTime;
        }
        else
        {
            canKnock = true;
            knockback = false;
        }
    }

    public void Stop()
    {
        rb.velocity = new Vector2(0, 0);
    }
}
