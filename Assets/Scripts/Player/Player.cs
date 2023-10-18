using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public Vector2[] attackMovement;


    public float counterAttackDuration = 0.2f;


    public float moveSpeed = 8f;
    private float defaultMoveSpeed;
    public float jumpForce = 12f;
    private float defaultJumpForce;

    public float dashSpeed = 25f;
    private float defaultDashSpeed;
    public float dashDuration = 0.2f;
    public float dashTimer;
    public float dashCD = 1.5f;





    //[SerializeField] private List<PlayerState> allStates;
    // public Dictionary<Type, PlayerState> _lookupState;

    public SkillManager skill;

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

    public PlayerCounterAttackState CounterAttackState { get; private set; }

    public PlayerDeathState PlayerDeathState { get; private set; }




    #endregion


    protected override void Awake()
    {
        base.Awake();
        //_lookupState = new Dictionary<Type, PlayerState>();
        //foreach (var states in allStates)
        //{
        //    _lookupState.Add(states.GetType(), states);
        //}

        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, "Idle");
        MoveState = new PlayerMoveState(this, StateMachine, "Move");
        JumpState = new PlayerJumpState(this, StateMachine, "Jump");
        AirState = new PlayerAirState(this, StateMachine, "Jump");
        DashState = new PlayerDashState(this, StateMachine, "Dash");
        WallState = new PlayerWallState(this, StateMachine, "Wall");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, "Jump");
        PrimaryAttackState = new PlayerPrimaryAttackState(this, StateMachine, "Attack");
        CounterAttackState = new PlayerCounterAttackState(this, StateMachine, "CounterAttack");
        PlayerDeathState = new PlayerDeathState(this, StateMachine, "Die");

    }

    //public PlayerState GetState(Type type)
    //{
    //    return _lookupState[type];
    //}

    protected override void Start()
    {
      
        base.Start();
        skill = SkillManager.Instance;
        StateMachine.Initialize(IdleState);

        defaultMoveSpeed = moveSpeed;
        defaultJumpForce = jumpForce;
        defaultDashSpeed = dashSpeed;
    }

    protected override void Update()
    {
        base.Update();
        StateMachine.currentState.Update();
        CheckForDashInput();


        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Inventory.Instance.UseFlask();
        }
    }


    private void CheckForDashInput()
    {
        if (IsWallDetected()) return;
   
        if (Input.GetKeyDown(KeyCode.LeftShift) && SkillManager.Instance.dash.CanUseSkill() )
        {
        
            StateMachine.ChangeState(DashState);
        }
           
    }

    public void AnimationTrigger() => StateMachine.currentState.AnimationFinishTrigger();

    public override void Die()
    {
        base.Die();

        StateMachine.ChangeState(PlayerDeathState);
    }


    public override void SlowEntity(float slowPercentage, float slowDuration)
    {
        moveSpeed = moveSpeed *(1- slowPercentage);
        jumpForce = jumpForce * (1- slowPercentage);
        dashSpeed = dashSpeed * (1- slowPercentage);
        animator.speed = animator.speed *(1- slowPercentage);

        Invoke("ReturnDefautSpeed", slowDuration);
    }

    protected override void ReturnDefautSpeed()
    {
        base.ReturnDefautSpeed();
        moveSpeed = defaultMoveSpeed;
        jumpForce = defaultJumpForce;
        dashSpeed = defaultMoveSpeed;
    }

}
