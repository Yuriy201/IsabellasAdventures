using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingBullet : MonoBehaviour
{
    public float lifeTime, speed;

    private void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.left * speed, ForceMode2D.Impulse);

        StartCoroutine(LifeTime());
    }
    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
        yield break;
    }
}
