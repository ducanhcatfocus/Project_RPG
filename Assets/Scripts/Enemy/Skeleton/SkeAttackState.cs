using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeAttackState : EnemyState
{
    private Enemy_Skeleton enemy;
    public SkeAttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Skeleton _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        enemy.SetVelocity(0, 0);
        if (triggerCalled)
            stateMachine.ChangeState(enemy.battleState);
    }

    public override void Exit()
    {
        base.Exit();
        enemy.lastTimeAttacked = Time.time;
    }

    
}
