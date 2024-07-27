using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class PhotonLobby : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        JoinLobby();
    }
    private void JoinLobby()
    {
        print("JoiningLobby");
        PhotonNetwork.JoinLobby();
    }
    public void JoinOrCreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 10;
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;

        PhotonNetwork.JoinOrCreateRoom("Test", roomOptions, TypedLobby.Default);
    }
    public override void OnJoinedRoom()
    {
        SceneChanger.Instance.LoadScene("Level2");
    }
}
