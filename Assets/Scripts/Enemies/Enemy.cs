using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private int _health;
    // [SerializeField] private AudioSource _soundSource;
    // [SerializeField] private AudioClip WolfDamage;
    // [SerializeField] private AudioClip WolfDie;
    [SerializeField] private EnemyHealthBar _healthBar;

    private int _currentHealth;

    private void Awake()
    {
        _currentHealth = _health;
        _healthBar.Init(_currentHealth);
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        Debug.Log(_currentHealth);
        _healthBar.ChangeHealth(_currentHealth);
        // _soundSource.PlayOneShot(WolfDamage);

        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}


