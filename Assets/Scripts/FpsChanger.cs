using UnityEngine;

public class FpsChanger : MonoBehaviour
{
    [SerializeField] private int _fps = 60;

    private void Awake()
    {
        Application.targetFrameRate = _fps;
    }
}
