using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeEnemyAttackTrigger : MonoBehaviour
{
    // Start is called before the first frame update

    private Enemy_Skeleton enemy => GetComponentInParent<Enemy_Skeleton>();
    private void AnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius);

        foreach (var hit in colliders)
        {
            if(hit.GetComponent<Player>() != null)
            {
                PlayerStats player = hit.GetComponent<PlayerStats>();
                enemy.characterStats.DoDamage(player);
               // hit.GetComponent<Player>().Damage();
            }
        }
    }

    private void OpenCounterWindow() => enemy.OpenCounterAttackWindow();
    private void CloseCounterWindow() => enemy.CloseCounterAttackWindow();
}
