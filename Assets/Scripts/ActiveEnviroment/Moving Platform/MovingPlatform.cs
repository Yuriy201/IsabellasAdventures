using CustomAttributes;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody2D rigidbody;
    private BoxCollider2D collider;

    [SerializeField]
    private float _moveSpeed = 2f;

    public Vector2[] _movePoints;

    private int nextPointIndex;

    private Vector2 platformPositionLastFrame = Vector2.zero;

    [ReadOnlyProperty]
    private HashSet<Rigidbody2D> rigidbodiesOnPlatform = new();

    //[ReadOnlyProperty]
    //private HashSet<Rigidbody2D> nonPlayableRigidbodiesOnPlatform = new();

    private void OnValidate()
    {
        collider = GetComponent<BoxCollider2D>();
        rigidbody = GetComponent<Rigidbody2D>();

        nextPointIndex = 1;   
        
        if (_movePoints.Length > 0)
        {
            _movePoints[0] = transform.position;
        }
    }

    private void Start()
    {
        _movePoints[0] = transform.position;

        platformPositionLastFrame = rigidbody.position;

        //rigidbody.DOPath(_movePoints.Select(x => (Vector2)x.position).ToArray(), _moveSpeed * 3).SetLoops(-1, LoopType.Yoyo);
    }

    private void GetNextPoint()
    {
        nextPointIndex++;

        if (nextPointIndex >= _movePoints.Length)
        {
            nextPointIndex = 0;
        }
    }

    private void FixedUpdate()
    {    
        if (Vector2.Distance(rigidbody.position, _movePoints[nextPointIndex]) < 0.2f)
        {
            GetNextPoint();
            rigidbody.velocity = Vector2.zero;
        }

        //rigidbody.MovePosition(Vector2.MoveTowards(rigidbody.position, (Vector2)_movePoints[nextPointIndex].position, _moveSpeed));
        rigidbody.velocity = ((Vector2)_movePoints[nextPointIndex] - rigidbody.position).normalized * _moveSpeed;

        foreach (var rb in rigidbodiesOnPlatform)
        {          
            //rb.position += new Vector2((rigidbody.position - platformPositionLastFrame).x, 0);
            rb.MovePosition(rb.position + (rb.velocity * Time.fixedDeltaTime) + new Vector2((rigidbody.position - platformPositionLastFrame).x, 0));
        }

        /*
        foreach (var rb in nonPlayableRigidbodiesOnPlatform)
        {
            rb.velocity = Vector2.zero;
            rb.position += new Vector2((rigidbody.position - platformPositionLastFrame).x, 0);
            //rb.MovePosition(rb.position + (rb.velocity * Time.fixedDeltaTime) + new Vector2((rigidbody.position - platformPositionLastFrame).x, 0));
        }
        */

        platformPositionLastFrame = rigidbody.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.attachedRigidbody == null && collision.attachedRigidbody.isKinematic)
            return;

        /* only box colliders stay on the moving platforms by default
        if (collision.TryGetComponent<BoxCollider2D>(out _))
            return;
        */

        if (collision.gameObject.CompareTag("Player"))
        {
            rigidbodiesOnPlatform.Add(collision.attachedRigidbody);           
        }
        /*
        else
        {
            nonPlayableRigidbodiesOnPlatform.Add(collision.attachedRigidbody);
        }
        */
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.attachedRigidbody != null)
        {
            if (rigidbodiesOnPlatform.Contains(collision.attachedRigidbody))
            {
                rigidbodiesOnPlatform.Remove(collision.attachedRigidbody);
              
                //collision.attachedRigidbody.velocity += new Vector2(rigidbody.velocity.x * 5, 0);
            }

            /*
            else if (nonPlayableRigidbodiesOnPlatform.Contains(collision.attachedRigidbody))
            {
                nonPlayableRigidbodiesOnPlatform.Remove(collision.attachedRigidbody);
            }
            */
        }
    }

    private void OnDrawGizmos()
    {
        Vector2 prevPoint = transform.position;

        foreach (var point in _movePoints)
        {
            Gizmos.DrawLine(point, prevPoint);
            Gizmos.DrawCube(point, collider.size * rigidbody.transform.lossyScale);
            prevPoint = point;
        }

        Gizmos.DrawLine(prevPoint, transform.position);
    }
}

public enum MovingPlatformEaseType
{
    EachPoint,
    WholePath
}
