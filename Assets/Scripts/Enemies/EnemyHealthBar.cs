using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _text;
    private int startHealth;

    public void Init(int health)
    {
        _slider.maxValue = health;
        _slider.value = _slider.maxValue;
        startHealth = health;
        _text.text = $"{health} / {startHealth}";
    }

    public void ChangeHealth(int currentHealth)
    {
        _slider.value = currentHealth;
        _text.text = $"{currentHealth} / {startHealth}";
    }
}
