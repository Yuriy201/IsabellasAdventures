using UnityEngine;

public class LossPlayerTransition : Transition
{
    public LayerMask playerLayer;
    public float attackDistance;

    private float distance;

    public void Update()
    {
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, transform.right, attackDistance, playerLayer);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, -transform.right, attackDistance, playerLayer);

        Debug.DrawRay(transform.position, transform.right * attackDistance, Color.red);
        Debug.DrawRay(transform.position, -transform.right * attackDistance, Color.red);

        distance = Vector2.Distance(transform.position, Target.transform.position);

        if (hitRight.collider != null || hitLeft.collider != null)
        {
            if (distance > attackDistance)
            {
                NeedTransit = true;
            }
        }
    }
}
