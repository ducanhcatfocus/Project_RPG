using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : PlayerState
{
    public PlayerGroundState(Player _player, PlayerStateMachine _stateMachine, string _aniBoolName) : base(_player, _stateMachine, _aniBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Z))
            stateMachine.ChangeState(player.PrimaryAttackState);
        if (!player.IsGroundDetected())
            stateMachine.ChangeState(player.AirState);
        if (Input.GetKeyDown(KeyCode.Space) && player.IsGroundDetected())
            stateMachine.ChangeState(player.JumpState);

        if (Input.GetKeyDown(KeyCode.Q))
            stateMachine.ChangeState(player.CounterAttackState);
    }
}
