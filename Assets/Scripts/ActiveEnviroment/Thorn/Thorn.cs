using UnityEngine;
using Player;
using System.Collections.Generic;
using System.Collections;

public class Thorn : MonoBehaviour
{
    private HashSet<PlayerController> hittedPlayers = new HashSet<PlayerController>();
    private WaitForSeconds hitCdWait;

    [SerializeField]
    private int _damage;

    [SerializeField]
    private int _hitCd;
    //public int GetDamageValue() => _damage;

    private void Start()
    {
        hitCdWait = new WaitForSeconds(_hitCd);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController player))
        {
            if (!hittedPlayers.Contains(player))
            {
                //_damage = player.Stats.MaxHealth;
                player.Stats.RemoveHealth(_damage);
                hittedPlayers.Add(player);

                StartCoroutine(HitCd(player));
            }        
        }
    }

    private IEnumerator HitCd(PlayerController player)
    {
        yield return hitCdWait;

        hittedPlayers.Remove(player);
    }
}
