using DG.Tweening;
using InputSystem;
using Player;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

[RequireComponent(typeof(PlayerController))]
public class PlayerDeath : MonoBehaviour
{
    [Header("Death Canvas")]
    [SerializeField]
    private Canvas deathCanvas;
    private CanvasGroup deathCanvasGroup;

    [Space(2)]
    [SerializeField]
    private float _deathDelay = 2f;

    private PlayerController playerController;  
    private PlayerStats stats;

    private void OnDisable() => stats.OnDied -= Death;

    private void OnValidate()
    {
        playerController = GetComponent<PlayerController>();
        deathCanvasGroup = deathCanvas.GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        stats = playerController.Stats;
        stats.OnDied += Death;

        deathCanvasGroup.alpha = 0f;
        deathCanvas.gameObject.SetActive(false);
    }

    private void Death()
    {
        playerController.enabled = false;
      
        deathCanvas.gameObject.SetActive(true);

        deathCanvasGroup.DOFade(1f, _deathDelay).OnComplete(() => 
        {
            //deathCanvasGroup.DOFade(0f, _deathDelay * 0.4f).OnComplete(() =>
            SceneManager.LoadSceneAsync("Titles");
        });
    }

    private void ShowTitle()
    {
        SceneManager.LoadSceneAsync("Titles");
    }
}
