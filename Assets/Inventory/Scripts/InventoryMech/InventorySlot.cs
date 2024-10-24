using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public ItemScriptableObject item;
    public bool isEmpty = true;
    public GameObject IconGO;
    private void Awake()
    {
        IconGO = transform.GetChild(0).GetChild(0).gameObject;
    }
    public void SetIcon(Sprite icon)
    {
        IconGO.GetComponent<Image>().color = new Color(1,1,1,1);
        IconGO.GetComponent<Image>().sprite = icon;
    }
    public void DeleteIcon()
    {
        IconGO.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        IconGO.GetComponent<Image>().sprite = null;
    }
    
}
