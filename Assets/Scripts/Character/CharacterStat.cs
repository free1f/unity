using System;
using System.Collections;
using System.Collections.Generic;
using Freelf.Character.Interfaces;
using Freelf.Stats;
using Freelf.Character.DataTransfer;
using UnityEngine;

namespace Freelf.Character
{
    public class CharacterStat : CharacterComponent, IAttached<DamageData>
    {
        public VitalityStat HealthStat;
        public VitalityStat StaminaStat;
        public int CurrentStamina => StaminaStat.CurrentValue.Value;
        public event Action OnDeath;
        private DamageData _damageData;

        public override void Init()
        {
            HealthStat.CurrentValue.AddListener(HealthChange);
            HealthStat.Change(HealthStat.MaxValue);
            StaminaStat.Change(StaminaStat.MaxValue);
            _damageData.OnDamage += SetHealth;
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

        public void Attached(ref DamageData value)
        {
            _damageData = value;
        }
    }
}