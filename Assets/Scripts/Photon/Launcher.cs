using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField] private string _gameVersion = "1.0.0";
    [SerializeField] private string _nextSceneName;

    private void Start()
    {
        Connect();
    }
    private void Connect()
    {
        if (PhotonNetwork.IsConnected)
        {
            LoadNextScene();
        }
        else
        {

            PhotonNetwork.ConnectUsingSettings();
            Debug.Log("Connecting...");

            PhotonNetwork.GameVersion = _gameVersion;
        }
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        Connect();
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected");

        LoadNextScene();
    }
    private void LoadNextScene()
    {
        SceneManager.LoadScene(_nextSceneName);
    }
}
