using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeDeathState : EnemyState
{
    private Enemy_Skeleton enemy;
    public SkeDeathState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Skeleton _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.animator.SetBool(enemy.lastAnimBoolName, true);
        enemy.animator.speed = 0;
        enemy.CapsuleCollider2D.enabled = false;
        stateTimer = 0.1f;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if(stateTimer > 0)
        {
            enemy.SetVelocity(0, 10);
        }
    }
}