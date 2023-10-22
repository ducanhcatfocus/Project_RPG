using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Heal Effect", menuName = "Data/Item Effect/Heal Effect")]

public class HealEffect : ItemEffect
{
    [Range(0f, 1f)]
    [SerializeField] private float healPercentage;
    public override void ExecuteEffect(Transform enemyPos)
    {

        //Debug.Log(healPercentage);
        PlayerStats playerStats = PlayerManager.Instance.player.GetComponent<PlayerStats>();
        int healAMount = Mathf.RoundToInt( playerStats.GetMaxHealth() * healPercentage);
        //Debug.Log(healAMount);

        playerStats.IncreaseHP(healAMount);
    }
}
