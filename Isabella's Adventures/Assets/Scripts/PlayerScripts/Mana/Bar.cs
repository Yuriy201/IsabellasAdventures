using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerScripts.Mana
{
    public abstract class Bar : MonoBehaviour
    {
        [SerializeField] protected Slider Slider;
        [SerializeField] private TMP_Text _barText;

        protected void OnValueChanged(int value, int maxValue)
        {
            Slider.value = (float)value / maxValue;
        }
        
        public void OnValueTextChanged(int value, int maxValue)
        {
            _barText.text = $"{(float)value} / {maxValue}";
        }
    }
}