using Photon.Pun;
using UnityEngine;
using System;

public class NickName : MonoBehaviour
{
    private readonly string NICKNAME = "NickName";

    private void Awake()
    {
        if (PlayerPrefs.HasKey(NICKNAME))
        {
            print("Initi");
            SetNickName(PlayerPrefs.GetString(NICKNAME));
        }
        else
        {
            print("Generate");
            GenerateNickName();
        }
    }
    private void GenerateNickName()
    {
        SetNickName("Player " + UnityEngine.Random.Range(0, 999));
    }
    public void SetNickName(string newName)
    {
        PhotonNetwork.NickName = newName;
        SaveNickName();
    }
    private void SaveNickName()
    {
        PlayerPrefs.SetString(NICKNAME, PhotonNetwork.NickName);
        PlayerPrefs.Save();
    }
}
