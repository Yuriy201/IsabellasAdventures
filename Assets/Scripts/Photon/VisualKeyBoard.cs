using UnityEngine;

public class VisualKeyBoard : MonoBehaviour
{
    private TouchScreenKeyboard _keyboard;

    public void OpenKeyboard()
    {
        _keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
    }
}
