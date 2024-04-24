using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class NextLevel : MonoBehaviour
{
    int currentLevel;
    int levelUnlock;
    void Start()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        levelUnlock = PlayerPrefs.GetInt("levels");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            UnlockLevel();
            Next();
        }
    }
    public void UnlockLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;

        if (currentLevel >= PlayerPrefs.GetInt("levels"))
        {
            PlayerPrefs.SetInt("levels", currentLevel + 1);
        }
    }
    void Next()
    {
        SceneManager.LoadScene(currentLevel + 1);
    }
}
