using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Freelf.Stats
{
    public class VitalityView : MonoBehaviour
    {
        public VitalityStat vitalityStat;
        public Slider Slider;
        
        void Awake()
        {
            Slider.maxValue = vitalityStat.MaxValue;
            Slider.minValue = vitalityStat.MinValue;
            // Subscribtion to vitalityStat
            vitalityStat.CurrentValue.AddListener(ChangeSlider);
        }

        private void ChangeSlider(int value)
        {
            Slider.value = value;
        }
    }
}