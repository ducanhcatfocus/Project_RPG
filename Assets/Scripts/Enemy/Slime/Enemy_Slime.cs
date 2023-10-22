using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SlimeType
{
    big, medium, small
}
public class Enemy_Slime : Enemy
{

    public SlimeType slimeType;
    [SerializeField] private int minNumberOfSlimeCreateWhenDeath = 2;
    [SerializeField] private int maxNumberOfSlimeCreateWhenDeath = 4;

    [SerializeField] private GameObject slimePrefab;
    [SerializeField] private Vector2 minCreateVec;
    [SerializeField] private Vector2 maxCreateVec;
    public SlimeIdleState idleState { get; private set; }
    public SlimeMoveState moveState { get; private set; }

    public SlimeBattleState battleState { get; private set; }

    public SlimeAttackState attackState { get; private set; }

    public SlimeStunState stunState { get; private set; }

    public SlimeDeathState deathState { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        idleState = new SlimeIdleState(this, stateMachine, "Idle", this);
        moveState = new SlimeMoveState(this, stateMachine, "Move", this);
        battleState = new SlimeBattleState(this, stateMachine, "Move", this);
        attackState = new SlimeAttackState(this, stateMachine, "Attack", this);
        stunState = new SlimeStunState(this, stateMachine, "Stun", this);
        deathState = new SlimeDeathState(this, stateMachine, "Idle", this);
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

        if (slimeType != SlimeType.small)
        {
            int numberOfSlime = Random.Range(minNumberOfSlimeCreateWhenDeath, maxNumberOfSlimeCreateWhenDeath);
            CreateSlime(numberOfSlime, slimePrefab);
        }
     
        stateMachine.ChangeState(deathState);

    }

    private void CreateSlime(int _numberOfSlimeCreateWhenDeath, GameObject _slimeProfab)
    {
        for (int i = 0; i < _numberOfSlimeCreateWhenDeath; i++)
        {
            GameObject slime = Instantiate(_slimeProfab, transform.position, Quaternion.identity);
            slime.GetComponent<Enemy_Slime>().SetUpSlime();
        }
    }

    public void SetUpSlime()
    {
        float xVelocity = Random.Range(minCreateVec.x, maxCreateVec.x);
        float yVelocity = Random.Range(minCreateVec.y, maxCreateVec.y);

        isKnocked = true;
        GetComponent<Rigidbody2D>().velocity = new Vector2(xVelocity, yVelocity);
    }
}
