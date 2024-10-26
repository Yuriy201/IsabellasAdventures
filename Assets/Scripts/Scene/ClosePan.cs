using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosePan : MonoBehaviour
{
    public GameObject _PanHelp;

    private void Start()
    {
        Time.timeScale = 0f;
    }
    public void Close()
    {
        _PanHelp.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
