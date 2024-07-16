using TMPro;
using System;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class NickNameUI : MonoBehaviour
{
    private TMP_Text _nickName;

    private void Awake()
    {
        _nickName = GetComponent<TMP_Text>();
    }
    public void SetNickName(string newName)
    {
        if(string.IsNullOrEmpty(newName))
            throw new ArgumentNullException(nameof(newName));

        _nickName.text = newName;
    }
    private void Update()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
}
