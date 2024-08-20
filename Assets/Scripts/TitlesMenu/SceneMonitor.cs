using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SceneMonitor : MonoBehaviour
{
    public static SceneMonitor Instance { get; private set; }
    private List<string> _sceneHistory = new List<string>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string sceneName = scene.name;
        if (_sceneHistory.Count == 0 || _sceneHistory[_sceneHistory.Count - 1] != sceneName)
        {
            _sceneHistory.Add(sceneName);
        }
    }

    public List<string> GetSceneHistory()
    {
        return new List<string>(_sceneHistory);
    }
}
