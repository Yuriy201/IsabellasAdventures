using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalactite : State
{
    public Rigidbody2D rb;
    public int damage;
    public Player target;

  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            rb.isKinematic = false;
           
        }
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Attack();
    }

    private void Attack()
    {
        target.ApplyDamage(damage);
        Destroy(gameObject);
    }
}
