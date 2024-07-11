using UnityEngine;

public class Pause : MonoBehaviour
{
    private void OnEnable()
    {
        SetPause(true);
    }

    private void OnDisable()
    {
        SetPause(false);
    }

    private static void SetPause(bool pause)
    {
        if (pause)
        {
            Debug.Log("pause");
            Time.timeScale = 0.0f;
        }
        else
        {

            Debug.Log("UnPause");
            Time.timeScale = 1.0f;
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        SetPause(!focus);
    }
}
