using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class GetPhotonView : MonoBehaviour
{
    protected PhotonView _view;
    protected virtual void Awake()
    {
        _view = GetComponent<PhotonView>();
    }
}
