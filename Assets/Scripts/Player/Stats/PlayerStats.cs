using UnityEngine;
using System;

namespace Player
{
    public class PlayerStats
    {
        public event Action StateChanged;
        public event Action OutOfManaTrying;
        public event Action OutOfMoneyTrying;

        public int CurrentHealth => _currentHealth;
        public int CurrentMana => _currentMana;
        public int MaxHealth => _maxHealth;
        public int MaxMana => _maxMana;
        public int Experience => _experience;
        public int Coin => _coin;
        public int Level => ExperienceInfo.CalculateLevel(_experience);
        

        private int _maxHealth;
        private int _maxMana;
        private int _currentHealth;
        private int _currentMana;

        private int _experience;
        private int _coin;

        public PlayerStats(StatsContainer stats, int experience = 0, int coin = 0)
        {
            _maxHealth = stats.MaxHealth;
            _maxMana = stats.MaxMana;
            _currentHealth = stats.CurrentHealth;
            _currentMana = stats.CurrentMana;

            _experience = experience;
            _coin = coin;
        }

        public void RemoveHealth(IDamageDiller damageDiller)
        {
            int value = damageDiller.GetDamageValue();

            if (value <= 0)
                throw new InvalidOperationException("Damage value should be more than 0!");

            _currentHealth -= value;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);

            StateChanged?.Invoke();
        }

        public bool RemoveMana(IManaUser manaUser)
        {
            int value = manaUser.GetManacost();

            if (value <= 0)
                throw new InvalidOperationException("Manacost value should be more than 0!");

            if (_currentMana - value < 0)
            {
                OutOfManaTrying?.Invoke();
                return false;
            }

            _currentMana -= value;

            StateChanged?.Invoke();
            return true;
        }

        public void AddHealth(IHealthAdder healthAdder)
        {
            int value = healthAdder.GetHealthBoostValue();

            if (value <= 0)
                throw new InvalidOperationException("Healthboost value should be more than 0!");

            _currentHealth += value;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);

            StateChanged?.Invoke();
        }

        public void AddMana(IManaAdder manaAdder)
        {
            int value = manaAdder.GetManaBoostValue();

            if (value <= 0)
                throw new InvalidOperationException("Manaboost value should be more than 0!");

            _currentMana += value;
            _currentMana = Mathf.Clamp(_currentMana, 0, _maxMana);

            StateChanged?.Invoke();
        }

        public void AddExperience(IExperienceAdder expAdder)
        {
            int value = expAdder.GetExpValue();
            int oldLevel = Level;

            if (value <= 0)
                throw new InvalidOperationException("Experience value should be more than 0!");

            _experience += value;
            if (Level != oldLevel) IncreaseMaxValues();

            StateChanged?.Invoke();
        }

        

        public void AddMoney(ICoinAdder coinAdder)
        {
            if (coinAdder == null)
                throw new ArgumentNullException("coinAdder is null!");

            _coin += 1;
            StateChanged?.Invoke();
        }

        public bool UseMoney(ICoinUser coinUser)
        {
            int value = coinUser.GetPrice();

            if (value <= 0)
                throw new InvalidOperationException("Price value should be more than 0!");

            if (_coin - value < 0)
            {
                OutOfManaTrying?.Invoke();
                return false;
            }

            _coin -= value;

            StateChanged?.Invoke();
            return true;
        }

        private void IncreaseMaxValues()
        {
            _maxHealth += 10;
            _maxMana += 10;

            Debug.Log($"<color=green>{CurrentHealth}/{MaxHealth} | {CurrentMana}/{MaxMana}</color>");
        }
    }
}
