using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileControls : MonoBehaviour
{
    public GameObject mobileButtons;
    public GameObject mobileJoystick;

    public GameObject jumpButton;
    public GameObject sprintButton;
    public GameObject shootButton;

    public void SwitchControls(ControlType controlType)
    {
        switch (controlType)
        {
            case ControlType.Joystick:
                mobileButtons.SetActive(false);
                mobileJoystick.SetActive(true);
                break;
            case ControlType.Buttons:
                mobileButtons.SetActive(true);
                mobileJoystick.SetActive(false);
                break;
        }
    }

    public void MobileControlsVisibility(bool visibility)
    {
        mobileButtons.SetActive(visibility);
        mobileJoystick.SetActive(visibility);

        jumpButton.SetActive(visibility);
        sprintButton.SetActive(visibility);
        shootButton.SetActive(visibility);
    }
}
