using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    int comboCounter;
    float lastTimeAttacked;
    [SerializeField] float comboWindow = 1.5f;
    public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _stateMachine, string _aniBoolName) : base(_player, _stateMachine, _aniBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if (comboCounter >2  || Time.time >= lastTimeAttacked + comboWindow) comboCounter = 0;

        player.animator.SetInteger("ComboCounter", comboCounter);
    }

    public override void Exit()
    {
        base.Exit();
        comboCounter++;
        lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();
        player.SetVelocity(0, 0);

        if (triggerCalled)
            stateMachine.ChangeState(player.IdleState);
    }
}
