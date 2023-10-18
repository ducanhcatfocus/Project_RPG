using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Thunder Effect", menuName = "Data/Item Effect/ThunderStrike")]

public class ThunderStrikeEffect : ItemEffect
{
    [SerializeField] private GameObject thunderStrikePrefab;
    public override void ExecuteEffect(Transform enemyPos)
    {
        GameObject newThunderStrike = Instantiate(thunderStrikePrefab, enemyPos.position, Quaternion.identity);
        Destroy(newThunderStrike, 0.5f);
    }
}
