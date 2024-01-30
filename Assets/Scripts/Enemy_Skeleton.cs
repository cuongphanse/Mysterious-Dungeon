using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Skeleton : Entity
{
    bool isActtacking;
    [Header("Move Info")]
    [SerializeField] private float moveSpeed;
    
    [Header("Player detection")]
    [SerializeField] private float playerCheckDistance;
    [SerializeField] private LayerMask whatIsPlayer;
    private RaycastHit2D isPlayerDetected;
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (isPlayerDetected)
        {
            if (isPlayerDetected.distance > 1)
            {
                rb.velocity = new Vector2(moveSpeed * facingDir *2f, rb.velocity.y);
                Debug.Log("m xong roi");
                isActtacking = false;
            }
            else
            {
                isActtacking = true;
                rb.velocity = new Vector2(0f, 0f);
                Debug.Log("m diii");
            }
        } else {
             isActtacking= false;
            rb.velocity = new Vector2(moveSpeed * facingDir, rb.velocity.y);
             Debug.Log("out range");
        }

        if (!isGround || isWallDetected)
            Flip();
            Movememt();
    }

    private void Movememt()
    {
        if(!isActtacking)
            rb.velocity = new Vector2(facingDir * moveSpeed, rb.velocity.y);
    }

    protected override void CheckIsGround()
    {
        base.CheckIsGround();
        isPlayerDetected = Physics2D.Raycast(transform.position,Vector2.right,playerCheckDistance*facingDir,whatIsPlayer);
    }
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position,new Vector3(transform.position.x + playerCheckDistance * facingDir,transform.position.y));
    }
}
