using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string _aniBoolName) : base(_player, _stateMachine, _aniBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (xInput != 0)
            player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y);


        if (player.IsWallDetected())
            stateMachine.ChangeState(player.WallState);

        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.IdleState);
    }
}
