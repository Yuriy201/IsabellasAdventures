using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] private GameObject _settings;

    private bool _isOpenSettings = false;
    
    public void Play()
    {
        //сделать норм загрузку
        SceneManager.LoadScene(1);
    }

    public void Levels()
    {
        print("Hui");
    }
    
    public void Settings()
    {
        _isOpenSettings = !_isOpenSettings;
        _settings.SetActive(_isOpenSettings);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
