using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();
    
    public void AnimationEndTrigger()
    {
        player.AnimationTrigger();
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);
        foreach (var hit in colliders)
        {

            if(hit.GetComponent<Enemy>() != null)
            {
                EnemyStats targetStat = hit.GetComponent<EnemyStats>();
                player.characterStats.DoDamage(targetStat);
                //hit.GetComponent<Enemy>().Damage();
                //hit.GetComponent<CharacterStats>().TakeDamage(player.characterStats.damage.GetValue());

         

                ItemDataEquipment weaponData = Inventory.Instance.GetEquipment(EquipmentType.Weapon);
                if (weaponData != null)
                    weaponData.ExeItemEffect(targetStat.transform);
            }
        }
    }


}
