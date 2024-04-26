using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Inventory Inventory;
    public GameObject Slot;
    public GameObject SlotItem;
    public GameObject SlotPanel;

    [SerializeField] List<SlotInvetoryUI> NoneSlots;
    [SerializeField] List<SlotInvetoryUI> ItemsSlots;

    public Canvas mainCanvas;

    private void OnEnable()
    {
        if (Inventory == null) return;
        SetInventory(Inventory);
    }

    private void OnRemoveSlot(int Index)
    {
        int LastIndex = ItemsSlots.Count - 1;
        Destroy(ItemsSlots[LastIndex].gameObject);
        ItemsSlots.RemoveAt(LastIndex);

        for (int i = 0; i < Inventory.Slots.Count; i++)
        {
            ItemsSlots[i].SetIndex(i,NoneSlots[Inventory.Slots[i].Position].transform);
        }
    }

    private void OnChange(int Index, SlotInventory NewSlot)
    {
        for (int i = 0; i < ItemsSlots.Count; i++)
        {
            if (Index == ItemsSlots[i].GetIndex())
            {
                ItemsSlots[i].UpSlot(NoneSlots[Inventory.Slots[Index].Position].transform);
                return;
            }
        }
    }

    private void OnAddSlot(int Index)
    {
        var L_NewSlot = Instantiate(SlotItem, NoneSlots[Inventory.Slots[Index].Position].transform).GetComponent<SlotInvetoryUI>();
        L_NewSlot.mainCanvas = mainCanvas;
        L_NewSlot.Init(Inventory, Index);
        ItemsSlots.Add(L_NewSlot);
    }

    public void SetInventory(Inventory NewInventory)
    {
        if(Inventory != null)
        {
            for (int i = 0; i < NoneSlots.Count; i++)
            {
                Destroy(NoneSlots[i].gameObject);
            }

            for (int i = 0; i < ItemsSlots.Count; i++)
            {
                Destroy(ItemsSlots[i].gameObject);
            }

            ItemsSlots.Clear();
            NoneSlots.Clear();
        }
        Inventory = NewInventory;

        if (NewInventory == null) return;

        Inventory.OnAddItem += OnAddSlot;
        Inventory.OnRemoveItem += OnRemoveSlot;
        Inventory.OnChange += OnChange;

        for (int i = 0; i < Inventory.MaxSlot; i++)
        {
            var L_NewSlot = Instantiate(Slot, SlotPanel.transform).GetComponent<SlotInvetoryUI>();
            L_NewSlot.Position = i;
            L_NewSlot.Init(Inventory, -1);
            NoneSlots.Add(L_NewSlot);
        }

        for (int i = 0; i < Inventory.Slots.Count; i++)
        {
            OnAddSlot(i);
        }

    }

    private void OnDisable()
    {
        Inventory.OnAddItem -= OnAddSlot;
        Inventory.OnRemoveItem -= OnRemoveSlot;
        Inventory.OnChange -= OnChange;

        for (int i = 0; i < NoneSlots.Count; i++)
        {
            Destroy(NoneSlots[i].gameObject);
        }

        for (int i = 0; i < ItemsSlots.Count; i++)
        {
            Destroy(ItemsSlots[i].gameObject);
        }

        ItemsSlots.Clear();
        NoneSlots.Clear();
    }
}
