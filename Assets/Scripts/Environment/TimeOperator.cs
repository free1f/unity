using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Freelf.Environment
{
    public class TimeOperator
    {
        private readonly TimeSettings _timeSettings;
        private DateTime _currentTime;
        public DateTime CurrentTime => _currentTime;
        private TimeSpan _sunriseTime;
        private TimeSpan _sunsetTime;

        public TimeOperator(TimeSettings timeSettings)
        {
            _timeSettings = timeSettings;
            _currentTime = DateTime.Now + TimeSpan.FromHours(_timeSettings.initialTime);
            _sunriseTime = TimeSpan.FromHours(_timeSettings.sunriseTime);
            _sunsetTime = TimeSpan.FromHours(_timeSettings.sunsetTime);
        }

        public void UpdateTime(float deltaTime)
        {
            _currentTime = _currentTime.AddSeconds(deltaTime * _timeSettings.timeScale);
        }

        public bool IsDay()
        {
            return _currentTime.TimeOfDay >= _sunriseTime && _currentTime.TimeOfDay < _sunsetTime;
        }

        public float CalculateSunRotation()
        {
            bool isDayTime = IsDay();
            float startAngle = isDayTime ? 0f : 180f;
            TimeSpan startTime = isDayTime ? _sunriseTime : _sunsetTime;
            TimeSpan endTime = isDayTime ? _sunsetTime : _sunriseTime;
            TimeSpan totalTime = CalculateTimeDifference(startTime, endTime);
            TimeSpan elapsedTime = CalculateTimeDifference(startTime, _currentTime.TimeOfDay);
            double timePercentage = elapsedTime.TotalMinutes / totalTime.TotalMinutes;
            return Mathf.Lerp(startAngle, startAngle + 180f, (float)timePercentage);
        }

        private TimeSpan CalculateTimeDifference(TimeSpan from, TimeSpan to)
        {
            TimeSpan difference = to - from;
            return difference.TotalHours < 0 ? difference + TimeSpan.FromHours(24) : difference;
        }
    }
}
