using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class NickNameSync : GetPhotonView
{
    [SerializeField] private NickNameUI _nickNameUI;

    private void Start()
    {
        _view.RPC(nameof(SetName), RpcTarget.AllBuffered, PhotonNetwork.NickName);

        if (_view.IsMine)
            Destroy(_nickNameUI.gameObject);
    }
    [PunRPC]
    private void SetName(string name)
    {
        _nickNameUI.SetNickName(name);
    }
}
