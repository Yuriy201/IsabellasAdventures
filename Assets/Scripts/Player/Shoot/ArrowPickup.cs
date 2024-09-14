using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPickup : MonoBehaviour
{
    [SerializeField] private int arrowsToGive = 1;
    [SerializeField] private float pickupDelay = 0.2f;

    [SerializeField]
    private Collider2D arrowCollider;

    private void OnValidate()
    {
        arrowCollider = GetComponent<Collider2D>();
    }

    private void Start()
    {     
        //arrowCollider.enabled = false;
        //StartCoroutine(EnablePickupAfterDelay());
    }

    private IEnumerator EnablePickupAfterDelay()
    {
        yield return new WaitForSeconds(pickupDelay); 
        arrowCollider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!arrowCollider.enabled) return;

        ArrowManager arrowManager = collision.GetComponent<ArrowManager>();
        if (arrowManager != null)
        {
            arrowManager.PickupArrow(arrowsToGive);
            ObjectPool.Instance.ReternObject(transform.parent.gameObject);
        }
    }
}
