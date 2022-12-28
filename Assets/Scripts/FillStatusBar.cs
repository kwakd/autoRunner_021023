using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillStatusBar : MonoBehaviour
{
    public HealthSystem myHealth;
    public Image fillImage;

    private Slider slider; 

    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        //if player RIP then disable fillImage
        if(slider.value <= slider.minValue)
        {
            fillImage.enabled = false;
        }
        else if(slider.value > slider.minValue && !fillImage.enabled)
        {
            fillImage.enabled = true;
        }

        float fillValue = myHealth.currentHealth / myHealth.maxHealth;

        //if lifebar goes below n/3
        if(fillValue <= slider.maxValue / 3)
        {
            //critical condition
            fillImage.color = Color.red;
        }
        else if(fillValue > slider.maxValue / 3)
        {
            //default condition
            fillImage.color = Color.blue;
        }

        slider.value = fillValue;
        
    }
}
