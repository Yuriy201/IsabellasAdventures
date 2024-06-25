using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitlesManager : MonoBehaviour
{
    [SerializeField]
    private Button _menuButton;

    [SerializeField]
    private Button _restartButton;

    private void Start()
    {
        _menuButton.onClick.AddListener(() => SceneManager.LoadSceneAsync("MainMenu"));
        _restartButton.onClick.AddListener(() => SceneManager.LoadSceneAsync("SampleScene"));
    }
}
