using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Transform InventoryPanel;
    public List<InventorySlot> slots = new List<InventorySlot>();
    [SerializeField] GameObject pan, menu;
   
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; 1 < InventoryPanel.childCount; i++)
        {
            if(InventoryPanel.GetChild(i).GetComponent<InventorySlot>() != null)
            {
                slots.Add(InventoryPanel.GetChild(i).GetComponent<InventorySlot>());
            }
        }
    }

    public void closepan()
    {
        pan.SetActive(false);
        menu.SetActive(true);
    }
    public void openPan()
    {
        pan.SetActive(true);
        menu.SetActive(false);
    }
    void Update()
    {

        
    }
}
