using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundState
{
    public PlayerIdleState(Player _player, PlayerStateMachine _stateMachine, string _aniBoolName) : base(_player, _stateMachine, _aniBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        rb.velocity = Vector2.zero;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();


        if (xInput == player.transform.localScale.x && player.IsWallDetected())
            return;

        if (xInput != 0)
        {
            stateMachine.ChangeState(player.MoveState);
            //stateMachine.ChangeState(player.GetState(typeof(PlayerMoveState)));
        }
    }
}
