using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelManager : MonoBehaviour
{
    int levelUnlock;
    public Button[] buttons;
    public Button level1;
    public Button level2; 
    public Button level3; 
    public Button level4; 
    public Button level5; 
    public Button level6;
    void Start()
    {
        levelUnlock = PlayerPrefs.GetInt("levels", 1);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
        for (int i = 0; i < levelUnlock; i++)
        {
            buttons[i].interactable = true;
        }
    }
    public void loadLevel(int levelindex)
    {
        SceneManager.LoadScene(levelindex);
    }
    public void Reset()
    {
        level2.interactable = false;
        level3.interactable = false;
        level4.interactable = false;
        level5.interactable = false;
        level6.interactable = false;
        PlayerPrefs.DeleteAll();
    }
}
