using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Potion", menuName = "Inventory/Item/Potion")]
public class PotionItem : ItemScriptableObject
{ 
    void Start()
    {
        itemType = ItemType.Potion;
    }
}
