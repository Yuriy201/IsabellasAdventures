using System.Collections;
using UnityEngine;
using Player;
using DG.Tweening;
using System.Linq;

namespace ActiveEnviroment
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class FallingPlatform : MonoBehaviour
    {       
        [SerializeField] private float _timeToFall = 3;
        [SerializeField] private float _restoreTime = 5;

        private Vector3 initPosition;
        private Color initColor;

        private Rigidbody2D _rigidbody;
        private Collider2D _triggerCollider;

        private SpriteRenderer _spriteRenderer;

        private void OnValidate()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _triggerCollider = GetComponents<Collider2D>().Where(x => x.isTrigger).First();

            if (_rigidbody != null )
                _rigidbody.isKinematic = true;
        }

        private void Start()
        {
            initPosition = transform.position;
            initColor = _spriteRenderer.color;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out PlayerController player))
                StartCoroutine(FallRoutine());
        }
        private IEnumerator FallRoutine()
        {
            yield return new WaitForSeconds(_timeToFall);
            _rigidbody.isKinematic = false;
            _triggerCollider.enabled = false;

            _spriteRenderer.DOFade(0, _restoreTime * 0.5f).SetDelay(_restoreTime * 0.5f).OnComplete(Restore);
        }

        private void Restore()
        {
            _rigidbody.isKinematic = true;
            transform.position = initPosition;
            _spriteRenderer.color = initColor;
            _triggerCollider.enabled = true;
        }
    } 
}