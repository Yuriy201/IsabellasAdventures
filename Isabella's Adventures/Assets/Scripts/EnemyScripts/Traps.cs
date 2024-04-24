using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps : MonoBehaviour
{
    [SerializeField] public int damage;
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }
    private GameObject parent;
    public GameObject Parent
    {
        get { return parent; }
        set { parent = value; }
    }
    [SerializeField] private Animator animator;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == parent)
            return;

        if (GameManager.Instance.healthContainer.ContainsKey(col.gameObject))
        {
            var health = GameManager.Instance.healthContainer[col.gameObject];
            health.TakeHit(damage, gameObject);
            {
                animator.SetBool("Damaged", true);
            }
        }
    }
}
