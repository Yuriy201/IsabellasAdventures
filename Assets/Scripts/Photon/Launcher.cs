using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField] private Button playButton;
    [SerializeField] private string _gameVersion = "1.0.0";
    [SerializeField] private string _nextSceneName;

    private void Start()
    {
        playButton.interactable = false;
        playButton.onClick.AddListener(LoadNextScene);
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
        playButton.interactable = true;
        
    }
    private void LoadNextScene()
    {
        PhotonNetwork.LoadLevel(_nextSceneName);
    }
}
