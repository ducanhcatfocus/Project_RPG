using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shady : Enemy
{
    public ShadyIdleState idleState { get; private set; }
    public ShadyMoveState moveState { get; private set; }

    public ShadyBattleState battleState { get; private set; }

    public ShadyAttackState attackState { get; private set; }

    public ShadyStunState stunState { get; private set; }

    public ShadyDeathState deathState { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        idleState = new ShadyIdleState(this, stateMachine, "Idle", this);
        moveState = new ShadyMoveState(this, stateMachine, "Move", this);
        battleState = new ShadyBattleState(this, stateMachine, "Move", this);
        attackState = new ShadyAttackState(this, stateMachine, "Attack", this);
        stunState = new ShadyStunState(this, stateMachine, "Stun", this);
        deathState = new ShadyDeathState(this, stateMachine, "Idle", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
    }

    public override bool CanBeStuned()
    {
        if (base.CanBeStuned())
        {
            stateMachine.ChangeState(stunState); return true;
        }
        return false;
    }


    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deathState);
    }
}
