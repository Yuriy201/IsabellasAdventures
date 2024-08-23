using System.Collections;
using UnityEngine;

public class UpPlatform : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float distance = 5f;
    [SerializeField] private float delayBeforeDescending = 60f;

    private Vector3 _startPosition;
    private bool _isTriggered = false;
    private Coroutine _moveCoroutine;

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_isTriggered)
        {
            _isTriggered = true;
            _moveCoroutine = StartCoroutine(MovePlatform());
        }
    }

    private IEnumerator MovePlatform()
    {
        float elapsedTime = 0f;

        while (elapsedTime < distance / speed)
        {
            transform.position = Vector3.Lerp(_startPosition, _startPosition + new Vector3(0f, distance, 0f), elapsedTime / (distance / speed));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = _startPosition + new Vector3(0f, distance, 0f);

        yield return new WaitForSeconds(delayBeforeDescending);

        elapsedTime = 0f;

        while (elapsedTime < distance / speed)
        {
            transform.position = Vector3.Lerp(_startPosition + new Vector3(0f, distance, 0f), _startPosition, elapsedTime / (distance / speed));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = _startPosition;
        _isTriggered = false;
    }
}
