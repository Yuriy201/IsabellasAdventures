using UnityEngine;

public class minigame : MonoBehaviour
{
    private bool isGame = false;
    private GameObject _menuOption;
    private Transform _basepos, _gamepos;

    private void Start()
    {
        _basepos.position = new Vector3(547, -93, 0);
        _gamepos.position = new Vector3(1363, -93, 0);
    }
    public void startGM()
    {
        isGame = true;
        
    }
}
