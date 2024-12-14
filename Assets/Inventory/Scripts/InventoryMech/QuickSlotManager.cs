using System.Collections.Generic;
using UnityEngine;


public class QuickSlotManager : MonoBehaviour
{
    public List<InventorySlot> quickSlots; 
    public bool isPotionInQuickSlot = false; 

    void Update()
    {
        CheckPotionInQuickSlot();
    }

    void CheckPotionInQuickSlot()
    {
        isPotionInQuickSlot = false; 

        foreach (var slot in quickSlots)
        {
            if (slot.item != null)
            {
                if (slot.item.itemType == ItemType.Potion)
                {
                    isPotionInQuickSlot = true; 
                    break; 
                }
            }
        }
    }

    public void losePoison(ItemScriptableObject _item)
    {
        foreach (InventorySlot slot in quickSlots)
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
