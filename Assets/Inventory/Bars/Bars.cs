using Player;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Bars : MonoBehaviour
{
    private PlayerStats _playerStats;
    [SerializeField] private Slider _healthbar, _manabar;
    [Inject]
    private void Inject(PlayerStats playerStats)
    {
        _playerStats = playerStats;
    }

    void Update()
    {
       
        _healthbar.value = _playerStats.CurrentHealth/100f;
        _manabar.value = _playerStats.CurrentMana / 100f;
    }
    
}
