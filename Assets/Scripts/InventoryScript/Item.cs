using UnityEngine;

public class Item : MonoBehaviour, IInteraction
{
    // Start is called before the first frame update
    public ScriptableItem DataItem;

    public void Interaction(GameObject Other)
    {
        if (Other.TryGetComponent(out CharacterPlayer L_Target))
        {
            if (L_Target.Controller.GetComponent<Inventory>().AddItem(this))
                Destroy(gameObject);
        }
    }
}
