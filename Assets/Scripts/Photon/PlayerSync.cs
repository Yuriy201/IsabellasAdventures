using Photon.Pun;
using UnityEngine;

[DefaultExecutionOrder(-50)]
[RequireComponent(typeof(PhotonView))]
public class PlayerSync : MonoBehaviour
{
    [SerializeField] private MonoBehaviour[] _localScripts;

    private PhotonView _view;
    private void Awake()
    {
        _view = GetComponent<PhotonView>();

        if (_view.IsMine == false)
        {
            DisableScripts();
        }
    }
    private void DisableScripts()
    {
        for (int i = 0; i < _localScripts.Length; i++)
        {
            _localScripts[i].enabled = false;
        }
    }
}
