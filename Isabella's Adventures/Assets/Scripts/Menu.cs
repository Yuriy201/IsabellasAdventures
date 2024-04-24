using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Button _back;

    public void LoadCharacterChoiceScreen()
    {
        //characterScreen.SetActive(true);
        SceneManager.LoadScene(1);
    }

    public void LoadCharacterScreen()
    {
    }

    public void LoadTo(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void Back()
    {
        _back.interactable = true;
    }

    public void OnClickPlay()
    {
        SceneManager.LoadScene(1);
    }

    public void OnClickExit()
    {
        Application.Quit();
    }
}