using ItemsDrops;
using UnityEngine;

public class Items : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;

    private ItemsTypeID _typeID;
    private int _count;
    
    public int Count
    {
        get => _count;
        set => _count = value;
    }

    public void GetItemId(ItemsTypeID typeID)
    {
        _typeID = typeID;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Bag bag))
        {
            switch (_typeID)
            {
                case ItemsTypeID.Money:
                    bag.AddMoney(_count);
                    Destroy(gameObject);
                    break;
                case ItemsTypeID.Arrow:
                    bag.AddItem(_count);
                    Destroy(gameObject);
                    break;
                case ItemsTypeID.Exp:
                    bag.Exp(_count);
                    Destroy(gameObject);
                    break;
            }
        }
    }
}