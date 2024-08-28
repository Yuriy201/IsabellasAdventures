using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Key", menuName = "Inventory/Item/Key")]
public class KeyItem : ItemScriptableObject
{
    public void Start()
    {
        itemType = ItemType.Key;
    }
}
