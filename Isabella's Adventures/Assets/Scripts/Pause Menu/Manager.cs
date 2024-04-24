using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    // Single instance
    public static Manager Instance
    {


        get;
        protected set;
    }
    public bool m_IsPaused;

    private void Start()
    {


        Instance = this;
        m_IsPaused = false;
    }

    private void Update()
    {


        if (Input.GetButtonDown("Cancel"))
        {


            PauseMenu.Instance.SwitchDisplay();
        }
    }
    // public void DisplayCursor(bool display)
    // {



    // }
}

