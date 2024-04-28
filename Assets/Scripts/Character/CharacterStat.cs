using System;
using System.Collections;
using System.Collections.Generic;
using Freelf.Character.Interfaces;
using Freelf.Stats;
using UnityEngine;

namespace Freelf.Character
{
    public class CharacterStat : CharacterComponent
    {
        public VitalityStat HealthStat;
        public VitalityStat StaminaStat;
        public int CurrentStamina => StaminaStat.CurrentValue.Value;
        public event Action OnDeath;

        public override void Init()
        {
            HealthStat.CurrentValue.AddListener(HealthChange);
            HealthStat.Change(HealthStat.MaxValue);
            StaminaStat.Change(StaminaStat.MaxValue);
        }

        private void HealthChange(int value)
        {
            if (value == HealthStat.MinValue)
            {
                OnDeath?.Invoke();
            }
        }

        public void SetHealth(int value)
        {
            var result = HealthStat.CurrentValue.Value + value;
            HealthStat.Change(result);
        }

        public void SetStamina(int value)
        {
            var result = StaminaStat.CurrentValue.Value + value;
            StaminaStat.Change(result);
        }
    }
}