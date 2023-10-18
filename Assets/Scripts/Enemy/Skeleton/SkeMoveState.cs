using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeMoveState : SkeGroundState 
{
    public SkeMoveState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Skeleton _enemy) : base(_enemyBase, _stateMachine, _animBoolName, _enemy)
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

        enemy.SetVelocity(enemy.moveSpeed * enemy.transform.localScale.x, enemy.rb.velocity.y);

        if(enemy.IsWallDetected() || !enemy.IsGroundDetected() ) 
        {
            enemy.SetVelocity(0, enemy.rb.velocity.y);

            enemy.Flip(enemy.transform.localScale.x * -1);
            stateMachine.ChangeState(enemy.idleState);
        }
    }
}

