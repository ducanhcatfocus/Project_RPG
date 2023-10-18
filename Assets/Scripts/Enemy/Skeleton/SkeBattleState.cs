using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeBattleState : EnemyState
{
    private Enemy_Skeleton enemy;
    private Transform player;
    private int moveDirection;
    public SkeBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Skeleton _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        player = PlayerManager.Instance.player.transform;

    }



    public override void Update()
    {
        base.Update();

        if (enemy.IsPlayerDetected())
        {
            stateTimer = enemy.battleTime;
            if(enemy.IsPlayerDetected().distance < enemy.attackDistance )
            {
                if(EnemyCanAttack())
                    stateMachine.ChangeState(enemy.attackState);
            }
        }
        else
        {
            if(stateTimer < 0 || Vector2.Distance(player.transform.position, enemy.transform.position) > 10)
            {
                stateMachine.ChangeState(enemy.idleState);

            }
        }
        if(player.position.x > enemy.transform.position.x)
        {
            moveDirection = 1;
        }else if (player.position.x < enemy.transform.position.x) { moveDirection = -1; }

        enemy.Flip(moveDirection);
        enemy.SetVelocity(enemy.moveSpeed * moveDirection, enemy.rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
    }

    private bool EnemyCanAttack()
    {
        if(Time.time >= enemy.lastTimeAttacked + enemy.attackCooldown)
        {
            enemy.lastTimeAttacked = Time.time;
            return true;
        }
        return false;
    }
}
