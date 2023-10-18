using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Burn Effect", menuName = "Data/Item Effect/BurnStrike")]

public class BurnStrikeEffect : ItemEffect
{
    [SerializeField] private GameObject burnStrikePrefab;
    public override void ExecuteEffect(Transform enemyPos)
    {
        GameObject newBurnStrike = Instantiate(burnStrikePrefab, enemyPos.position, Quaternion.identity);
        Destroy(newBurnStrike, 0.5f);
    }
}
