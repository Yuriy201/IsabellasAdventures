using UnityEngine;
using TMPro;

public class PlayerStats: MonoBehaviour
{
    [SerializeField] internal int _playerHp;
    [SerializeField] internal int _coin;

    #region PlayerUI
    [SerializeField] internal TMP_Text _coinText;
    #endregion
}
