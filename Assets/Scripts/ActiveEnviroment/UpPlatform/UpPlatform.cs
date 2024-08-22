using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpPlatform : MonoBehaviour
{
    [SerializeField] private float speed = 2f; 
    [SerializeField] private float distance = 5f;

    private Vector3 _startPosition;

    private void Start()
    {
       
        _startPosition = transform.position;
    }

    private void Update()
    {
       
        float newY = Mathf.PingPong(Time.time * speed, distance);
        transform.position = _startPosition + new Vector3(0f, newY, 0f);
    }
}
