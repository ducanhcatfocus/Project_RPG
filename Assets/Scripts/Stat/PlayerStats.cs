using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    // Start is called before the first frame update

    private Player player;
    protected override void Start()
    {
        base.Start();
        player = GetComponent<Player>();
    }

    public override void TakeDamage(int dmg)
    {
        base.TakeDamage(dmg);
        player.Damage();
    }

    protected override void Die()
    {
        base.Die();
        player.Die();
    }
}
