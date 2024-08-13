using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private float _speed;
    [SerializeField] private float _stayDelay;

    private int _currentIndexPoint;

    private void Start() => 
        Move();

    private void Move()
    {
        transform
            .DOMove(_points[_currentIndexPoint].position, _speed)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                ChangeIndexPoint();
                StartCoroutine(StayDelay(Move));
            });
    }

    private void ChangeIndexPoint()
    {
        _currentIndexPoint++;
        if (_currentIndexPoint > _points.Length - 1)
            _currentIndexPoint = 0;
    }

    private IEnumerator StayDelay(Action callback)
    {
        yield return new WaitForSeconds(_stayDelay);
        callback.Invoke();
    }
}
