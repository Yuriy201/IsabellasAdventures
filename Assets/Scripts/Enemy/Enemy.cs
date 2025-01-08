using UnityEngine;
using System;
using Sound;
using UnityEngine.UI;

namespace Enemy
{
    public abstract class Enemy : MonoBehaviour, IDamagable, IExperienceAdder
    {
        public event Action HealthChanged;
        public event Action Died;

        [field: SerializeField] public int MaxHealth { get; protected set; }
        [field: SerializeField] public float Health { get; protected set; }

        public Slider healthSlider;

        public void GetDamage(int damage)
        {
            if (damage <= 0)
                throw new InvalidOperationException("<color=red>Damage should be more then 0</color>");

            Health -= damage;
            Health = Mathf.Clamp(Health, 0, MaxHealth);
            HealthChanged?.Invoke();
            if (Health <= 0)
            {
                Died?.Invoke();
                Destroy(gameObject);
            }
            
        }
        
        public int GetExpValue() => UnityEngine.Random.Range(40, 70 + 1);
    }
}
