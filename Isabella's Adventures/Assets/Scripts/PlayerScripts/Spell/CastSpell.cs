using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PlayerScripts.Spell
{
    public class CastSpell : MonoBehaviour
    {
        [SerializeField] private Button _cast;
        [SerializeField] private int _maxManaPlayer;
        [SerializeField] private Transform _castPos;
        [SerializeField] private BaseSpell _spell;

        private int _currentMana;
        public event UnityAction<int, int> ManaChanged;
        public event UnityAction<int, int> ManaTextChanged;

        private void Start()
        {
            _currentMana = _maxManaPlayer;
            ManaTextChanged?.Invoke(_currentMana, _maxManaPlayer);
        }

        private void OnEnable() => 
            _cast.onClick.AddListener(Cast);

        private void OnDisable() => 
            _cast.onClick.RemoveListener(Cast);

        private void Cast()
        {
            if (_currentMana != 0)
            {
                var spell = CreateSpell();
                _currentMana -= spell.ManaSpell;
                ManaChanged?.Invoke(_currentMana, _maxManaPlayer);
                ManaTextChanged?.Invoke(_currentMana, _maxManaPlayer);
            }            
        }

        private BaseSpell CreateSpell()
        {
            if (transform.localScale.x == 1)
            {
                var spell = Instantiate(_spell, _castPos.position, Quaternion.Euler(0, 0, 0));
                return spell;
            }

            Instantiate(_spell, _castPos.position, Quaternion.Euler(0, 180, 0));
            return _spell;
        }
    }
}