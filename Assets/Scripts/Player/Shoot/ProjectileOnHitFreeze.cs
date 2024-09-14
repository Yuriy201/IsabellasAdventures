using DG.Tweening;
using Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Bullet))]
[RequireComponent(typeof(Rigidbody2D))]
public class ProjectileOnHitFreeze : MonoBehaviour
{
    private LayerMask excludeLayers;
    private LayerMask includeLayers;
    private Rigidbody2D rb;
    private ArrowRotation arrowRotation = null;

    [SerializeField]
    private ArrowPickup arrowPickup = null;

    private DG.Tweening.Core.TweenerCore<Vector2, Vector2, DG.Tweening.Plugins.Options.VectorOptions> currentTween = null;

    private List<Enemy.Enemy> enemiesListened = new List<Enemy.Enemy>();
    private Collider2D _collider;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        excludeLayers = rb.excludeLayers;
        includeLayers = rb.includeLayers;

        GetComponent<Bullet>().OnHit += OnHit;

        if (TryGetComponent<ArrowRotation>(out ArrowRotation arrowRotation))
        {
            this.arrowRotation = arrowRotation;
        }
    }

    private void OnEnable()
    {
        FiredState();
    }

    private void OnHit(Collider2D collision)
    {
        //Debug.LogWarning("HIT = " + collision.name);

        rb.simulated = false;
        _collider.enabled = false;

        rb.includeLayers = ~-1;
        rb.excludeLayers = ~0;

        currentTween = DOTween.To(() => rb.velocity, x => rb.velocity = x, Vector2.zero, 0.1f).SetEase(Ease.InOutCubic).OnComplete(() =>
        {
            rb.velocity = Vector3.zero;

            FreezeState();
            rb.simulated = true;

        }).OnUpdate(() =>
        {
            if (arrowRotation != null)
            {
                if (rb.velocity.sqrMagnitude < 2f)
                {
                    arrowRotation.canRotate = false;
                }
            }
        }).SetLink(gameObject);

        if (collision.TryGetComponent(out IDamagable target))
        {
            if (collision.TryGetComponent(out Enemy.Enemy enemy))
            {
                rb.transform.SetParent(collision.transform, true);
                enemy.Died += HandleEnemyDeath;
                enemiesListened.Add(enemy);
            }
        }
    }

    private void FiredState()
    {
        StopAllCoroutines();
        
        if (currentTween != null)
        {
            currentTween.Kill(false);
            currentTween = null;
        }

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
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.freezeRotation = true;
        rb.includeLayers = ~-1;
        rb.excludeLayers = ~-1;
        _collider.enabled = false;

        if (arrowPickup != null)
        {
            arrowPickup.enabled = true;
            arrowRotation.lerp = false;
        }

        StopAllCoroutines();
    }

    private void HandleEnemyDeath()
    {
        transform.SetParent(null);

        foreach (var enemy in enemiesListened)
        {
            enemy.Died -= HandleEnemyDeath;
        }

        enemiesListened.Clear();

        FiredState();
        
        if (arrowRotation != null)
        {
            arrowRotation.lerp = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (rb.includeLayers != ~-1)
        {
            FreezeState();
        }      
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        FiredState();
    }
}
