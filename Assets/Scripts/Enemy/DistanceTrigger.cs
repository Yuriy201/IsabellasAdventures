using CustomAttributes;
using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DistanceTrigger : MonoBehaviour
{
    [ReadOnlyProperty]
    public PlayerController ClosestPlayer = null;

    private Collider2D closestPlayerCollider = null;

    [ReadOnlyProperty]
    public float CurrentDistance;

    public event Action<PlayerController> OnPlayerChanged;

    private Collider2D collider;

    private void OnValidate()
    {
        collider = GetComponent<Collider2D>();
        collider.isTrigger = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        float distance = (collision.transform.position - transform.position).sqrMagnitude;      

        if (ClosestPlayer != null)
        {
            CurrentDistance = (ClosestPlayer.transform.position - transform.position).sqrMagnitude;

            if (distance > CurrentDistance)
            {
                ClosestPlayer = collision.GetComponent<PlayerController>();
                closestPlayerCollider = collision;
                CurrentDistance = distance;

                OnPlayerChanged.Invoke(ClosestPlayer);
            }
        }
        else
        {
            ClosestPlayer = collision.GetComponent<PlayerController>();
            closestPlayerCollider = collision;
            CurrentDistance = distance;

            OnPlayerChanged.Invoke(ClosestPlayer);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == closestPlayerCollider)
        {
            ClosestPlayer = null;
            closestPlayerCollider = null;
            CurrentDistance = 0f;
        }
    }
}
