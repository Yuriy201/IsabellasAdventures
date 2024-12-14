using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    public Transform InventoryKeyPanel, InventoryPotionPanel, InventoryConsumablesPanel, minislotopen;
    public List<InventorySlot> KeySlots = new List<InventorySlot>();
    public List<InventorySlot> PotionSlots = new List<InventorySlot>();
    public List<InventorySlot> ConsumablesSlots = new List<InventorySlot>();
    [SerializeField] GameObject pan, menu, key, potion, cons, minislotclosed;
    public bool isOpen = false;
    [SerializeField] private GameObject _minislotpanel;

    
    void Start()
    {
        for (int i = 0; i < InventoryKeyPanel.childCount; i++)
        {
            if (InventoryKeyPanel.GetChild(i).GetComponent<InventorySlot>() != null)
            {
                KeySlots.Add(InventoryKeyPanel.GetChild(i).GetComponent<InventorySlot>());
            }
        }
        for (int i = 0; i < InventoryPotionPanel.childCount; i++)
        {
            if (InventoryPotionPanel.GetChild(i).GetComponent<InventorySlot>() != null)
            {
                PotionSlots.Add(InventoryPotionPanel.GetChild(i).GetComponent<InventorySlot>());
            }
        }
        for (int i = 0; i < InventoryConsumablesPanel.childCount; i++)
        {
            if (InventoryConsumablesPanel.GetChild(i).GetComponent<InventorySlot>() != null)
            {
                ConsumablesSlots.Add(InventoryConsumablesPanel.GetChild(i).GetComponent<InventorySlot>());
            }
        }
    }

    public void TogglePan(bool isOpen)
    {
        pan.SetActive(isOpen);
        menu.SetActive(!isOpen);
        Time.timeScale = isOpen ? 0f : 1f;
    }
    void Update()
    {
        if (menu.activeSelf)
        {
            pan.SetActive(false);
        }

       
        if (key.activeSelf)
        {
            potion.SetActive(false);
            cons.SetActive(false);
        }
        else if (potion.activeSelf)
        {
            key.SetActive(false);
            cons.SetActive(false);
        }
        else if (cons.activeSelf)
        {
            key.SetActive(false);
            potion.SetActive(false);
        }

        
        _minislotpanel.transform.position = pan.activeSelf ? minislotopen.position : minislotclosed.transform.position;
        minislotclosed.SetActive(!pan.activeSelf);
    }


    public void AddItemKey(ItemScriptableObject _item)
    {
        foreach (InventorySlot slot in KeySlots)
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
    public void AddItemPotion(ItemScriptableObject _item)
    {
        foreach (InventorySlot slot in PotionSlots)
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
    public void AddItemConsumables(ItemScriptableObject _item)
    {
        foreach (InventorySlot slot in ConsumablesSlots)
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
        foreach (InventorySlot slot in KeySlots)
        {
            if (slot.isEmpty == false && slot.item == _item)
            {
                slot.item = null;
                slot.isEmpty = true;
                slot.DeleteIcon();
                return;
            }
        }


        foreach (InventorySlot slot in PotionSlots)
        {
            if (slot.isEmpty == false && slot.item == _item)
            {
                slot.item = null;
                slot.isEmpty = true;
                slot.DeleteIcon();
                return;
            }
        }

        foreach (InventorySlot slot in ConsumablesSlots)
        {
            if (slot.isEmpty == false && slot.item == _item)
            {
                slot.item = null;
                slot.isEmpty = true;
                slot.DeleteIcon();
                return;
            }
        }

    }

}
