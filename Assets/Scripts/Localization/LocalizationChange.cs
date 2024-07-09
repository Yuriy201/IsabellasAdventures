using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocalizationChange : MonoBehaviour
{
    public string[] languagesIndexes;
    public static SystemLanguage language = SystemLanguage.English;

    [SerializeField]
    private int indexLanguage = 0;

    private IEnumerator Start()
    {
        yield return LocalizationSettings.InitializationOperation;

        if (PlayerPrefs.HasKey("Localization"))
        {
            indexLanguage = PlayerPrefs.GetInt("Localization", 0);
        }
        else
        {
            indexLanguage = GetSystemLanguage();
        }

        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[indexLanguage];
    }

    private int GetSystemLanguage()
    {
        for (int i = 0; i < languagesIndexes.Length; i++)
        {
            string[] parts = languagesIndexes[i].Split(' ');

            if (parts.Length > 0 && parts[0] == Application.systemLanguage.ToString())
            {
                return i;
            }
        }

        return 0;
    }

    public void SetLanguage(int index)
    {
        indexLanguage = index;
        PlayerPrefs.SetInt("Localization", indexLanguage);
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[indexLanguage];
    }

    private void OnValidate()
    {
        var locales = LocalizationSettings.AvailableLocales.Locales;
        languagesIndexes = new string[locales.Count];

        for (int i = 0; i < locales.Count; i++)
        {
            var locale = locales[i];
            languagesIndexes[i] = locale.LocaleName;
        }
    }
}
