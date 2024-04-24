using System;
using System.Collections;
using Script.Enemy.EnemyWithDamage;
using UnityEngine;

namespace PlayerScripts.Spell
{
    public class BaseSpell : MonoBehaviour
    {
        [SerializeField] private int _speed;
        [SerializeField] private int _damage;
        [SerializeField] private int _manaSpell;
        [SerializeField] private float _lifeTime;
        public int ManaSpell => _manaSpell;

        private void Start()
        {
            if (transform.rotation.y == 0)
            {
                GetComponent<Rigidbody2D>().AddForce(Vector2.right * 25, ForceMode2D.Impulse);
            }
            else
            {
                GetComponent<Rigidbody2D>().AddForce(Vector2.left * 25, ForceMode2D.Impulse);

            }
            
            StartCoroutine(LifeTime());
        }
        
        IEnumerator LifeTime()
        {
            yield return new WaitForSeconds(_lifeTime);
            Destroy(gameObject);
            yield break;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out EnemyWithDamage enemy))
            {
                enemy.TakeDamage(_damage);
                Destroy(gameObject);
            }
        }
    }
}