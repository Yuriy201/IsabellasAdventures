using PlayerScripts.Spell;
using UnityEngine;

namespace PlayerScripts.Mana
{
    public class ManaBar : Bar
    {
        [SerializeField] private CastSpell _player;

        private void OnEnable()
        {
            _player.ManaChanged += OnValueChanged;
            _player.ManaTextChanged += OnValueTextChanged;
        }

        private void OnDisable()
        {
            _player.ManaChanged -= OnValueChanged;
            _player.ManaTextChanged -= OnValueTextChanged;
        }
    }
}