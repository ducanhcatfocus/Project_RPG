using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    public PlayerWallJumpState(Player _player, PlayerStateMachine _stateMachine, string _aniBoolName) : base(_player, _stateMachine, _aniBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = 0.2f;
        player.SetVelocity(5 * -player.transform.localScale.x, player.jumpForce);
        player.Flip(-player.transform.localScale.x);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(xInput != 0 && stateTimer < 0)
            player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y);

        if (rb.velocity.y < 0)
            stateMachine.ChangeState(player.AirState);

        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.IdleState);
    }
}
