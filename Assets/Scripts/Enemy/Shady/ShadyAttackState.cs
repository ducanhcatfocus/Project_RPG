using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadyAttackState : EnemyState
{
    private Enemy_Shady enemy;
    public ShadyAttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Shady _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
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
        {
            enemy.DestroyEntity(enemy.gameObject);
 
        }
           // stateMachine.ChangeState(enemy.battleState);
           
    }

    public override void Exit()
    {
        base.Exit();
        enemy.lastTimeAttacked = Time.time;
    }

}
