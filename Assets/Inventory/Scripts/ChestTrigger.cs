//using UnityEngine.UI;
using UnityEngine;


public class ChestTrigger : MonoBehaviour
{
    public GameObject openChestBtn;
    public ItemScriptableObject item;
    public InventoryManager InventoryManager;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            openChestBtn.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            openChestBtn.SetActive(false);
    }
    public void OpenChest()
    {
        InventoryManager.LoseItem(item);
        Destroy(this.gameObject);
    }
}
