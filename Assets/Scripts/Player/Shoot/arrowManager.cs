using UnityEngine;

public class ArrowManager : MonoBehaviour
{
    [SerializeField] private int maxArrows = 6;
    [SerializeField] private int currentArrows;

    private void Start()
    {
        currentArrows = maxArrows;
    }

    public bool HasArrows()
    {
        return currentArrows > 0;
    }

    public void UseArrow()
    {
        if (currentArrows > 0)
        {
            currentArrows--;
            Debug.Log("Arrow used. Remaining arrows: " + currentArrows);
        }
    }

    public void PickupArrow(int amount)
    {
        currentArrows = Mathf.Min(currentArrows + amount, maxArrows);
        Debug.Log("Arrow picked up. Current arrows: " + currentArrows);
    }

    public int GetArrowCount()
    {
        return currentArrows;
    }
}
