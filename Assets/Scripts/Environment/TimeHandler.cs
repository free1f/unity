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
        void Start()
        {
            _timeOperator = new TimeOperator(timeSettings);
        }

        void Update()
        {
            _timeOperator.UpdateTime(Time.deltaTime);
            if (_timeOperator.IsDay())
            {
                Debug.Log("Day");
            }
            else
            {
                Debug.Log("Night");
            }
            timeText.text = _timeOperator.CurrentTime.ToString("hh:mm");
        }
    }
}

