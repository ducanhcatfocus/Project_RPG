using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item Effect", menuName = "Data/Item Effect")]

public class ItemEffect : ScriptableObject
{
    public virtual void ExecuteEffect(Transform enemyPos) 
    { 
        Debug.Log("Effect executed");
    }
}
