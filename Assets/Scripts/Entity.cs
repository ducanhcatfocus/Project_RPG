using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{

    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask groundLayer;

    public Transform attackCheck;
    public float attackCheckRadius;


    [SerializeField] protected Vector2 knockBackDirection;
    [SerializeField] protected float knockBackDuration;
    protected bool isKnocked;

    #region Component
    public Animator animator { get; private set; }
    public Rigidbody2D rb { get; private set; }

    public EntityFX entityFX;
    public CharacterStats characterStats { get; private set; } 

    public CapsuleCollider2D CapsuleCollider2D { get; private set; }
    #endregion

    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        entityFX = GetComponent<EntityFX>();
        characterStats = GetComponent<CharacterStats>();
        CapsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }
    protected virtual void Update()
    {

    }

    public virtual void SlowEntity(float slowPercentage, float slowDuration)
    {

    }

    protected virtual void ReturnDefautSpeed()
    {
        animator.speed = 1;
    }

    public virtual void Damage()
    {
        entityFX.StartCoroutine("FlashFX");
        StartCoroutine(HitKnockBack());
    }

    protected virtual IEnumerator HitKnockBack()
    {
        isKnocked = true;
        rb.velocity = new Vector2(knockBackDirection.x * -transform.localScale.x, knockBackDirection.y);
        yield return new WaitForSeconds(knockBackDuration);
        isKnocked=false;

    }

    public virtual void SetVelocity(float _xVelocity, float _yVelocity)
    {
        if (isKnocked) return;
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
    }


    public virtual void Flip(float xInput)
    {
        transform.localScale = new Vector3(xInput, 1, 1);
    }

    public virtual void Die()
    {

    }

    public virtual bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);

    public virtual bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * transform.localScale.x, wallCheckDistance, groundLayer);






    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }


}
