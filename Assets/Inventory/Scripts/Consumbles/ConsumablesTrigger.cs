using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumablesTrigger : MonoBehaviour
{
    public ItemScriptableObject item;
    public InventoryManager InventoryManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            InventoryManager.AddItemConsumables(item);
            Destroy(this.gameObject);
        }

    }
}
