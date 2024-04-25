using UnityEngine;

public class DamageSource : MonoBehaviour
{
    private int _damage;

    public void Init(int damage)
    {
        _damage = damage;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            Debug.Log("Attacking player!");
        }
    }
}
