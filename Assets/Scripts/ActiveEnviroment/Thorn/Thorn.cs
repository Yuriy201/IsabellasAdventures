using UnityEngine;
using Player;

public class Thorn : MonoBehaviour, IDamageDiller
{
    private int _damage;

    public int GetDamageValue() => _damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController player))
        {
            _damage = player.Stats.MaxHealth;
            player.Stats.RemoveHealth(this);
        }
    }
}
