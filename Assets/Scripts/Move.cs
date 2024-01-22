using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private Animator anima;
    private float xInput;
    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] private float jumpSpeed = 10f;
    [Header("Collision Info")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    private bool isGround;
    private Rigidbody2D rb;

    private int facingDir = 1;
    private bool facingRight = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anima = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Moving();
        CheckInput();
        PlayerMove();
        CheckIsGround();
        PlayerFlip();
    }

    public void CheckIsGround()
    {
        isGround = Physics2D.Raycast(transform.position,Vector2.down,groundCheckDistance,whatIsGround);
    }
    private void CheckInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void Moving()
    {
        rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
    }

    private void Jump()
    {
        if(isGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }
    }

    private void PlayerMove()
    {
        bool isMoving = rb.velocity.x != 0;
        anima.SetFloat("yVelocity", rb.velocity.y);
        anima.SetBool("isMoving", isMoving);
        anima.SetBool("isGrounded", isGround);
    }

    private void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0,180,0);
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

    private void OnDrawGizmos() {
        Gizmos.DrawLine(transform.position,new Vector3(transform.position.x, transform.position.y - groundCheckDistance));
    }
}
