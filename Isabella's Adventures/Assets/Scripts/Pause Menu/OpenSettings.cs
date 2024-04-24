using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenSettings : MonoBehaviour
{
    [SerializeField] private Transform _panelSettings;
    
    private bool isEnabled = false;

    public void ButtonClick()
    {
        isEnabled = isEnabled == false;

        _panelSettings.gameObject.SetActive(isEnabled);
    }
}
