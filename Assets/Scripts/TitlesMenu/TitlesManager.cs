using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitlesManager : MonoBehaviour
{
    [SerializeField]
    private Button _menuButton;

    [SerializeField]
    private Button _restartButton;

    private SceneMonitor sceneMonitor;

    private void Start()
    {
        _menuButton.onClick.AddListener(() => SceneManager.LoadSceneAsync("MainMenu"));
        _restartButton.onClick.AddListener(LoadPreviousScene);

        sceneMonitor = SceneMonitor.Instance;
    }

    private void LoadPreviousScene()
    {
        if (sceneMonitor != null)
        {
            var sceneHistory = sceneMonitor.GetSceneHistory();
            if (sceneHistory.Count > 1)
            {
                string previousSceneName = sceneHistory[sceneHistory.Count - 2];
                SceneManager.LoadSceneAsync(previousSceneName);
            }
            else
            {
                Debug.Log("Нет предыдущей сцены для загрузки.");
            }
        }
    }
}
