using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _aniBoolName) : base(_player, _stateMachine, _aniBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.skill.clone.CreateClone(player.transform);
        stateTimer = player.dashDuration;
    }

    public override void Exit()
    {
        base.Exit();
        player.SetVelocity(0, rb.velocity.y);

    }

    public override void Update()
    {
        base.Update();

        if(!player.IsGroundDetected() && player.IsWallDetected())
            stateMachine.ChangeState(player.WallState);

        player.SetVelocity(player.dashSpeed * player.transform.localScale.x, 0);
        if (stateTimer < 0) stateMachine.ChangeState(player.IdleState);
    }
}
