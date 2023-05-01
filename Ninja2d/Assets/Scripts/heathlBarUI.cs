using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class heathlBarUI : MonoBehaviour
{

    public PlayerStats playerStats;
    public Slider heathSlider;
    // Start is called before the first frame update
    void Start()
    {
        playerStats.onDamageTook += refreshUI;
        heathSlider.maxValue = playerStats.maxHealth;
        heathSlider.value = playerStats.currentHealth;
       

    }

    public void refreshUI()
    {
        heathSlider.value = playerStats.currentHealth;
       
    }

    
   
}
