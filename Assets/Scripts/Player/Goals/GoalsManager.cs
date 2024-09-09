using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance;
    public GameObject achievementPrefab;
    public float displayDuration = 3f;
    public float fadeDuration = 1f; 

    private bool level2AchievementUnlocked = false;
    private bool playTimeAchievementUnlocked = false;
    private bool wolfKillAchievementUnlocked = false;
    private int wolfKillCount = 0;
    private const int wolfKillThreshold = 30;
    private float playTimeCounter = 0f;
    private const float playTimeThreshold = 180f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        LoadAchievements();
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Update()
    {
        if (!playTimeAchievementUnlocked)
        {
            playTimeCounter += Time.deltaTime;
            if (playTimeCounter >= playTimeThreshold)
            {
                UnlockPlayTimeAchievement();
            }
        }
    }
    public void RegisterWolfKill()
    {
        if (!wolfKillAchievementUnlocked)
        {
            wolfKillCount++;
            if (wolfKillCount >= wolfKillThreshold)
            {
                UnlockWolfKillAchievement();
            }
        }
    }
    private void UnlockWolfKillAchievement()
    {
        wolfKillAchievementUnlocked = true;
        ShowAchievement("Убить 30 волков");
        SaveAchievements();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!level2AchievementUnlocked && scene.name == "Level2")
        {
            UnlockLevel2Achievement();
        }
    }

    private void UnlockLevel2Achievement()
    {
        level2AchievementUnlocked = true;
        ShowAchievement("Начать игру");
        SaveAchievements();
    }

    private void UnlockPlayTimeAchievement()
    {
        playTimeAchievementUnlocked = true;
        ShowAchievement("Играть в игру 3 минуты");
        SaveAchievements();
    }

    private void ShowAchievement(string message)
    {
        GameObject achievement = Instantiate(achievementPrefab);
        TextMeshProUGUI text = achievement.GetComponentInChildren<TextMeshProUGUI>();
        if (text != null)
        {
            text.text = message;
        }

        CanvasGroup canvasGroup = achievement.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = achievement.AddComponent<CanvasGroup>();
        }

        StartCoroutine(FadeAchievement(canvasGroup));
    }

    private IEnumerator FadeAchievement(CanvasGroup canvasGroup)
    {
        float timer = 0f;

  
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            yield return null;
        }

       
        yield return new WaitForSeconds(displayDuration);

        timer = 0f;

      
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            yield return null;
        }

        Destroy(canvasGroup.gameObject);
    }

    private void SaveAchievements()
    {
        PlayerPrefs.SetInt("Level2Achievement", level2AchievementUnlocked ? 1 : 0);
        PlayerPrefs.SetInt("PlayTimeAchievement", playTimeAchievementUnlocked ? 1 : 0);
        PlayerPrefs.SetInt("WolfKillAchievement", wolfKillAchievementUnlocked ? 1 : 0);
        PlayerPrefs.SetInt("WolfKillCount", wolfKillCount);
        PlayerPrefs.Save();
    }

    private void LoadAchievements()
    {
        level2AchievementUnlocked = PlayerPrefs.GetInt("Level2Achievement", 0) == 1;
        playTimeAchievementUnlocked = PlayerPrefs.GetInt("PlayTimeAchievement", 0) == 1;
        wolfKillAchievementUnlocked = PlayerPrefs.GetInt("WolfKillAchievement", 0) == 1;
        wolfKillCount = PlayerPrefs.GetInt("WolfKillCount", 0);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
