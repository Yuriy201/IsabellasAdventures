using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class WolfDie : MonoBehaviour
{
    [SerializeField] private Wolf _wolf;
    [SerializeField] private int _enemyHealth;
    
    public AudioClip[] din;
    private int audioCount, audioId;
    private bool _canHurt;
    private int _maxHealth;
    public int CurrenEnemyHealth => _enemyHealth;
    public event UnityAction<int, int> EnemyHealthCheanged;

    private void Start()
    {
        _maxHealth = _enemyHealth;
    }

    public void TakeDamage(int amount)
    {
        _enemyHealth -= amount;
        StartCoroutine(Hurt());
        EnemyHealthCheanged?.Invoke(_enemyHealth, _maxHealth);
        if (_enemyHealth <= 0)
        {
            _wolf.ItemDrop();
            _wolf.Experience();
            Die();
            //PlaySlashAudio();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    IEnumerator Hurt()
    {
        if (_enemyHealth > 0)
        {
            _canHurt = false;
            yield return new WaitForSeconds(0.1f);
            _canHurt = true;
        }
    }

    private void PlaySlashAudio()
    {
        audioCount = din.Length;
        audioId = Random.Range(0, audioCount);
        gameObject.GetComponent<AudioSource>().PlayOneShot(din[audioId]);
    }

    // void OnTriggerEnter2D(Collider2D col)
    // {
    //     if (col.tag == "Player")
    //     {
    //         //Instantiate(bloodSpot, transform.position, Quaternion.identity);
    //         PlaySlashAudio();
    //     }
    // }
}
