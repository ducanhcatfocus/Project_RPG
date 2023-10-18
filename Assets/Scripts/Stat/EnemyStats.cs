using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats    
{
    private Enemy enemy;
    private ItemDrop itemDrop;

    [SerializeField] private int level = 1;

    [Range(0f, 1f)]
    [SerializeField]
    private float percentageModifier = 0.4f;
    protected override void Start()
    {
        ApplyLevelModifier();
        base.Start();
        enemy = GetComponent<Enemy>();
        itemDrop = GetComponent<ItemDrop>();

    }

    private void ApplyLevelModifier()
    {
        Modify(damage);
        Modify(maxHp);
        Modify(armor);
        Modify(evasion);
        Modify(fireDmg);
        Modify(IceDmg);
        Modify(lightningDmg);
    }

    private void Modify(Stats stats)
    {
        for (int i = 1; i < level; i++)
        {
            float modifier = stats.GetValue() * percentageModifier;
            stats.AddModifier(Mathf.RoundToInt(modifier));
        }
    }

    public override void TakeDamage(int dmg)
    {
        base.TakeDamage(dmg);
        enemy.Damage();
    }

    protected override void Die()
    {
        base.Die();
        enemy.Die();

        itemDrop.GenerateDrop();
    }
}
