
using UnityEngine;

public class ChoosePanel : MonoBehaviour
{
    [SerializeField] GameObject keypanel, potionpanel, consumablespanel;
    public void OpenKey()
    {
        keypanel.SetActive(true);
        potionpanel.SetActive(false);
        consumablespanel.SetActive(false);
    }
    public void OpenPotion()
    {
        keypanel.SetActive(false);
        potionpanel.SetActive(true);
        consumablespanel.SetActive(false);
    }
    public void OpenConsumable()
    {
        keypanel.SetActive(false);
        potionpanel.SetActive(false);
        consumablespanel.SetActive(true);
    }
}
