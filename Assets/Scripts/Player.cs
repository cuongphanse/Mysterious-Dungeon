using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Move Info")]
    public float moveSpeed = 12f;
    public float jumpForce = 20f;
    [Header("Dash Info")]
    [SerializeField] private float dashCooldown;
    private float dashTimer;
    public float dashSpeed = 30f;
    public float dashDuration = 0.5f;
    public float dashDir {  get; private set; }
    [Header("Check Info")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallDistance;
    [SerializeField] private LayerMask whatIsGround;
    public int facingDir { get; private set; } = 1;
    public bool facingRight = true;
    #region Components
    public Animator anim {  get; private set; }
    public Rigidbody2D rb { get; private set; }
#endregion
    #region State
    public PlayerStateMachine stateMachine {  get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    #endregion
    public void Awake()
    {
        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        stateMachine.currentState.Update();
        CheckForDashInput();
    }
    public void CheckForDashInput()
    {
        dashTimer -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.L) && dashTimer < 0)
        {
            dashDir = Input.GetAxisRaw("Horizontal");
            dashTimer = dashCooldown;
            if(dashDir == 0)
            {
                dashDir = facingDir;
            }
            stateMachine.ChangeState(dashState);
        }
    }
    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }
    public bool IsGroundedDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundDistance, whatIsGround);
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallDistance, wallCheck.position.y));
    }
    public void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0,180,0);
    }
    public void FlipController(float _x)
    {
        if(_x > 0 && !facingRight)
        {
            Flip();
        } else if(_x < 0 && facingRight) {
            Flip();
        }
    }
}
