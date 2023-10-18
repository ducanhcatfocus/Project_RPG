using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    static SkillManager instance;

    public static SkillManager Instance => instance;

    public DashSkill dash { get; private set; }

    public CloneSkill clone { get; private set; }


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;

        }
    }


    private void Start()
    {
        dash = GetComponent<DashSkill>();
        clone = GetComponent<CloneSkill>();
    }
}
