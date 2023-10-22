using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{

    [SerializeField] protected LayerMask isPlayer;

    public float moveSpeed;
    private float defaultSpeed;
    public float idleTime;
    public float battleTime;
    public float attackDistance;
    public float attackCooldown = 0.5f;
    public float lastTimeAttacked;
    public float stunDuration;
    public Vector2 stunDirection;
    protected bool canBeStuned;
    [SerializeField] protected GameObject counterImage;
    public EnemyStateMachine stateMachine { get; private set; }
    public string lastAnimBoolName {  get; private set; }

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
    }

    protected override void Start()
    {
        base.Start();
        defaultSpeed = moveSpeed;

    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }
    public virtual void AssignLastAnimBoolName(string animBoolName)
    {
        lastAnimBoolName = animBoolName;
    }

    public virtual void OpenCounterAttackWindow()
    {
        canBeStuned = true;
        counterImage.SetActive(true);
    }

    public virtual void CloseCounterAttackWindow()
    {
        canBeStuned = false;
        counterImage.SetActive(false);
    }

    public virtual bool CanBeStuned()
    {
        if (canBeStuned) {
            CloseCounterAttackWindow();
            return true;
        } 
        return false;
    }


    public override void SlowEntity(float slowPercentage, float slowDuration)
    {
        moveSpeed = moveSpeed * (1 - slowPercentage);
        animator.speed = animator.speed * (1 - slowPercentage);

        Invoke("ReturnDefautSpeed", slowDuration);
    }

    protected override void ReturnDefautSpeed()
    {
        base.ReturnDefautSpeed();
        moveSpeed = defaultSpeed;
    }
    public virtual void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();


    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * transform.localScale.x, 30, isPlayer);

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector3((transform.position.x + attackDistance)*transform.localScale.x, transform.position.y));
    }

    public override void Die()
    {
        base.Die();

        GameManager.Instance.IncreateKill();
    }

    public override void DestroyEntity(GameObject gameObject)
    {
        base.DestroyEntity(gameObject);
    }
}
