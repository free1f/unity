using System;
using System.Collections;
using System.Collections.Generic;
using Freelf.Patterns;
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
        private Observer<bool> _onDayTime;
        private Observer<int> _onCurrentHour;
        public event Action OnSunrise = delegate {};
        public event Action OnSunset = delegate {};
        public event Action OnHourChange = delegate {};

        public TimeOperator(TimeSettings timeSettings)
        {
            _timeSettings = timeSettings;
            _currentTime = DateTime.Now + TimeSpan.FromHours(_timeSettings.initialTime);
            _sunriseTime = TimeSpan.FromHours(_timeSettings.sunriseTime);
            _sunsetTime = TimeSpan.FromHours(_timeSettings.sunsetTime);
            _onDayTime = new Observer<bool>(IsDay());
            _onCurrentHour = new Observer<int>(_currentTime.Hour);
            _onDayTime.AddListener(isDay => (isDay ? OnSunrise : OnSunset)?.Invoke());
            _onCurrentHour.AddListener(_ => OnHourChange?.Invoke());
        }

        public void UpdateTime(float deltaTime)
        {
            _currentTime = _currentTime.AddSeconds(deltaTime * _timeSettings.timeScale);
            _onDayTime.Value = IsDay();
            _onCurrentHour.Value = _currentTime.Hour;
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
