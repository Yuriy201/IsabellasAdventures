using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance
    {

        get; protected set;
    }
    public GameObject optionMenu;
    public GameObject restartMenu;
    public GameObject quitGameMenu;
    public GameObject pauseMenu;


    public void Start()
    {


        Instance = this;
        gameObject.SetActive(false);
    }

    public void SwitchDisplay()
    {


        bool isPaused = Manager.Instance.m_IsPaused;
        Manager.Instance.m_IsPaused = !isPaused;
        gameObject.SetActive(!isPaused);
        Time.timeScale = Manager.Instance.m_IsPaused ? 0 : 1;
    }

    // UI button
    public void ShowPauseMenu()
    {


        pauseMenu.SetActive(true);
    }

    public void HidePauseMenu()
    {


        pauseMenu.SetActive(false);
    }

    public void ShowOptionMenu()
    {


        optionMenu.SetActive(true);
        HidePauseMenu();
    }
    public void OptionMenuBack()
    {


        optionMenu.SetActive(false);
        ShowPauseMenu();
    }
    public void ShowRestartMenu()
    {


        restartMenu.SetActive(true);
        HidePauseMenu();
    }
    public void RestartMenuBack()
    {


        restartMenu.SetActive(false);
        ShowPauseMenu();
    }
    public void ShowQuitGameMenu()
    {


        quitGameMenu.SetActive(true);
        HidePauseMenu();
    }
    public void QuitGameMenuBack()
    {


        quitGameMenu.SetActive(false);
        ShowPauseMenu();
    }

    // Function button
    public void RestartGame()
    {


        Time.timeScale = 1;
        SceneManager.LoadScene("test");
    }

    public void QuitGame()
    {


#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }


}
