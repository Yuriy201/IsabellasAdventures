using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Consumables", menuName = "Inventory/Item/Consumables")]
public class ConsumablesItem : ItemScriptableObject
{
    void Start()
    {
        itemType = ItemType.Consumables;
    }
}
