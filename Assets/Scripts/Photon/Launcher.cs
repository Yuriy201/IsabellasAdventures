using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

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

        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;

        PhotonNetwork.JoinOrCreateRoom("test", roomOptions, TypedLobby.Default);
    }
    public override void OnJoinedRoom()
    {
        LoadNextScene();
    }
    private void LoadNextScene()
    {
        PhotonNetwork.LoadLevel(_nextSceneName);
    }
}
