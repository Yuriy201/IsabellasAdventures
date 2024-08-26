using UnityEngine;

public class FpsChanher : MonoBehaviour
{
    [SerializeField] private int _fps = 60;

    private void Awake()
    {
        Application.targetFrameRate = _fps;
    }
}
