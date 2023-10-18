using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneSkill : Skill
{
    [SerializeField] private GameObject clonePrefab;
    [SerializeField] private float cloneDuration;
    [SerializeField] private bool canAttack;

    public void CreateClone(Transform newTransform)
    {
        GameObject newClone = Instantiate(clonePrefab, newTransform.position, Quaternion.identity);
        newClone.transform.localScale = newTransform.localScale;
        newClone.GetComponent<SpriteRenderer>().color = Color.black;
        newClone.GetComponent<CloneController>().SetUpClone(cloneDuration, canAttack); 
    }
}
