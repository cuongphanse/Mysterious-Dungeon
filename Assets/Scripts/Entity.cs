using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Collision Info")]
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected LayerMask whatIsGround;
    protected bool isGround;
    protected bool isWallDetected;
    protected int facingDir = 1;
    protected bool facingRight = true;
    protected Rigidbody2D rb;
    protected Animator anima;
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anima = GetComponentInChildren<Animator>();

        if(wallCheck == null){
            wallCheck = transform;
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        CheckIsGround();
    }
    
    protected virtual void CheckIsGround()
    {
        isGround = Physics2D.Raycast(groundCheck.position,Vector2.down,groundCheckDistance,whatIsGround);
        isWallDetected = Physics2D.Raycast(wallCheck.position,Vector2.right,wallCheckDistance * facingDir,whatIsGround);
    }

    protected virtual void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0,180,0);
    }
    protected virtual void OnDrawGizmos() {
        Gizmos.DrawLine(groundCheck.position,new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position,new Vector3(wallCheck.position.x + wallCheckDistance * facingDir, wallCheck.position.y));

    }
}
