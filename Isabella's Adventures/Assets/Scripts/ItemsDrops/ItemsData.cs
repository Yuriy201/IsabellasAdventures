using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemsDrops;


[CreateAssetMenu(fileName = "Item", menuName = "StaticData/Item")]
public class ItemsData : ScriptableObject
{
    public ItemsTypeID TypeID;
    public int Value;
    public GameObject Prefab;
}
