using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Transform InventoryPanel;
    public List<InventorySlot> slots = new List<InventorySlot>();
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; 1 < InventoryPanel.childCount; i++)
        {
            if(InventoryPanel.GetChild(0).GetComponent<InventorySlot>() != null)
            {
                slots.Add(InventoryPanel.GetChild(0).GetComponent<InventorySlot>());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
