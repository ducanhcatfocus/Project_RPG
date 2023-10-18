using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarUI : MonoBehaviour
{
    private Transform transformparent;
    private Slider slider;
    private CharacterStats characterStats;
    void Start()
    {
        transformparent = transform.parent;
        slider = GetComponentInChildren<Slider>();
        characterStats = GetComponentInParent<CharacterStats>();

        characterStats.onHPChanged += UpdateHealthUI;
        UpdateHealthUI();
    }

    // Update is called once per frame
    void Update()
    {
        FlipUI();
     
    }

    private void UpdateHealthUI()
    {
        slider.maxValue = characterStats.GetMaxHealth();
        slider.value = characterStats.curentHp;
        if(slider.value <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void FlipUI()
    {
        if (transformparent.localScale.x < 0)
        {
            transform.localScale = new Vector3(-0.005f, transform.localScale.y, transform.localScale.z);
        }
        if (transformparent.localScale.x > 0)
        {
            transform.localScale = new Vector3(0.005f, transform.localScale.y, transform.localScale.z);
        }
    }

    private void OnDisable()
    {
        characterStats.onHPChanged -= UpdateHealthUI;
    }
}
