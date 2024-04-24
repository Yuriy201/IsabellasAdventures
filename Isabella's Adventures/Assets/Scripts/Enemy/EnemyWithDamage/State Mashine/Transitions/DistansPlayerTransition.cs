using UnityEngine;

public class DistansPlayerTransition : Transition
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            NeedTransit = true;
        }
    }
}
