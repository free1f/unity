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
        private int _currentHealth;
        private int _currentStamina;
        public event Action OnDeath;

        public override void Init()
        {
            _currentHealth = HealthStat.MaxValue;
            HealthStat.Change(_currentHealth);
            _currentStamina = StaminaStat.MaxValue;
            StaminaStat.Change(_currentStamina);
        }
    }
}