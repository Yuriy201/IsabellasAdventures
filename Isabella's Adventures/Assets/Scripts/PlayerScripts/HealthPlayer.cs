using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerScripts
{
    public class HealthPlayer : MonoBehaviour
    {
        [SerializeField] private Player _player;
        public Slider _slider;
        [SerializeField] private TMP_Text _barText;

        private void OnEnable()
        {
            _player.HealthChanged += OnValueChanged;
            _player.HealthTextChanged += OnValueTextChanged;
            _slider.value = 1;
        }

        private void OnDisable()
        {
            _player.HealthChanged -= OnValueChanged;
            _player.HealthTextChanged -= OnValueTextChanged;
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