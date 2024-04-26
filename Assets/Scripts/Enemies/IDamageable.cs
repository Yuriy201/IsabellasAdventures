using UnityEngine;

public interface IDamageable
{
    public void TakeDamage(int damage);
}

public interface IInteraction
{
    public void Interaction(GameObject Other);
}
