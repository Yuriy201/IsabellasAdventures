using UnityEngine;
using UnityEngine.Events;

public class Bag : MonoBehaviour
{
    [SerializeField] private LevelWindow _levelWindow;
    public int MoneyCount { get; set; }
    private static Bag Instance { get; set; }

    public event UnityAction<int> MoneyChanged;
    public event UnityAction<int> ExpChanged; 
    private void Start()
    {
        Instance = this;
    }

    public void AddMoney(int count)
    {
        MoneyCount += count;
        MoneyChanged?.Invoke(count);
    }

    public void AddItem(int count)
    {
        
    }

    public void Exp(int count)
    {
        _levelWindow.AddExp(count);
        //ExpChanged?.Invoke(count);
    }
}
