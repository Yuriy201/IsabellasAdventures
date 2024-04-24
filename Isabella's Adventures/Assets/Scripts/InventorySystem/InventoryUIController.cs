using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    [SerializeField] Cell[] cells;
    [SerializeField] private int cellCount;
    [SerializeField] private Cell cellPrefab;
    [SerializeField] private Transform rootParent;

    void Init()
    {
        cells = new Cell[cellCount];
        for (int i = 0; i < cellCount; i++)
        {
            cells[i] = Instantiate(cellPrefab, rootParent);
            cells[i].OnUpdateCell += UpdateInventory;
        }

        cellPrefab.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if (cells == null || cells.Length <= 0)
            Init();
        UpdateInventory();
    }

    public void UpdateInventory()
    {
        var inventory = GameManager.Instance.inventory;
        foreach (var cell in cells)
            cell.Init(null);
        for (int i = 0; i < inventory.Items.Count; i++)
        {
            if (i < cells.Length)
                cells[i].Init(inventory.Items[i]);
        }
    }
}