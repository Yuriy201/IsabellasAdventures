using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

namespace Enemy
{
    public abstract class Enemy : MonoBehaviour, IDamagable, IExperienceAdder
    {
        public event Action HealthChanged;
        public event Action Died;
        protected WolfSoundManager soundManager;

        [field: SerializeField] public int MaxHealth { get; protected set; }
        [field: SerializeField] public float Health { get; protected set; }

        public Slider healthSlider;

        private Coroutine damageSoundCoroutine;

        public void SetSoundManager(WolfSoundManager soundManager)
        {
            if (soundManager == null)
            {
                Debug.LogError("Попытка установить null в качестве звукового менеджера!");
                return;
            }

            this.soundManager = soundManager;
        }

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

            if (damageSoundCoroutine != null)
            {
                StopCoroutine(damageSoundCoroutine);
            }

            if (Health > 0)
            {
                damageSoundCoroutine = StartCoroutine(PlayDamageAndHurtSounds());
            }
            else
            {
                damageSoundCoroutine = StartCoroutine(HandleDeath());
            }
        }

        private IEnumerator PlayDamageAndHurtSounds()
        {
            soundManager?.PlayDamageSound();
            yield return new WaitForSeconds(0.8f);
            soundManager?.PlayRandomHurtSound();
        }

        private IEnumerator PlayDamageAndDeathSounds()
        {
            soundManager?.PlayDamageSound();
            yield return new WaitForSeconds(0.8f);
            soundManager?.PlayDeathSound();
        }

        private IEnumerator HandleDeath()
        {
            yield return StartCoroutine(PlayDamageAndDeathSounds());
            Died?.Invoke();
            Destroy(gameObject);
        }

        public int GetExpValue() => UnityEngine.Random.Range(40, 70 + 1);
    }
}
