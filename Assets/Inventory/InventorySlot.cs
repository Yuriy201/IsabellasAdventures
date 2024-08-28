using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public ItemScriptableObject item;
    public bool isEmpty = true;
    public GameObject IconGO;
    private void Awake()
    {
        IconGO = transform.GetChild(0).gameObject;
    }
    public void SetIcon(Sprite icon)
    {
        IconGO.GetComponent<Image>().color = new Color(1,1,1,1);
        IconGO.GetComponent<Image>().sprite = icon;
    }
}
