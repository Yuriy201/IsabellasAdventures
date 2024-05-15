using DG.Tweening;
using UnityEngine;

public class Coin : MonoBehaviour
{ 
    [SerializeField] private Transform _Point;
    [SerializeField] private float _animTime;
    
    private void Start()
    {
        transform.DOMove(_Point.position, _animTime).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            Destroy(gameObject);
        }
    }
}