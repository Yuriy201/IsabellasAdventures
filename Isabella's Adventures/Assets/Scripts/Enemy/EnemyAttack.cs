using System;
using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private int _damage;
        [SerializeField] private Animator _animator;
        [SerializeField] private float _cooldown = 3f;

        private Player _player;
        private float distance;
        private bool _canAttack = true;
        private int _currentSpeed;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Player player))
            {
                _currentSpeed = 0;

                if (_canAttack)
                {
                    //_attackSound.Play();
                    //_animator.PlayAttack();
                    player.ApplyDamage(_damage);
                    StartCoroutine(ResetAttack());
                }                
            }
        }

        private IEnumerator ResetAttack()
        {
            _canAttack = false;
            yield return new WaitForSeconds(_cooldown);
            _canAttack = true;
        }
    }
}