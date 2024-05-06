using UnityEngine;

public interface IEnemyStates
{
    public void Walk();
    public void Attack(GameObject player);
    public void Die();
}
