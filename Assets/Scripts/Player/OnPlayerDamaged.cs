using DG.Tweening;
using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class OnPlayerDamaged : MonoBehaviour
{
    public float effectDuration = 0.5f;

    [SerializeField, HideInInspector]
    private PlayerController controller;

    [SerializeField, HideInInspector]
    private SpriteRenderer spriteRenderer;

    [SerializeField, HideInInspector]
    private Color defaultColor;

    private void OnValidate()
    {
        controller = GetComponent<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultColor = spriteRenderer.color;
    }
    private void Start()
    {
        controller.Stats.OnDamaged += Stats_OnDamaged;
    }

    private void Stats_OnDamaged()
    {
        spriteRenderer.DOBlendableColor(Color.red, effectDuration).SetEase(Ease.OutQuad).SetLoops(2, LoopType.Yoyo);
    }
}
