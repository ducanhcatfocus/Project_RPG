using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallState : PlayerState
{
    public PlayerWallState(Player _player, PlayerStateMachine _stateMachine, string _aniBoolName) : base(_player, _stateMachine, _aniBoolName)
    {
    }
    float faceDirection;

    public override void Enter()
    {
        base.Enter();
        faceDirection = xInput;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.WallJumpState);
            return;
        }

        if (yInput >= 0)
            rb.velocity = new Vector2(0, rb.velocity.y * 0.7f);

        if(xInput != 0 && faceDirection != xInput)
            stateMachine.ChangeState(player.IdleState);

        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.IdleState);



    }
}
