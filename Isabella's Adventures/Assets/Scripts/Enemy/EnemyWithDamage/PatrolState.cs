using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    [SerializeField] private Transform _way;
    [SerializeField] private float _speed;

    private Transform[] _point;
    private int _curentPoint;
    private int _move = 1;

    private void Start()
    {
        _point = new Transform[_way.childCount];

        for (int i = 0; i < _way.childCount; i++)
        {
            _point[i] = _way.GetChild(i);
        }

    }

    private void Update()
    {
        Mover();
    }

    private void Mover()
    {
        Transform target = _point[_curentPoint];
        transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);

        if (transform.position == target.position)
        {
            _curentPoint++;
            Reflect(-_move);

            if (_curentPoint >= _point.Length)
            {
                _curentPoint = 0;
                Reflect(_move);
            }
        }
    }

    private void Reflect(int move)
    {
        transform.localScale = new Vector3(move, 1, 1);
    }
}
