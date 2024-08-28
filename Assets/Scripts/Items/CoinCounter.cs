using UnityEngine;
using TMPro;

public class CoinCounter : MonoBehaviour
{
    public static CoinCounter Instance { get; private set; }

    public int _coinCount;
    [SerializeField] private TextMeshProUGUI _coinText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        _coinCount = PlayerPrefs.GetInt("Coins");
    }

    private void Start()
    {
        UpdateCoinText();
    }

    public void AddCoin()
    {
        _coinCount++;
        PlayerPrefs.SetInt("Coins", _coinCount);
        UpdateCoinText();
    }

    public int GetCoinCount()
    {
        _coinCount = PlayerPrefs.GetInt("Coins");
        return _coinCount;
    }

    private void UpdateCoinText()
    {
        if (_coinText != null)
        {
            _coinText.text = "Coins: " + _coinCount;
        }
    }
}
