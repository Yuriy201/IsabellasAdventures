using UnityEngine;
using System;

public class PlayerStats 
{
    public event Action StateChanged;
    public event Action OutOfManaTrying;

    public int CurrentHealth => _currentHealth;
    public int CurrentMana => _currentMana;
    public int MaxHealth => _maxHealth;
    public int MaxMana => _maxMana;

    [SerializeField] private int _maxHealth;
    [SerializeField] private int _maxMana;

    private int _currentHealth;
    private int _currentMana;

    public PlayerStats (int maxHealth, int maxMana, int currentHealth, int currentMana)
    {
        _maxHealth = maxHealth;
        _maxMana = maxMana;
        _currentHealth = currentHealth;
        _currentMana = currentMana;
    }   

    public void RemoveHealth(IDamageDiller damageDiller)
    {
        _currentHealth -= damageDiller.GetDamageValue();
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        Debug.Log(CurrentHealth + " " + MaxHealth);

        StateChanged?.Invoke();
    }

    public bool RemoveMana(IManaUser manaUser)
    {
        if (_currentMana - manaUser.GetManacost() < 0)
        {
            OutOfManaTrying?.Invoke();
            return false;
        }
        
        _currentMana -= manaUser.GetManacost();
        Debug.Log(CurrentMana + " " + MaxMana);

        StateChanged?.Invoke();
        return true;
    }

    public void AddHealth(IHealthAdder healthAdder)
    {
        _currentHealth += healthAdder.GetHealthBoost();
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        Debug.Log(CurrentHealth + " " + MaxHealth);

        StateChanged?.Invoke();
    }

    public void AddMana(IManaAdder manaAdder)
    {
        _currentMana += manaAdder.GetManaBoost();
        _currentMana = Mathf.Clamp(_currentMana, 0, _maxMana);
        Debug.Log(CurrentMana + " " + MaxMana);

        StateChanged?.Invoke();
    }
}
