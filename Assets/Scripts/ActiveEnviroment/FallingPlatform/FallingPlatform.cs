using System.Collections;
using UnityEngine;
using Player;

namespace ActiveEnviroment
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
    public class FallingPlatform : MonoBehaviour
    {
        [SerializeField] private float _timeToFall;
        [SerializeField] private string _animatorArgumentName;

        private Animator _animator;
        private Rigidbody2D _rigidbody;

        private void OnValidate()
        {
            _animator ??= GetComponent<Animator>();
            _rigidbody ??= GetComponent<Rigidbody2D>();

            if (_rigidbody != null )
                _rigidbody.isKinematic = true;
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
        }
    } 
}