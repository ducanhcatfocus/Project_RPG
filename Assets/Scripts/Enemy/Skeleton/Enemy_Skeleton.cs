using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Skeleton : Enemy
{

    public SkeIdleState idleState { get; private set; }
    public SkeMoveState moveState { get; private set; }

    public SkeBattleState battleState { get; private set; }

    public SkeAttackState attackState { get; private set; }

    public SkeStunState stunState { get; private set; }

    public SkeDeathState deathState { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        idleState = new SkeIdleState(this, stateMachine,"Idle", this);
        moveState = new SkeMoveState(this, stateMachine, "Move", this);
        battleState = new SkeBattleState(this, stateMachine, "Move", this);
        attackState = new SkeAttackState(this, stateMachine, "Attack", this);
        stunState = new SkeStunState(this, stateMachine, "Stun", this);
        deathState = new SkeDeathState(this, stateMachine, "Idle", this);
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
