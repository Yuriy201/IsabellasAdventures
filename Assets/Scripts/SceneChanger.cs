using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public static SceneChanger Instance;

    [SerializeField] private GameConfig _gameConfig;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            return;
        }

        Destroy(gameObject);
    }
    public void LoadScene(string sceneName)
    {
        if (_gameConfig.IsMultiplayer)
        {
            PhotonNetwork.LoadLevel(sceneName);
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
