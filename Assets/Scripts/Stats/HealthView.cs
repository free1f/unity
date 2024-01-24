using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    public HealthStat HealthStat;
    public Slider Slider;
    
    void Awake()
    {
        // Subscribtion to HealthStat
        HealthStat.OnChanged += ChangeSlider;
    }

    private void ChangeSlider(int value)
    {
        Slider.value = (float)value/(float)HealthStat.MaxValue;
    }
}
