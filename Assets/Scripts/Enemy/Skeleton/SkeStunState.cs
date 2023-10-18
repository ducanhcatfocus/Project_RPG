using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeStunState : EnemyState
{
    private Enemy_Skeleton enemy;
    public SkeStunState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Skeleton _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = enemy.stunDuration;
        enemy.SetVelocity(-enemy.transform.localScale.x * enemy.stunDirection.x, enemy.stunDirection.y);
        enemy.entityFX.InvokeRepeating("RedColorBlinkFx", 0, 0.1f);
    }

    public override void Exit()
    {
        base.Exit();
        enemy.entityFX.Invoke("CancelRedBlickFX", 0);

    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
            stateMachine.ChangeState(enemy.idleState);

        
     
    }
}
