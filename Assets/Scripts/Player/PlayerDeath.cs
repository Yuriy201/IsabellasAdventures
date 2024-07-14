using DG.Tweening;
using NeoxiderUi;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerController))]
public class PlayerDeath : MonoBehaviour
{
    [Header("Death Canvas")]
    private Page _gameOverPage;
    private CanvasGroup _deathCanvasGroup;

    [Space(2)]
    [SerializeField]
    private float _deathDelay = 2f;

    private PlayerController playerController;
    private PlayerStats stats;

    private void OnDisable()
    {
        stats.OnDied -= Death;
    }
    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void Start()
    {
        stats = playerController.Stats;
        stats.OnDied += Death;

        _gameOverPage = PagesManager.instance.FindPage(PageType.Lose);
        _deathCanvasGroup = _gameOverPage.GetComponent<CanvasGroup>();

        _deathCanvasGroup.alpha = 0f;
        _gameOverPage.gameObject.SetActive(false);
    }
    public void SetUp(Page gameOverPage)
    {
        _gameOverPage = gameOverPage;
    }
    private void Death()
    {
        playerController.enabled = false;

        _gameOverPage.gameObject.SetActive(true);

        _deathCanvasGroup.DOFade(1f, _deathDelay).OnComplete(() =>
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
