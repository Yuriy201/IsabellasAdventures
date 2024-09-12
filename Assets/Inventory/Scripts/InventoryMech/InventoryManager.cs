using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    public Transform InventoryKeyPanel, InventoryPotionPanel, InventoryConsumablesPanel, minislotopen, minislotclosed;
    public List<InventorySlot> KeySlots = new List<InventorySlot>();
    public List<InventorySlot> PotionSlots = new List<InventorySlot>();
    public List<InventorySlot> ConsumablesSlots = new List<InventorySlot>();
    [SerializeField] GameObject pan, menu, key, potion, cons;
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

    public void closepan()
    {
        pan.SetActive(false);
        menu.SetActive(true);
        Time.timeScale = 1f;
    }
    public void openPan()
    {
        pan.SetActive(true);
        menu.SetActive(false);
        Time.timeScale = 0f;
    }
    void Update()
    {
        if (menu.activeSelf == true)
            pan.SetActive(false);
        if (key.activeSelf)
        {
            potion.SetActive(false);
            cons.SetActive(false);
        }
        if (pan.activeSelf)
            _minislotpanel.transform.position = minislotopen.position;
        else
            _minislotpanel.transform.position = minislotclosed.position;

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
