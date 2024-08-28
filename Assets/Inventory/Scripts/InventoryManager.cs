using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Transform InventoryPanel;
    public List<InventorySlot> slots = new List<InventorySlot>();
    [SerializeField] GameObject pan, menu;
    public bool isOpen = false;

    
    void Start()
    {
        for (int i = 0; 1 < InventoryPanel.childCount; i++)
        {
            if (InventoryPanel.GetChild(i).GetComponent<InventorySlot>() != null)
            {
                slots.Add(InventoryPanel.GetChild(i).GetComponent<InventorySlot>());
            }
        }
        
    }

    public void closepan()
    {
        pan.SetActive(false);
        menu.SetActive(true);
        
      
    }
    public void openPan()
    {
        pan.SetActive(true);
        menu.SetActive(false);
    }
    void Update()
    {
        if (menu.activeSelf == true)
        {
            pan.SetActive(false);

        }
    }

    public void AddItem(ItemScriptableObject _item)
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot.isEmpty == true)
            {
                slot.item = _item;
                slot.isEmpty = false;
                slot.SetIcon(_item.icon);
                break;
            }
        }
    }

    public void LoseItem(ItemScriptableObject _item)
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot.isEmpty == false)
            {
                slot.item = null;
                slot.isEmpty = true;
                slot.DeleteIcon();
                break;
            }
        }
    }
}
