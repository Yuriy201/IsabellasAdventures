using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct SlotInventory
{
    public ScriptableItem DataItem;
    public int Count;
    public int Position;
}

public class Inventory : MonoBehaviour , IInteraction
{
    public bool IsOpen;
    public int MaxSlot;

    public delegate void Event_OnAddItem(int NewIndex);
    public delegate void Event_OnRemoveItem(int Index);
    public delegate void Event_OnChange(int Index,SlotInventory NewSlot);

    public Event_OnAddItem OnAddItem;
    public Event_OnRemoveItem OnRemoveItem;
    public Event_OnChange OnChange;

    public List<SlotInventory> Slots;

    public List<SlotInventory> GetSlots()
    {
        return Slots;
    }

    public int FindPosition()
    {
        int Current = 0;
        do
        {
            bool IsFind = false;
            for (int i = 0; i < Slots.Count; i++)
            {
                if (Slots[i].Position == Current)
                {
                    Current++;
                    IsFind = true;
                    break;
                }
            }

            if(!IsFind)
                return Current;

        } while (Current <= MaxSlot);

        return -1;
    }

    bool SetItemPositoin(int Index, int NewPosition)
    {
        if (MaxSlot < NewPosition) return false;

        for (int i = 0; i < Slots.Count; i++)
        {
            if (Slots[i].Position == NewPosition)
                return false;
        }

        var NewSlot = Slots[Index];
        NewSlot.Position = NewPosition;
        Slots[Index] = NewSlot;
        OnChange?.Invoke(Index, NewSlot);
        return true;
    }

    public bool AddItem(Item item)
    {
        var Data = item.DataItem;

        if (Data.MaxStack != 1)
        {
            for (int i = 0; i < Slots.Count; i++)
            {
                if (Slots[i].DataItem == Data && Slots[i].Count < Slots[i].DataItem.MaxStack)
                {
                    SlotInventory L_NewSlot = Slots[i];
                    
                    L_NewSlot.Count += 1;
                    Slots[i] = L_NewSlot;
                    OnChange?.Invoke(i, L_NewSlot);
                    return true;
                }
            }
        }   
        
        int NewPosition = FindPosition();

        if (NewPosition == -1)
            return false;

        SlotInventory NewSlot;
        NewSlot.DataItem = item.DataItem;
        NewSlot.Count = 1;
        NewSlot.Position = FindPosition();
        Slots.Add(NewSlot);
        
        OnAddItem?.Invoke(Slots.Count -1);

        return true;
    }
    
    public bool AddItem(ScriptableItem Data, int Position)
    {
        for (int i = 0; i < Slots.Count; i++)
        {
            if (Slots[i].Position == Position)
            {
                return false;
            }
        }

        SlotInventory NewSlot;
        NewSlot.DataItem = Data;
        NewSlot.Count = 1;
        NewSlot.Position = Position;
        Slots.Add(NewSlot);
        
        OnAddItem?.Invoke(Slots.Count -1);

        return true;
    }

    public bool RemoveItem(int Index, int count = 1)
    {
        if (Index <= 0 && Index > Slots.Count)
            return false;

        if (Slots[Index].DataItem.MaxStack == 1) {
               
            OnRemoveItem?.Invoke(Index);
            Slots.RemoveAt(Index);
            return true;
        }

        if (Slots[Index].Count - count >= 0)
        {
            if (Slots[Index].Count - count == 0){

                Slots.RemoveAt(Index);
                OnRemoveItem?.Invoke(Index);
            }
            else
            {
                SlotInventory L_NewSlot = Slots[Index];

                L_NewSlot.Count -= count;
                Slots[Index] = L_NewSlot;
                OnChange?.Invoke(Index, L_NewSlot);
            }
            return true;
        }

        return false;
    }

    public bool DragDropItem(Inventory ToInventory, int Index, int ToIndex, int Position)
    {
        if (ToInventory == null || ToInventory == this)
        {
            if(ToIndex == -1)
            {
                if (Slots[Index].DataItem.MaxStack != 1 && Slots[Index].Count != 1)
                {
                    if (AddItem(Slots[Index].DataItem, Position))
                    {
                        RemoveItem(Index);
                        return true;
                    }
                }

                return SetItemPositoin(Index, Position);
            }
            else
            {
                SlotInventory L_Slot = Slots[ToIndex];
                if(L_Slot.DataItem == Slots[Index].DataItem && L_Slot.Count != L_Slot.DataItem.MaxStack)
                {
                    L_Slot.Count++;
                    Slots[ToIndex] = L_Slot;

                    OnChange?.Invoke(ToIndex, L_Slot);
                    RemoveItem(Index);

                    return true;
                }
            }
        }
        else{
            if (ToIndex == -1)
            {
                if (ToInventory.AddItem(Slots[Index].DataItem, Position))
                {
                    RemoveItem(Index);
                    return true;
                }
            }
            else
            {
                SlotInventory L_Slot = ToInventory.Slots[ToIndex];
                if (L_Slot.DataItem == Slots[Index].DataItem && L_Slot.Count != L_Slot.DataItem.MaxStack)
                {
                    L_Slot.Count++;
                    ToInventory.Slots[ToIndex] = L_Slot;

                    ToInventory.OnChange?.Invoke(ToIndex, L_Slot);
                    RemoveItem(Index);

                    return true;
                }
            }
        }
            return false;
    }

    public void Interaction(GameObject Other)
    {
        if (!IsOpen) return;
        //if (Other.TryGetComponent(out MoveCharacter L_Target))
        //{
        //    L_Target.Controller.OpenBox(this);
        //}
    }
}
