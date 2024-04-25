using System.Collections;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public float AttackDistance { get => _distance; }
    public bool CanAttack { get => _canAttack; }

    [SerializeField] private DamageSource _dmgSource;
    [SerializeField] private int _damage;
    [SerializeField] private float _delay;
    [SerializeField] private float _distance;

    private WaitForSeconds _attackDelay;
    private bool _canAttack = true;
    private Animator _anim;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _dmgSource.Init(_damage);
        _dmgSource.gameObject.SetActive(false);
    }

    public void Attack()
    {
        if (!_canAttack) return;
        _canAttack = false;
        _dmgSource.gameObject.SetActive(true);
        _anim.SetTrigger("Attack");
    }

    private void AttackEnded()
    {
        _dmgSource.gameObject.SetActive(false);
        StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        yield return _attackDelay;
        _canAttack = true;
    }
}

