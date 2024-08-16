using CustomAttributes;
using DG.Tweening;
using Photon.Pun.Demo.SlotRacer.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class MovingPlatformWITHPLAYER : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody2D rigidbody;

    private BoxCollider2D collider;

    [SerializeField]
    private float _moveSpeed = 2f;

    [Tooltip("Дистанция на которой платформа 'остановится' и плавно перейдет в точную позицию точки (чем выше скорость, тем выше число желательно)")]
    [SerializeField]
    private float stopDistance = 0.05f;

    [SerializeField]
    private bool snapToPoint = true;

    [SerializeField]
    private float _nextPointDelay = 0.5f;
    private float nextPointDelayTimer;

    public PointWithCurve[] _movePoints = new PointWithCurve[0];

    private Vector2 linearPathDirection = Vector2.zero;

    private int nextPointIndex;

    private Vector2 lastPlatformPoint;
    private Vector2 platformPositionLastFrame = Vector2.zero;

    [SerializeField]
    private AnimationCurve _speedEasing;

    private HashSet<Rigidbody2D> rigidbodiesOnPlatform = new();

    [Space(6)]
    [Header("Editor only")]
    public int pathCirclesCount = 10;
    public float pathCirclesRadius = 0.5f;

    [Space(2)]
    public float curvePointRadius = 1f;

    [System.Serializable]
    public struct PointWithCurve
    {
        public Vector2 point;
        public Vector2 curve;
        public bool isLinear;
    }

    private void OnValidate()
    {
        collider = GetComponent<BoxCollider2D>();
        rigidbody = GetComponent<Rigidbody2D>();

        rigidbody.isKinematic = true;
        rigidbody.freezeRotation = true;
        rigidbody.interpolation = RigidbodyInterpolation2D.Interpolate;
        rigidbody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        if (_movePoints.Length > 0)
        {
            _movePoints[0].point = transform.position;
        }
        else
        {
            _movePoints = new PointWithCurve[1];
            _movePoints[0].point = transform.position;
        }
    }

    private void Start()
    {
        nextPointIndex = 0;

        _movePoints[0].point = transform.position;

        lastPlatformPoint = _movePoints[0].point;

        platformPositionLastFrame = rigidbody.position;

        nextPointDelayTimer = 0;

        GetNextPoint();
        rigidbody.velocity = Vector2.zero;

        if (_movePoints[nextPointIndex].isLinear)
        {
            linearPathDirection = (_movePoints[nextPointIndex].point - rigidbody.position).normalized;
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = _movePoints[nextPointIndex].point - rigidbody.position;

        if (direction.sqrMagnitude <= stopDistance)
        {
            nextPointDelayTimer += Time.deltaTime;

            rigidbody.velocity = Vector2.zero;

            if (snapToPoint)
                rigidbody.MovePosition(Vector2.Lerp(rigidbody.position, _movePoints[nextPointIndex].point, nextPointDelayTimer / _nextPointDelay));

            if (nextPointDelayTimer >= _nextPointDelay)
            {
                nextPointDelayTimer = 0;

                lastPlatformPoint = _movePoints[nextPointIndex].point;

                GetNextPoint();

                if (_movePoints[nextPointIndex].isLinear)
                {
                    linearPathDirection = (_movePoints[nextPointIndex].point - rigidbody.position).normalized;
                }
            }

            foreach (var rb in rigidbodiesOnPlatform)
            {
                rb.MovePosition(rb.position + (rb.velocity * Time.fixedDeltaTime) + new Vector2((rigidbody.position - platformPositionLastFrame).x, 0));
            }

            platformPositionLastFrame = rigidbody.position;

            return;
        }

        float t = GetPercentageAlong(lastPlatformPoint, _movePoints[nextPointIndex].point, rigidbody.position);

        if (!_movePoints[nextPointIndex].isLinear)
        {
            rigidbody.velocity = (GetPositionOnCurve(rigidbody.position, _movePoints[nextPointIndex].curve, _movePoints[nextPointIndex].point, t + 0.05f) - rigidbody.position).normalized * _moveSpeed * _speedEasing.Evaluate(t);
        }
        else
        {
            rigidbody.velocity = linearPathDirection * _moveSpeed * _speedEasing.Evaluate(t);
        }

        //Debug.DrawLine(rigidbody.position, rigidbody.position + (GetPositionOnCurve(rigidbody.position, _movePoints[nextPointIndex].curve, _movePoints[nextPointIndex].point, t + 0.05f) - rigidbody.position).normalized * 5, Color.red);

        foreach (var rb in rigidbodiesOnPlatform)
        {
            rb.MovePosition(rb.position + (rb.velocity * Time.fixedDeltaTime) + new Vector2((rigidbody.position - platformPositionLastFrame).x, 0));
        }

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

    private Vector2 GetPositionOnCurve(Vector2 a, Vector2 b, Vector2 c, float t)
    {
        Vector2 ab = Vector2.Lerp(a, b, t);
        Vector2 bc = Vector2.Lerp(b, c, t);

        return Vector2.Lerp(ab, bc, t);
    }


    private float GetPercentageAlong(Vector2 a, Vector2 b, Vector2 c)
    {
        var ab = b - a;
        var ac = c - a;
        return Vector2.Dot(ac, ab) / ab.sqrMagnitude;
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
        Gizmos.color = Color.gray;

        Vector2 prevPoint = transform.position;

        foreach (var point in _movePoints)
        {
            Gizmos.DrawLine(point.point, prevPoint);
            Gizmos.DrawCube(point.point, collider.size * rigidbody.transform.lossyScale);
            prevPoint = point.point;
        }

        Gizmos.DrawLine(prevPoint, transform.position);

        if (pathCirclesCount > 0)
        {
            Gizmos.color = Color.green;

            Vector3 boxSize = new Vector3(curvePointRadius, curvePointRadius, curvePointRadius);

            float pcc = (float)pathCirclesCount;

            for (int i = 1; i < _movePoints.Length; i++)
            {
                if (!_movePoints[i].isLinear)
                {
                    Gizmos.DrawCube(_movePoints[i].curve, boxSize);
                    for (int j = 0; j <= pathCirclesCount; j++)
                    {
                        Gizmos.DrawSphere(GetPositionOnCurve(_movePoints[i - 1].point, _movePoints[i].curve, _movePoints[i].point, j / pcc), pathCirclesRadius);
                    }
                }
            }

            if (!_movePoints[0].isLinear)
            {
                Gizmos.DrawCube(_movePoints[0].curve, boxSize);
                for (int j = 0; j <= pathCirclesCount; j++)
                {
                    Gizmos.DrawSphere(GetPositionOnCurve(_movePoints[_movePoints.Length - 1].point, _movePoints[0].curve, _movePoints[0].point, j / pcc), pathCirclesRadius);
                }
            }
        }

        // draw a cube on next point (kinda useless)
        /*
        Gizmos.color = Color.red;
        Gizmos.DrawCube(_movePoints[nextPointIndex].point, new Vector3(3, 3, 3));
        Gizmos.DrawSphere(GetPositionOnCurve(rigidbody.position, _movePoints[nextPointIndex].curve, _movePoints[nextPointIndex].point, tt + 0.001f), 2);
        */
    }
}
