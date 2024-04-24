using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] public int damage;
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }
    float moveSpeed = 7f;
    private Rigidbody2D rigidboby;
    Player target;
    Vector2 moveDirecrion;
    void Start()
    {
        rigidboby = GetComponent<Rigidbody2D>();
        target = GameObject.FindObjectOfType<Player>();
        moveDirecrion = (target.transform.position - transform.position).normalized*moveSpeed;
        rigidboby.velocity = new Vector2(moveDirecrion.x, moveDirecrion.y);
        Destroy(gameObject, 2f);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Destroy(gameObject);
        }

        if (GameManager.Instance.healthContainer.ContainsKey(col.gameObject))
            {
                var health = GameManager.Instance.healthContainer[col.gameObject];
                health.TakeHit(damage, gameObject);
            }
        }
}
