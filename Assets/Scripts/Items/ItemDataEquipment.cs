
using System.Collections.Generic;
using UnityEngine;


public enum EquipmentType
{
    Weapon,
    Armor,
    Amulet,
    Flask
}
[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Equipment ")]

public class ItemDataEquipment : ItemData
{
    public EquipmentType equipmentType;

    public float itemCd;

    public ItemEffect[] itemEffects;

    public int str; // +dmg + critPow
    public int agi; //+evasion + critChange
    public int inl; //+ magicDmg + magicResis
    public int vit; // + hp

    public int damage;
    public int critChance;
    public int critPower;




    public int maxHp;
    public int armor;
    public int evasion;


    public int fireDmg;
    public int IceDmg;
    public int lightningDmg;

    public List<InventoryItem> inventoryItems;
    public void ExeItemEffect(Transform enemyPos)
    {
        foreach (var item in itemEffects) {
            item.ExecuteEffect(enemyPos);
        }
    }



    public void AddModifier()
    {
        PlayerStats playerStats = PlayerManager.Instance.player.GetComponent<PlayerStats>();
        playerStats.str.AddModifier(str);
        playerStats.agi.AddModifier(agi);
        playerStats.inl.AddModifier(inl);
        playerStats.vit.AddModifier(vit);
        playerStats.damage.AddModifier(damage);
        playerStats.critChance.AddModifier(critChance);
        playerStats.critPower.AddModifier(critPower);
        playerStats.maxHp.AddModifier(maxHp);
        playerStats.armor.AddModifier(armor);
        playerStats.evasion.AddModifier(evasion);
        playerStats.fireDmg.AddModifier(fireDmg);
        playerStats.IceDmg.AddModifier(IceDmg);
        playerStats.lightningDmg.AddModifier(lightningDmg);
    }

    public void RemoveModifier()
    {
        PlayerStats playerStats = PlayerManager.Instance.player.GetComponent<PlayerStats>();
        playerStats.str.RemoveModifier(str);
        playerStats.agi.RemoveModifier(agi);
        playerStats.inl.RemoveModifier(inl);
        playerStats.vit.RemoveModifier(vit);
        playerStats.damage.RemoveModifier(damage);
        playerStats.critChance.RemoveModifier(critChance);
        playerStats.critPower.RemoveModifier(critPower);
        playerStats.maxHp.RemoveModifier(maxHp);
        playerStats.armor.RemoveModifier(armor);
        playerStats.evasion.RemoveModifier(evasion);
        playerStats.fireDmg.RemoveModifier(fireDmg);
        playerStats.IceDmg.RemoveModifier(IceDmg);
        playerStats.lightningDmg.RemoveModifier(lightningDmg);
    }
}

   
