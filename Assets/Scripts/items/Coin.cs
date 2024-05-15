using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private Transform _topPoint;
    [SerializeField] private Transform _bottomPoint;
    [SerializeField] private float _animTime;
    
    private void Start()
    {
        StartCoroutine(Animation());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator Animation()
    {
        while (true)
        {
            float time = 0;
            
            while (time < _animTime)
            {
                transform.position = Vector3.Lerp(_topPoint.position, _bottomPoint.position, time / _animTime);
                time += Time.deltaTime;
                yield return null;
            }
            
            time = 0;
            
            while (time < _animTime)
            {
                transform.position = Vector3.Lerp(_bottomPoint.position, _topPoint.position, time / _animTime);
                time += Time.deltaTime;
                yield return null;
            }
            
            transform.position = _topPoint.position;
            yield return null;
        }
    }
}