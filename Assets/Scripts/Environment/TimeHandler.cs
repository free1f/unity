using System;
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
        public Light moonLight;
        public float maxSunIntensity = 1f;
        public float maxMoonIntensity = 0.5f;
        public AnimationCurve lightIntensityCurve;
        public Material skyboxMaterial;

        void Start()
        {
            _timeOperator = new TimeOperator(timeSettings);
        }

        void Update()
        {
            HandleTime();
            HandleSun();
            UpdateLight();
            UpdateSkybox();
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

        private void UpdateLight()
        {
            var dotProduct = Vector3.Dot(sunLight.transform.forward, Vector3.down);
            sunLight.intensity = Mathf.Lerp(0, maxSunIntensity, lightIntensityCurve.Evaluate(dotProduct));
            moonLight.intensity = Mathf.Lerp(maxMoonIntensity, 0, lightIntensityCurve.Evaluate(dotProduct));
        }

        private void UpdateSkybox()
        {
            var dotProduct = Vector3.Dot(sunLight.transform.forward, Vector3.up);
            float blend = Mathf.Lerp(0, 1, lightIntensityCurve.Evaluate(dotProduct));
            skyboxMaterial.SetFloat("_Blend", blend);
        }
    }
}

