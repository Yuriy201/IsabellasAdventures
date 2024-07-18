using System.Collections;
using UnityEngine;
using Player;
using DG.Tweening;
using System.Linq;

namespace ActiveEnviroment
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
    public class FallingPlatform : MonoBehaviour
    {       
        [SerializeField] private float _timeToFall = 3;
        [SerializeField] private string _animatorArgumentName;
        [SerializeField] private float _restoreTime = 5;

        private Vector3 initPosition;
        private Color initColor;

        private Animator _animator;
        private Rigidbody2D _rigidbody;
        private Collider2D _triggerCollider;

        private SpriteRenderer _spriteRenderer;

        private void OnValidate()
        {
            _animator = GetComponent<Animator>();
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
            _animator.SetTrigger(_animatorArgumentName);
            yield return new WaitForSeconds(_timeToFall);
            _rigidbody.isKinematic = false;
            _triggerCollider.enabled = false;

            _spriteRenderer.DOFade(0, _restoreTime * 0.5f).SetDelay(_restoreTime * 0.5f).OnComplete(Restore);
        }

        private void Restore()
        {
            _rigidbody.isKinematic = true;
            _animator.ResetTrigger(_animatorArgumentName);
            transform.position = initPosition;
            _spriteRenderer.color = initColor;
            _triggerCollider.enabled = true;
        }
    } 
}