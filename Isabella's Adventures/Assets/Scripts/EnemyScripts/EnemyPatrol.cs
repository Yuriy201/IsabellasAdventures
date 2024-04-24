using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] public GameObject LeftBorder;
    [SerializeField] public GameObject RightBorder;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private bool isRightDirection;
    [SerializeField] private float speed = 2;

    public float Speed
    {
        get { return speed; }
        set
        {
            if (value > 5)
                speed = value;
        }
    }

    private Vector3 direction;
    [SerializeField] private GroundDetection groundDetection;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private CollisionDamage collisionDamage;


    void Update()
    {
        if (groundDetection.isGrounded)
        {
            if (transform.position.x > RightBorder.transform.position.x
                || collisionDamage.Direction < 0)
                isRightDirection = false;
            else if (transform.position.x < LeftBorder.transform.position.x
                     || collisionDamage.Direction > 0)
                isRightDirection = true;
            rb.velocity = isRightDirection ? Vector2.right : Vector2.left;
            rb.velocity *= speed;
        }

        if (rb.velocity.x > 0)
            spriteRenderer.flipX = true;
        if (rb.velocity.x < 0)
            spriteRenderer.flipX = false;
    }
}