using DG.Tweening;
using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;

[RequireComponent(typeof(Enemy.Enemy))]
public class OnEnemyDamage : MonoBehaviour
{
    public float effectDuration = 0.5f;

    [SerializeField, HideInInspector]
    private Enemy.Enemy stats;

    [SerializeField, HideInInspector]
    private SpriteRenderer spriteRenderer;

    [SerializeField, HideInInspector]   
    private Color defaultColor;

    private void OnValidate()
    {
        stats = GetComponent<Enemy.Enemy>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultColor = spriteRenderer.color;
    }
    private void Start()
    {        
        stats.HealthChanged += Stats_OnDamaged;
    }

    private void Stats_OnDamaged()
    {
        spriteRenderer.DOBlendableColor(Color.red, effectDuration).SetEase(Ease.OutQuad).SetLoops(2, LoopType.Yoyo);
    }
}
