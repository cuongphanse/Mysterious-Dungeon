using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharacter : MonoBehaviour
{
    public Rigidbody2D rb;
    public int speed = 4;
    public float leftRight;
    public float jumb = 10f;
    public bool isFacingRight = true;

    public Animator animator;
    public LayerMask groundLayer;
    public Vector3 initialScale;



    // Start is called before the first frame update
    void Start()
    {
        initialScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        //move left or right
        leftRight = Input.GetAxisRaw("Horizontal");//a=-1,0,d=1
        rb.velocity = new Vector2(speed * leftRight, rb.velocity.y);

        // Check if the character is on the ground
        bool isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, groundLayer);

        if (isActiveAndEnabled == true && leftRight == -1)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            isFacingRight = false;
        }
        if (isFacingRight == false && leftRight == 1)
        {
            transform.localScale = new Vector3(1, 1, 1);
            isFacingRight = true;
        }

        //animator
        animator.SetFloat("move", Mathf.Abs(leftRight));
        if (Input.GetKey(KeyCode.J))
        {
            animator.SetTrigger("Attack");
        };

        if (Input.GetKey(KeyCode.L))
        {
            animator.SetTrigger("Roll");
        }

        if (Input.GetKeyDown(KeyCode.K) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumb);
            animator.SetTrigger("Jumb");
            /*if (isGround && !isActtack)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            }*/

            // Reset scale to initial scale when jumping
            transform.localScale = initialScale;
        }
    }

}
