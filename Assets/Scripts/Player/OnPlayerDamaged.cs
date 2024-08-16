using DG.Tweening;
using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class OnPlayerDamaged : MonoBehaviour
{
    public float effectDuration = 0.5f;

    private PlayerStats stats;
    private SpriteRenderer spriteRenderer;

    private Color defaultColor;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultColor = spriteRenderer.color;
        stats = GetComponent<PlayerController>().Stats;

        stats.OnDamaged += Stats_OnDamaged;
    }

    private void Stats_OnDamaged()
    {
        spriteRenderer.DOBlendableColor(Color.red, effectDuration).SetEase(Ease.OutQuad).SetLoops(2, LoopType.Yoyo);
    }
}
