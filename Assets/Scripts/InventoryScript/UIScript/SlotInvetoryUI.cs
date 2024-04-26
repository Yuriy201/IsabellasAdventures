using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotInvetoryUI : MonoBehaviour , IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    public int Position;
    public Text TextCount;
    public Image image;

    public Canvas mainCanvas;

    RectTransform _RectTransform;
    private int Index = -1;
    private Inventory Inventory = null;

    Transform CurrentParent;
    CanvasGroup Group;

    void Start()
    {
        _RectTransform = GetComponent<RectTransform>();
        Group = GetComponent<CanvasGroup>();
        CurrentParent = _RectTransform.parent;
    }

    public int GetIndex()
    {
        return Index;
    }

    ScriptableItem GetData()
    {
        return Inventory.Slots[Index].DataItem;
    }

    public void Init(Inventory NewInventory, int NewIndex)
    {
        Inventory = NewInventory;
        Index = NewIndex;

        if (NewIndex != -1)
            UpSlot(CurrentParent);
    }

    public void SetIndex(int NewIndex, Transform NewParent)
    {
        Index = NewIndex;
        UpSlot(NewParent);
    }

    public void UpSlot(Transform NewParent)
    {
        var L_Slot = Inventory.Slots[Index];
        image.sprite = L_Slot.DataItem.Image;

        if (CurrentParent != NewParent)
        {
            CurrentParent = NewParent;
            transform.SetParent(CurrentParent);
            _RectTransform.anchoredPosition = Vector2.zero;
        }

        if (L_Slot.DataItem.MaxStack == 1)
            TextCount.gameObject.SetActive(false);
        else
            TextCount.text = L_Slot.Count.ToString();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Index == -1) return;

        transform.SetParent(mainCanvas.transform);
        Group.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Index == -1) return;

        _RectTransform.anchoredPosition += eventData.delta / mainCanvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Index == -1) return;

        transform.SetParent(CurrentParent);
        _RectTransform.anchoredPosition = Vector2.zero;
        Group.blocksRaycasts = true;
    }

    public void OnDrop(PointerEventData eventData)
    {
        var SlotTran = eventData.pointerDrag.transform.GetComponent<SlotInvetoryUI>();
     
        if (Index == -1)
        {
            SlotTran.Inventory.DragDropItem(Inventory, SlotTran.Index, -1, Position);
        }
        else
        {
            SlotTran.Inventory.DragDropItem(Inventory, SlotTran.Index, Index, Position);
        }
    }
}
