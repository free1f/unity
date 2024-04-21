using System.Collections;
using System.Collections.Generic;
using Freelf.Environment;
using TMPro;
using UnityEngine;

namespace Freelf.Environment
{
    public class TimeHandler : MonoBehaviour
    {
        public TimeSettings timeSettings;
        private TimeOperator _timeOperator;
        public TMP_Text timeText;
        public Light sunLight;
        void Start()
        {
            _timeOperator = new TimeOperator(timeSettings);
        }

        void Update()
        {
            HandleTime();
            HandleSun();
        }

        private void HandleTime()
        {
            _timeOperator.UpdateTime(Time.deltaTime);
            timeText.text = _timeOperator.CurrentTime.ToString("hh:mm");
        }

        private void HandleSun()
        {
            float sunRotation = _timeOperator.CalculateSunRotation();
            sunLight.transform.rotation = Quaternion.AngleAxis(sunRotation, Vector3.right);
        }
    }
}

