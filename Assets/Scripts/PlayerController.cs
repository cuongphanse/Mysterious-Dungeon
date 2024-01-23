using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Entity
{
    private float xInput;
    [Header("Move Info")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpSpeed = 10f;
    [Header("Dash Info")]
    [SerializeField] private float dashSpeed;
    private float dashTime;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashCooldown;
    private float dashCooldownTimer;
    [Header("Attach Info")]
    private float comboTime = 1f;
    private float comboCooldownTimer;
    private bool isActtack;
    private int countAttack;
    

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        Moving();
        CheckInput();
        AnimatorController();
        PlayerFlip();
        dashTime -= Time.deltaTime;
        dashCooldownTimer -= Time.deltaTime;
        comboCooldownTimer -= Time.deltaTime;
    }

    public void AttackOver()
    {
        isActtack = false;
        countAttack++;
        if(countAttack>1){
            countAttack = 0;
        }
    }
    private void CheckInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        if(Input.GetKeyDown(KeyCode.J))
        {
            AttackEvent();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Dash();
        }
    }

    private void AttackEvent()
    {
        if(!isGround)
        {
            return;    
        }
        if (comboCooldownTimer < 0)
        {
            countAttack = 0;
        }
        isActtack = true;
        comboCooldownTimer = comboTime;
    }

    private void Dash()
    {
        if (dashCooldownTimer < 0 && !isActtack)
        {
            dashCooldownTimer = dashCooldown;
            dashTime = dashDuration;
        }
    }

    private void Moving()
    {
        if(isActtack)
        {
            rb.velocity = new Vector2(0,0);
        }else if(dashTime > 0)
        {
            rb.velocity = new Vector2(facingDir * dashSpeed,0);
        } else
        {
            rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
        }
    }

    private void Jump()
    {
        if(isGround && !isActtack)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }
    }

    private void AnimatorController()
    {
        bool isMoving = rb.velocity.x != 0;
        anima.SetFloat("yVelocity", rb.velocity.y);
        anima.SetBool("isMoving", isMoving);
        anima.SetBool("isGrounded", isGround);
        anima.SetBool("isDashing", dashTime >0);
        anima.SetBool("isAttack", isActtack);
        anima.SetInteger("countCombo", countAttack);
    }

    private void PlayerFlip()
    {
        if(rb.velocity.x > 0 && !facingRight)
        {
            Flip();
        } else if (rb.velocity.x <0 && facingRight)
        {
            Flip();
        }
    }

}
