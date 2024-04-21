using System;
using System.Collections;
using System.Collections.Generic;

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
    }
}
