using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PlayerScripts;

public class LevelWindow : MonoBehaviour
{
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private Image experienceBarImage;
    private LevelSystem levelSystem;

    private void Start()
    {
        levelText.text = "LEVEL " +(levelSystem.GetLevelNumber());
    }

    public void AddExp(int count)
    {
        levelSystem.AddExperience(count);
    }
    
    private void SetExperienceBarSize(float experienceNormalized)

    {
        experienceBarImage.fillAmount = experienceNormalized;
    }
    private void SetLevelNumber(int levelNumber)
    {
        levelText.text = "LEVEL \n" +( levelNumber + 1);
    
    }

    public void SetLevelSystem(LevelSystem levelSystem)
    {
        this.levelSystem = levelSystem;
        SetLevelNumber(levelSystem.GetLevelNumber());
        SetExperienceBarSize(levelSystem.GetExperienceNormalized());

        levelSystem.OnExperienceChanged += LevelSystem_OnExperienceChanged;
        levelSystem.OnLevelChanged += LevelSystem_OnLevelChanged;
    }

    private void LevelSystem_OnLevelChanged(object sender, System.EventArgs e)
    {
        SetLevelNumber(levelSystem.GetLevelNumber());
    }

    private void LevelSystem_OnExperienceChanged(object sender,System.EventArgs e)
    {
        SetExperienceBarSize(levelSystem.GetExperienceNormalized());

    }
}
