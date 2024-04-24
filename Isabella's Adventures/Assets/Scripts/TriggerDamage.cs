using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDamage : MonoBehaviour
{
    [SerializeField] private int damage;

    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    [SerializeField] private bool isDestroyingAfterCollision;
    private IObjectDestroyer destroyer;
    public GameObject parent;

    public void Init(IObjectDestroyer destroyer)
    {
        this.destroyer = destroyer;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == parent)
            return;


        if (col.name == "Enemy")
        {
            col.GetComponent<EnemyHealth>().TakeDamage(damage);
        }


        if (GameManager.Instance.healthContainer.ContainsKey(col.gameObject))
        {
            var health = GameManager.Instance.healthContainer[col.gameObject];
            health.TakeHit(damage, gameObject);
        }

        if (isDestroyingAfterCollision)
        {
            if (destroyer == null)
                Destroy(gameObject);
            else destroyer.Destroy(gameObject);
        }
    }
}

public interface IObjectDestroyer
{
    void Destroy(GameObject gameObject);
}