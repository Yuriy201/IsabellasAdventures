using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPickup : MonoBehaviour
{
    [SerializeField] private int arrowsToGive = 5;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ArrowManager arrowManager = collision.GetComponent<ArrowManager>();
        if (arrowManager != null)
        {
            arrowManager.PickupArrow(arrowsToGive);
            Destroy(gameObject); // Destroy the pickup after use
        }
    }
}
