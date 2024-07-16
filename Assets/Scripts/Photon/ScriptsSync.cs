using Photon.Pun;
using UnityEngine;

[DefaultExecutionOrder(-50)]
public class ScriptsSync : GetPhotonView
{
    [SerializeField] private MonoBehaviour[] _localScripts;

    protected override void Awake()
    {
        base.Awake();

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
