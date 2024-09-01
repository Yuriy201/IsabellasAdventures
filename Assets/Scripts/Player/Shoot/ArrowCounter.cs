using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowManager : MonoBehaviour
{
    [SerializeField] private int maxArrows = 5;
    [SerializeField] private int currentArrows;

    private void Start()
    {
        currentArrows = maxArrows; // Initialize the player with the maximum number of arrows
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
           
        }
    }

    public void PickupArrow(int amount)
    {
        currentArrows = Mathf.Min(currentArrows + amount, maxArrows);

    }

    public int GetArrowCount()
    {
        return currentArrows;
    }
}
