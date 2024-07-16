using Photon.Pun;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_InputField))]
public class NickNameInput : MonoBehaviour
{
    private TMP_InputField _nickNameInput;

    private void Awake()
    {
        _nickNameInput = GetComponent<TMP_InputField>();
    }
    private void Start()
    {
        _nickNameInput.text = PhotonNetwork.NickName;
    }
}
