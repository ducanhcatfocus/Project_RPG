using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float moveSpeed = 8f;
    public float jumpForce = 12f;
    public float dashSpeed = 25f;
    public float dashDuration = 0.2f;
    public float dashTimer;
    public float dashCD = 1.5f;



    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask groundLayer;


    #region Component
    public Animator animator { get; private set; }
    public Rigidbody2D rb { get; private set; }
    #endregion
    #region State
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerAirState AirState { get; private set; }
    public PlayerDashState DashState {  get; private set; }
    public PlayerWallState WallState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerPrimaryAttackState PrimaryAttackState { get; private set; }




    #endregion


    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, "Idle");
        MoveState = new PlayerMoveState(this, StateMachine, "Move");
        JumpState = new PlayerJumpState(this, StateMachine, "Jump");
        AirState = new PlayerAirState(this, StateMachine, "Jump");
        DashState = new PlayerDashState(this, StateMachine, "Dash");
        WallState = new PlayerWallState(this, StateMachine, "Wall");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, "Jump");
        PrimaryAttackState = new PlayerPrimaryAttackState(this, StateMachine, "Attack");
    }


    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        StateMachine.currentState.Update();
        CheckForDashInput();
    }

    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
    }

    
    public void Flip(float xInput)
    {
        transform.localScale = new Vector3(xInput,1,1);
    }


    private void CheckForDashInput()
    {
        dashTimer -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashTimer < 0)
        {
            dashTimer = dashCD;
            StateMachine.ChangeState(DashState);
        }
           
    }

    public void AnimationTrigger() => StateMachine.currentState.AnimationFinishTrigger();
    
    public bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);

    public bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * transform.localScale.x, wallCheckDistance, groundLayer);






    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y ));

    }
}
