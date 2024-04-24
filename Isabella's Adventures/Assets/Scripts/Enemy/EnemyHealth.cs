using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int Enemyhealth = 50;
    [SerializeField] private AudioSource soundSource;
    [SerializeField] private AudioClip WolfDamage;
    [SerializeField] private AudioClip WolfDie;
    
    public float life;
  
    public void TakeDamage(int amount)
    {
        Enemyhealth -= amount;
        soundSource.PlayOneShot(WolfDamage);
        if (Enemyhealth <= 0) 
        {
            StartCoroutine(StartDie());
        }
    }
    IEnumerator StartDie()
    {
        soundSource.PlayOneShot(WolfDie);
        yield return new WaitForSeconds(life);

        Destroy(gameObject);
        yield break;
    }
}
