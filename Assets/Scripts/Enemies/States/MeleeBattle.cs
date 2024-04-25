using UnityEngine;

public class MeleeBattle : State
{
    private Transform _source;
    private MeleeAttack _meleeAttack;
    private Movement _movement;
    private Transform _target;

    public MeleeBattle(Movement movement, MeleeAttack meleeAttack, Transform source)
    {
        _meleeAttack = meleeAttack;
        _movement = movement;
        _source = source;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public override void Update()
    {
        if (Mathf.Abs(_source.position.x - _target.position.x) > _meleeAttack.AttackDistance)
        {
            _movement.MoveToTarget(_target.position);
        }
        else
        {
            _meleeAttack.Attack();
        }
    }
}

