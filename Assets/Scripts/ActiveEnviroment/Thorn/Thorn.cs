using UnityEngine;
using Player;
using System.Collections.Generic;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class Thorn : MonoBehaviour
{
    private HashSet<Collider2D> hittedPlayers = new HashSet<Collider2D>();
    private WaitForSeconds hitCdWait;

    [SerializeField]
    private int _damage;

    [SerializeField]
    private float _hitCd;
    //public int GetDamageValue() => _damage;

    private void Start()
    {
        hitCdWait = new WaitForSeconds(_hitCd);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!hittedPlayers.Contains(collision) && collision.TryGetComponent(out PlayerController player))
        {
            //_damage = player.Stats.MaxHealth;
            player.Stats.RemoveHealth(_damage);
            hittedPlayers.Add(collision);

            StartCoroutine(HitCd(collision));
        }
    }

    private IEnumerator HitCd(Collider2D collision)
    {
        yield return hitCdWait;

        hittedPlayers.Remove(collision);
    }
}
