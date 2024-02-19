using System;
using System.Collections;
using System.Collections.Generic;
using Freelf.Stats;
using UnityEngine;

namespace Freelf.Character
{
    public class CharacterStat : MonoBehaviour
    {
        public VitalityStat HealthStat;
        public VitalityStat StaminaStat;
        private int _currentHealth;
        private int _currentStamina;
        public event Action OnDeath;
        void Start()
        {
            _currentHealth = HealthStat.MaxValue;
            HealthStat.Change(_currentHealth);
            _currentStamina = StaminaStat.MaxValue;
            StaminaStat.Change(_currentStamina);
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.H)) {
                _currentHealth -= 10;
                HealthStat.Change(_currentHealth);
                if(_currentHealth == 0) OnDeath?.Invoke();
            }
            if(Input.GetKeyDown(KeyCode.J)) {
                _currentHealth += 10;
                HealthStat.Change(_currentHealth);
            }

            if(Input.GetKeyDown(KeyCode.B)) {
                _currentStamina -= 10;
                StaminaStat.Change(_currentStamina);
            }
            if(Input.GetKeyDown(KeyCode.N)) {
                _currentStamina += 10;
                StaminaStat.Change(_currentStamina);
            }
        }
    }
}
