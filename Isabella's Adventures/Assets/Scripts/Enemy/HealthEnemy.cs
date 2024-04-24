using Script.Enemy.EnemyWithDamage;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Enemy
{
    public class HealthEnemy : MonoBehaviour
    {
        [SerializeField] private EnemyWithDamage _enemy;
        [SerializeField] private Slider _slider;
        [SerializeField] private TMP_Text _barText;

        private void OnEnable()
        {
            _enemy.EnemyHealthCheanged += OnValueChanged;
            _enemy.EnemyHealthTextChanged += OnValueTextChanged;
            _slider.value = 1;
        }

        private void OnDisable()
        {
            _enemy.EnemyHealthCheanged -= OnValueChanged;
            _enemy.EnemyHealthTextChanged -= OnValueTextChanged;
        }

        private void OnValueChanged(int value, int maxValue)
        {
            _slider.value = (float)value / maxValue;
        }

        private void OnValueTextChanged(int value, int maxValue)
        {
            _barText.text = $"{(float)value} / {maxValue}";
        }
    }
}