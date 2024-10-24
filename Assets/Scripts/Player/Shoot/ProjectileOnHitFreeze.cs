using DG.Tweening;
using Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Bullet))]
[RequireComponent(typeof(Rigidbody2D))]
public class ProjectileOnHitFreeze : MonoBehaviour
{
    [SerializeField]
    private float deccelerationRate = -1;

    [SerializeField]
    private float stopRotationSqrVelocity = 1f;

    private LayerMask excludeLayers;
    private LayerMask includeLayers;

    [SerializeField, HideInInspector]
    private Rigidbody2D rb;

    [SerializeField]
    private float arrowPickupEnableDelay = 0.5f;

    private WaitForSeconds arrowPickupDelayWFS;

    [SerializeField]
    private ArrowRotation arrowRotation = null;

    [SerializeField]
    private ArrowPickup arrowPickup = null;

    private DG.Tweening.Core.TweenerCore<Vector2, Vector2, DG.Tweening.Plugins.Options.VectorOptions> currentTween = null;

    [SerializeField]
    private Collider2D _collider;

    private Vector2 prevVelocity;

    private bool isPenetrating = false;

    private void OnValidate()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Awake()
    {
        excludeLayers = rb.excludeLayers;
        includeLayers = rb.includeLayers;

        GetComponent<Bullet>().OnHit += OnHit;

        if (TryGetComponent<ArrowRotation>(out ArrowRotation arrowRotation))
        {
            this.arrowRotation = arrowRotation;
        }

        arrowPickupDelayWFS = new WaitForSeconds(arrowPickupEnableDelay);
    }

    private void OnEnable()
    {
        FiredState();
    }

    private void FixedUpdate()
    {
        prevVelocity = rb.velocity;

        if (isPenetrating && arrowRotation != null)
        {
            if (rb.velocity.sqrMagnitude < stopRotationSqrVelocity)
            {
                arrowRotation.canRotate = false;
                rb.freezeRotation = true;
            }
        }
    }

    private void OnHit(Collider2D collision)
    {
        transform.SetParent(collision.transform);
        //Debug.LogWarning("HIT = " + collision.name);

        //rb.simulated = false;
        _collider.enabled = false;

        rb.includeLayers = ~-1;
        rb.excludeLayers = ~0;

        rb.velocity = prevVelocity;

        isPenetrating = true;

        float deccelerationTime = -Mathf.Abs(rb.velocity.magnitude) / deccelerationRate;

        currentTween = DOTween.To(() => rb.velocity, x => rb.velocity = x, Vector2.zero, deccelerationTime).SetEase(Ease.OutSine).OnComplete(() =>
        {
            rb.velocity = Vector2.zero;

            Debug.Log("ROTATION = " + rb.rotation);

            FreezeState();
            rb.simulated = true;

        }).SetLink(gameObject);

        //if (collision.TryGetComponent(out IDamagable target))
        //{
        //    if (collision.TryGetComponent(out Enemy.Enemy enemy))
        //    {
        //        rb.transform.SetParent(collision.transform, true);
        //        enemy.Died += HandleEnemyDeath;
        //        enemiesListened.Add(enemy);
        //    }
        //}
    }

    private void FiredState()
    {
        StopAllCoroutines();

        isPenetrating = false;
        
        if (currentTween != null)
        {
            currentTween.Kill(false);
            currentTween = null;
        }

        transform.SetParent(null);

        rb.simulated = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.freezeRotation = false;
        rb.includeLayers = includeLayers;
        rb.excludeLayers = excludeLayers;
        _collider.enabled = true;

        if (arrowPickup != null)
        {
            arrowPickup.enabled = false;
        }

        if (arrowRotation != null)
        {
            arrowRotation.canRotate = true;
            arrowRotation.lerp = false;
        }       
    }

    private void FreezeState()
    {
        StopAllCoroutines();

        isPenetrating = false;

        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.freezeRotation = true;
        rb.includeLayers = ~-1;
        rb.excludeLayers = ~-1;
        _collider.enabled = false;

        if (arrowRotation != null)
        {
            arrowRotation.lerp = false;
        }

        if (arrowPickup != null)
        {
            StartCoroutine(PickupDelay());
        }
    }

    private IEnumerator PickupDelay()
    {
        yield return arrowPickupDelayWFS;

        arrowPickup.enabled = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        FiredState();
    }
}
