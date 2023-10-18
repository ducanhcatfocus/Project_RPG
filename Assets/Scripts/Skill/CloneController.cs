using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneController : MonoBehaviour
{ 
    [SerializeField] private float colorLoosingSpeed;
    [SerializeField] private Transform attackCheck;
    [SerializeField] private float attackCheckRadius = 0.8f;
    private float cloneTimer;
    private SpriteRenderer sr;
    private Animator animator;


    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        cloneTimer -= Time.deltaTime;
        if(cloneTimer <0)
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a - (Time.deltaTime * colorLoosingSpeed));
            if(sr.color.a < 0)
            {
                Destroy(gameObject);
            }
        }

    }
    public void SetUpClone(float cloneDuration, bool canAttack)
    {
        if (canAttack)
        {
            animator.SetBool("Attack", true);
        }
        cloneTimer = cloneDuration;
    }

    public void AnimationEndTrigger()
    {
        cloneTimer = -.1f;
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCheck.position, attackCheckRadius);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                hit.GetComponent<Enemy>().Damage();
            }
        }
    }

    private void FaceClosestTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 25);

    }
}
