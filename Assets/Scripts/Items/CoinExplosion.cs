using UnityEngine;
using System;

public class CoinExplosion : MonoBehaviour
{
    [SerializeField] private int _coinCount;
    [SerializeField] private float _explosionForce;
    [SerializeField] private GameObject _coinPrefab;

    public void Exploision(int count = 1)
    {

        if (count <= 0)
            throw new InvalidOperationException("Coin count should be more than 0");

        if (count == 1)
        {
            Rigidbody2D rb = Instantiate(_coinPrefab, transform.position, transform.rotation).GetComponent<Rigidbody2D>();
            rb.AddForce(Vector2.up * _explosionForce);
        }
        else if (count > 1)
        {
            for (int i = 0; i < _coinCount; i++)
            {
                int sign = 0;
                if (i % 2 == 0)
                    sign = 1;
                else
                    sign = -1;

                Rigidbody2D rb = Instantiate(_coinPrefab, transform.position, transform.rotation).GetComponent<Rigidbody2D>();
                rb.AddForce(new Vector2(UnityEngine.Random.Range(0f, 0.6f) * sign, 1f) * _explosionForce);
            }
        }
    }
}
