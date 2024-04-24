using UnityEngine.UI;
using UnityEngine;
using System;


public class Cell : MonoBehaviour
{
    [SerializeField] private Image icon;
    private Item item;
    public Action OnUpdateCell;

    private void Awake()
    {
        icon.sprite = null;
    }

    public void Init(Item item)
    {
        this.item = item;
        if (item == null)
            icon.sprite = null;
        else
            icon.sprite = item.Icon;
    }

    public void OnClickCell()
    {
        if (item == null)
            return;
        GameManager.Instance.inventory.Items.Remove(item);
        Buff buff = new Buff
        {
            type = item.Type,
            additiveBonus = item.Value
        };
        GameManager.Instance.inventory.buffReciever.AddBuff(buff);
        if (OnUpdateCell != null)
            OnUpdateCell();
    }
}