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
            // Subscribtion to vitalityStat
            vitalityStat.OnChanged += ChangeSlider;
        }

        private void ChangeSlider(int value)
        {
            Slider.value = (float)value/(float)vitalityStat.MaxValue;
        }
    }
}