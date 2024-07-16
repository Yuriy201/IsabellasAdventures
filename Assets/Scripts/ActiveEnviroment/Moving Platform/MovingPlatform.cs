using CustomAttributes;
using DG.Tweening;
using Photon.Pun.Demo.SlotRacer.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class MovingPlatform : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody2D rigidbody;

    private BoxCollider2D collider;

    [SerializeField]
    private float _moveSpeed = 2f;

    [SerializeField]
    private float _nextPointDelay = 0.5f;
    private float nextPointDelayTimer;

    public Vector2[] _movePoints = new Vector2[0];

    private int nextPointIndex;

    private Vector2 lastPlatformPoint;
    private Vector2 platformPositionLastFrame = Vector2.zero;

    [SerializeField]
    private AnimationCurve _speedEasing;

    public List<Rigidbody2D> rigidbodiesOnPlatform = new();

    private void OnValidate()
    {
        collider = GetComponent<BoxCollider2D>();
        rigidbody = GetComponent<Rigidbody2D>();

        rigidbody.isKinematic = true;
        rigidbody.freezeRotation = true;

        nextPointIndex = 1;   
        
        if (_movePoints.Length > 0)
        {
            _movePoints[0] = transform.position;
        }
        else
        {
            _movePoints = new Vector2[1];
            _movePoints[0] = transform.position;
        }
    }

    private void Start()
    {
        nextPointDelayTimer = _nextPointDelay;
        _movePoints[0] = transform.position;

        lastPlatformPoint = _movePoints[0];

        platformPositionLastFrame = rigidbody.position;
    }

    private void GetNextPoint()
    {
        nextPointIndex++;

        if (nextPointIndex >= _movePoints.Length)
        {
            nextPointIndex = 0;
        }
    }


    private float GetPercentageAlong(Vector2 a, Vector2 b, Vector2 c)
    {
        var ab = b - a;
        var ac = c - a;
        return Vector2.Dot(ac, ab) / ab.sqrMagnitude;
    }

    private void FixedUpdate()
    {    
        if (Vector2.Distance(rigidbody.position, _movePoints[nextPointIndex]) < 0.2f)
        {
            nextPointDelayTimer -= Time.deltaTime;

            if (nextPointDelayTimer <= 0)
            {
                nextPointDelayTimer = _nextPointDelay;

                lastPlatformPoint = _movePoints[nextPointIndex];

                GetNextPoint();
                rigidbody.velocity = Vector2.zero;
            }
        }

        float t = GetPercentageAlong(lastPlatformPoint, _movePoints[nextPointIndex], rigidbody.position);

        rigidbody.velocity = (_movePoints[nextPointIndex] - rigidbody.position).normalized * _moveSpeed * _speedEasing.Evaluate(t);

        foreach (var rb in rigidbodiesOnPlatform)
        {          
            rb.MovePosition(rb.position + (rb.velocity * Time.fixedDeltaTime) + new Vector2((rigidbody.position - platformPositionLastFrame).x, 0));
        }

        platformPositionLastFrame = rigidbody.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.attachedRigidbody == null && collision.attachedRigidbody.isKinematic)
            return;

        if (collision.gameObject.CompareTag("Player"))
        {
            rigidbodiesOnPlatform.Add(collision.attachedRigidbody);           
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.attachedRigidbody != null)
        {
            if (rigidbodiesOnPlatform.Contains(collision.attachedRigidbody))
            {
                rigidbodiesOnPlatform.Remove(collision.attachedRigidbody);            
            }
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
