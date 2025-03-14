﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class DragAndDropItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public InventorySlot oldSlot;
    private RectTransform rectTransform;
    private Image itemImage;
    private void Start()
    {
        oldSlot = transform.GetComponentInParent<InventorySlot>();
        rectTransform = GetComponent<RectTransform>();
        itemImage = GetComponentInChildren<Image>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (oldSlot.isEmpty)
            return;

        // Обновляем позицию объекта с учетом абсолютных координат
        rectTransform.position = eventData.position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (oldSlot.isEmpty)
            return;


        itemImage.color = new Color(1, 1, 1, 0.75f);
        itemImage.raycastTarget = false;
        transform.SetParent(transform.parent.parent);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (oldSlot.isEmpty)
            return;


        itemImage.color = new Color(1, 1, 1, 1f);

        // Разрешаем мыши взаимодействовать с объектом снова
        itemImage.raycastTarget = true;

        // Возвращаем объект в родительский слот
        transform.SetParent(oldSlot.transform);
        transform.position = oldSlot.transform.position;  // Ставим на место

        if (eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.GetComponent<InventorySlot>() != null)
        {
            // Перемещаем данные из одного слота в другой
            ExchangeSlotData(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.GetComponent<InventorySlot>());
        }
    }

    void ExchangeSlotData(InventorySlot newSlot)
    {
        // Временно храним данные newSlot в отдельных переменных
        ItemScriptableObject item = newSlot.item;
        bool isEmpty = newSlot.isEmpty;
        GameObject iconGO = newSlot.IconGO;

        // Заменяем значения newSlot на значения oldSlot
        newSlot.item = oldSlot.item;
        if (oldSlot.isEmpty == false)
        {
            newSlot.SetIcon(oldSlot.IconGO.GetComponent<Image>().sprite);
        }
        else
        {
            newSlot.IconGO.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            newSlot.IconGO.GetComponent<Image>().sprite = null;
        }

        newSlot.isEmpty = oldSlot.isEmpty;

        // Заменяем значения oldSlot на значения newSlot сохраненные в переменных
        oldSlot.item = item;
        if (isEmpty == false)
        {
            oldSlot.SetIcon(iconGO.GetComponent<Image>().sprite);
        }
        else
        {
            oldSlot.IconGO.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            oldSlot.IconGO.GetComponent<Image>().sprite = null;
        }

        oldSlot.isEmpty = isEmpty;
    }
}