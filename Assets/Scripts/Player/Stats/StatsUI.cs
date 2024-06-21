using UnityEngine.UI;
using UnityEngine;
using Zenject;
using TMPro;

namespace Player
{
    public class StatsUI : MonoBehaviour, IManaAdder, IHealthAdder
    {
        [SerializeField] private Image _healthBar;
        [SerializeField] private Image _manaBar;
        [SerializeField] private TextMeshProUGUI _expText;

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

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                _playerStats.AddHealth(this);
                _playerStats.AddMana(this);
            }
        }

        private void UpdateUI()
        {
            _healthBar.fillAmount = (float)_playerStats.CurrentHealth / _playerStats.MaxHealth;
            _manaBar.fillAmount = (float)_playerStats.CurrentMana / _playerStats.MaxMana;

            int remExp = 0;
            ExperienceInfo.CalculateLevel(_playerStats.Experience, out remExp);

            _expText.text = $"Level {_playerStats.Level}: " + remExp  + "/" + ExperienceInfo.GetExpForNext(_playerStats.Experience);
        }

        private void OnEnable() => _playerStats.OnStateChanged += UpdateUI;
        private void OnDisable() => _playerStats.OnStateChanged -= UpdateUI;

        public int GetManaBoostValue()
        {
            return 10;
        }

        public int GetHealthBoostValue()
        {
            return 10;
        }
    }
}