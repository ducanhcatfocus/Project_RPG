using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIIngame : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private PlayerStats playerStats;
    void Start()
    {
        if(playerStats != null)
        {
            playerStats.onHPChanged += UpdateHealthUI;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateHealthUI()
    {
        slider.maxValue = playerStats.GetMaxHealth();
        slider.value = playerStats.curentHp;

    }
}
