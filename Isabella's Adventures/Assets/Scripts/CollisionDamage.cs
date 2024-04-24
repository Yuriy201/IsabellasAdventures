using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    public int damage = 10;
    [SerializeField] private Animator animator;
    private Health health;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private float direction;

    public float Direction

    {
        get { return direction; }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (GameManager.Instance.collisionContainer.ContainsKey(col.gameObject))
        {
            health = GameManager.Instance.collisionContainer[col.gameObject];
            direction = (col.transform.position - transform.position).x;
            animator.SetFloat("Direction", Mathf.Abs(direction));
        }
    }

    public void SetDamage()
    {
        if (health != null)
        {
            health.TakeHit(damage, gameObject);
            health = null;
            direction = 0;
            animator.SetFloat("Direction", 0F);
        }
    }
}