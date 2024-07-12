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
            Debug.Log("<color=red>Pause</color>");
            Time.timeScale = 0.0f;
        }
        else
        {

            Debug.Log("<color=red>UnPause</color>");
            Time.timeScale = 1.0f;
        }
    }
}
