using DG.Tweening;
using Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Bullet))]
[RequireComponent(typeof(Rigidbody2D))]
public class ProjectileOnHitFreeze : MonoBehaviour
{
    public float StayTime = 2f;

    private LayerMask excludeLayers;

    private Rigidbody2D rb;
    private ArrowRotation arrowRotation = null;

    private List<Enemy.Enemy> enemiesListened = new List<Enemy.Enemy>();
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        excludeLayers = rb.includeLayers;

        GetComponent<Bullet>().OnHit += OnHit;

        if (TryGetComponent<ArrowRotation>(out ArrowRotation arrowRotation))
        {
            this.arrowRotation = arrowRotation;
        }
    }

    private void OnEnable()
    {
        rb.isKinematic = false;
        rb.simulated = true;

        rb.includeLayers = excludeLayers;

        StopAllCoroutines();
    }

    private void OnHit(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamagable target))
        {
            rb.excludeLayers = ~0;

            DOTween.To(() => rb.velocity, x => rb.velocity = x, Vector2.zero, 0.1f).SetEase(Ease.InOutCubic).OnComplete(() =>
            {
                rb.isKinematic = true;
                rb.velocity = Vector3.zero;
                rb.simulated = false;
            }).OnUpdate(() =>
            {
                if (arrowRotation != null)
                {
                    if (rb.velocity.sqrMagnitude < 1f)
                    {
                        arrowRotation.canRotate = false;
                    }
                }
            });

            rb.transform.SetParent(collision.transform, true);

            if (collision.TryGetComponent(out Enemy.Enemy enemy))
            {
                enemy.Died += ReturnToPool;
                enemiesListened.Add(enemy);
            }

            StartCoroutine(ReturnToPoolAfterTime());
        }
    }

    private IEnumerator ReturnToPoolAfterTime()
    {
        yield return new WaitForSeconds(StayTime);

        ReturnToPool();
    }

    private void ReturnToPool()
    {
        transform.SetParent(null);
        ObjectPool.Instance.ReternObject(gameObject);

        foreach (var enemy in enemiesListened)
        {
            enemy.Died -= ReturnToPool;
        }
    }
}
