using System;

namespace Freelf.Character.DataTransfer
{
    public class StatData
    {
        public event Action OnEmptyHealth;
        public event Action OnEmptyStamina;
        public event Action<int> OnHealthChanged;
        public event Action<int> OnStaminaChanged;

        private int _currentHealth;
        private int _currentStamina;

        public int CurrentHealth
        {
            get => _currentHealth;
            set
            {
                if (_currentHealth == value) return;
                _currentHealth = value;
                OnHealthChanged?.Invoke(_currentHealth);
                if (_currentHealth <= 0) OnEmptyHealth?.Invoke();
            }
        }

        public int CurrentStamina
        {
            get => _currentStamina;
            set
            {
                if (_currentStamina == value) return;
                _currentStamina = value;
                OnStaminaChanged?.Invoke(_currentStamina);
                if (_currentStamina <= 0) OnEmptyStamina?.Invoke();
            }
        }
    }
}