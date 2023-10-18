using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chill Effect", menuName = "Data/Item Effect/ChillStrike")]

public class ChillStrikeEffect : ItemEffect
{
    [SerializeField] private GameObject chillStrikePrefab;
    public override void ExecuteEffect(Transform enemyPos)
    {
        GameObject newChillStrike = Instantiate(chillStrikePrefab, enemyPos.position, Quaternion.identity);
        Destroy(newChillStrike, 0.5f);
    }
}