using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

    [SerializeField]
    private Image health_stats, stamina_stats;

    public void Display_HealthStats(float healthValue)
    {
        healthValue /= 100f;

        health_stats.fillAmount = healthValue;
    }

    public void Display_StaminaStats(float staminaValue)
    {
        staminaValue /= 100f;

        stamina_stats.fillAmount = staminaValue;
    }
}
