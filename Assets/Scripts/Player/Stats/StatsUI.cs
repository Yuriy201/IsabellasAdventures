using UnityEngine.UI;
using UnityEngine;
using Zenject;

namespace Player
{
    public class StatsUI : MonoBehaviour, IManaAdder, IManaUser, IDamageDiller, IHealthAdder
    {
        [SerializeField] private Image _healthBar;
        [SerializeField] private Image _manaBar;

        private PlayerStats _playerStats;

        [Inject]
        private void Inject(PlayerStats playerStats)
        {
            _playerStats = playerStats;
        }

        private void Start()
        {
            UpdateUI();
        }

        private void Update()
        {
            if (_playerStats == null) return;

            if (Input.GetKeyDown(KeyCode.UpArrow)) _playerStats.AddHealth(this);
            if (Input.GetKeyDown(KeyCode.DownArrow)) _playerStats.RemoveHealth(this);
            if (Input.GetKeyDown(KeyCode.RightArrow)) _playerStats.AddMana(this);
            if (Input.GetKeyDown(KeyCode.LeftArrow)) _playerStats.RemoveMana(this);
        }

        private void UpdateUI()
        {
            _healthBar.fillAmount = (float)_playerStats.CurrentHealth / _playerStats.MaxHealth;
            _manaBar.fillAmount = (float)_playerStats.CurrentMana / _playerStats.MaxMana;
        }

        private void OnEnable() => _playerStats.StateChanged += UpdateUI;
        private void OnDisable() => _playerStats.StateChanged -= UpdateUI;

        public int GetManaBoost()
        {
            return 1;
        }

        public int GetManacost()
        {
            return 1;
        }

        public int GetDamageValue()
        {
            return 1;
        }

        public int GetHealthBoost()
        {
            return 1;
        }
    }
}