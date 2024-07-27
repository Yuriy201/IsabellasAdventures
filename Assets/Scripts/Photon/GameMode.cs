using UnityEngine;
using Zenject;

public class GameMode : MonoBehaviour
{
    [Inject] private GameConfig _gameConfig;

    [SerializeField] private bool _multiplayer = true;

    private void Awake()
    {
        SetGameMode(_multiplayer);
    }
    public void SetGameMode(bool isMultiplayer)
    {
        _gameConfig.IsMultiplayer = isMultiplayer;
    }
}
