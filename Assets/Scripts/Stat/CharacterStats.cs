using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterStats : MonoBehaviour
{

    private EntityFX fx;
    // Start is called before the first frame update
    public Stats str; // +dmg + critPow
    public Stats agi; //+evasion + critChange
    public Stats inl; //+ magicDmg + magicResis
    public Stats vit; // + hp

    public Stats damage;
    public Stats critChance;
    public Stats critPower;




    public Stats maxHp;
    public Stats armor;
    public Stats evasion;


    public Stats fireDmg;
    public Stats IceDmg;
    public Stats lightningDmg;

    public bool isIgnited; // dps
    public bool isChilled; // slow, reduce armor
    public bool isShocked; // 

    [SerializeField] float aglimentDuration = 4f;
    private float ignitedTimer;
    private float chillTimer;
    private float shockTimer;


    private float ignitedDamageCd = 0.1f;
    private float ignitedDamageTimer;
    private int igniteDamage;



    public int curentHp;

    public System.Action onHPChanged;
    protected virtual void Start()
    {
        critPower.SetDefaultValue(150);
        curentHp = GetMaxHealth();
        fx = GetComponent<EntityFX>();
    }

    protected virtual void Update()
    {
        ignitedTimer -=Time.deltaTime;
        chillTimer -=Time.deltaTime;
        shockTimer -=Time.deltaTime;
        ignitedDamageTimer -=Time.deltaTime;

        if (ignitedTimer < 0)
            isIgnited = false;
        if (chillTimer < 0)
            isChilled = false;
        if (shockTimer < 0)
            isShocked = false;

        if (ignitedDamageCd < 0 && isIgnited)
        {
            DecreaseHPbyDmg(igniteDamage);
            if(curentHp <= 0)
            {
                Die();
            }
            ignitedDamageTimer = ignitedDamageCd;
        }

    }

   


    public void DoMagicalDmg(CharacterStats target)
    {
        int _fireDmg = fireDmg.GetValue();
        int _iceDmg = IceDmg.GetValue();
        int _shockDmg = lightningDmg.GetValue();


        int totalMagicalDmg = _fireDmg + _iceDmg + _shockDmg + inl.GetValue();
        totalMagicalDmg = Mathf.Clamp(totalMagicalDmg, 0, int.MaxValue);
        target.TakeDamage(totalMagicalDmg);

        if(Mathf.Max(_fireDmg, _iceDmg, _shockDmg) <= 0)
        {
            return;
        }
        bool canApplyIgnite = _fireDmg > _iceDmg && _fireDmg > _shockDmg;
        bool canApplyChill = _iceDmg > _fireDmg && _iceDmg > _shockDmg;
        bool canApplyShock = _shockDmg > _fireDmg && _shockDmg > _iceDmg;


        if (canApplyIgnite)
            target.SetupIgniteDmg(Mathf.RoundToInt(_fireDmg * 0.2f));
        target.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);
    }

    public  void ApplyAilments(bool ignite, bool chill, bool shock)
    {
        if (isChilled || isIgnited || isShocked) return;

        if(ignite)
        {
            isIgnited = ignite;
            ignitedTimer = aglimentDuration;
            fx.InvokeIgniteColorFX(aglimentDuration);
        }
        if (chill)
        {
            isChilled = chill;
            chillTimer = aglimentDuration;
            float slowPercentage = 0.4f;
            GetComponent<Entity>().SlowEntity(slowPercentage, aglimentDuration);
            fx.InvokeChillColorFX(aglimentDuration);
        }
        if (shock)
        {
            isShocked = shock;
            shockTimer = aglimentDuration;
            fx.InvokeShockColorFX(aglimentDuration);
        }
        isIgnited = ignite;
        isChilled = chill;
        isShocked = shock;
    }

    public void SetupIgniteDmg(int dmg)
    {
        igniteDamage = dmg;
    }
    public virtual void DoDamage(CharacterStats _targetStat)
    {
        if (CalculateEvasionBeforeTakeDmg(_targetStat)) return;
        int totalDmg = damage.GetValue() + str.GetValue();

        if(CanCrit())
        {
            totalDmg = CalCulateCritDmg(totalDmg);
        }
        totalDmg = CalculateDamageAfterArmor(_targetStat, totalDmg);
        _targetStat.TakeDamage(totalDmg);
        DoMagicalDmg(_targetStat);
    }

    private int CalculateDamageAfterArmor(CharacterStats _targetStat, int totalDmg)
    {
        if(_targetStat.isChilled)
        {
            totalDmg -= Mathf.RoundToInt( _targetStat.armor.GetValue() * 0.8f);
        }
        else
        {
            totalDmg -= _targetStat.armor.GetValue();

        }
        totalDmg = Mathf.Clamp(totalDmg, 0, int.MaxValue);
        return totalDmg;
    }

    private bool CalculateEvasionBeforeTakeDmg(CharacterStats _targetStat)
    {
        int totalEvasion = _targetStat.evasion.GetValue() + _targetStat.agi.GetValue();


        if (isShocked) totalEvasion -= 20;
        if (Random.Range(0, 100) < totalEvasion)
        {
            Debug.Log("Miss");
            return true;
        }
        return false;
    }

    private bool CanCrit()
    {
        int totalCritChance = critChance.GetValue() + agi.GetValue();

        if (Random.Range(0, 100) <= totalCritChance) return true;

        return false;
    }

    private int CalCulateCritDmg(int dmg)
    {
        float totalCritPower = (critPower.GetValue() + str.GetValue()) * 0.01f;
        float critDmg = dmg * totalCritPower;

        return Mathf.RoundToInt(critDmg);
    }

    public virtual void TakeDamage(int dmg)
    {
        DecreaseHPbyDmg(dmg);
        if(curentHp <= 0)
        {
            Die();
        }
    }

    protected virtual void DecreaseHPbyDmg(int dmg)
    {
        curentHp -= dmg;
        if (onHPChanged != null)
            onHPChanged();
    }

    public virtual void IncreaseHP(int amount)
    {
        curentHp += amount;

        if(curentHp > GetMaxHealth())
            curentHp = GetMaxHealth();

        if(onHPChanged != null) onHPChanged();
    }

    public int GetMaxHealth()
    {
        return maxHp.GetValue() + vit.GetValue() * 5;
    }

    protected virtual void Die()
    {
        
    }
}
