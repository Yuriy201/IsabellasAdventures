using CustomAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ArrowRotation : MonoBehaviour
{
    private Rigidbody2D rigidbody;

    [ReadOnlyProperty]
    public bool canRotate = true;

    public bool lerp = false;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        canRotate = true;
    }

    private void Update()
    {
        if (canRotate && rigidbody.velocity != Vector2.zero)
        {
            float t = 1;

            if (lerp)
            {
                t = Time.deltaTime * 10;
            }

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.x, transform.rotation.y, Mathf.Atan2(rigidbody.velocity.y, rigidbody.velocity.x) * Mathf.Rad2Deg), t);
        }          
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    canRotate = false;
    //}

    //private void OnDisable()
    //{
    //    canRotate = false;
    //}
}
