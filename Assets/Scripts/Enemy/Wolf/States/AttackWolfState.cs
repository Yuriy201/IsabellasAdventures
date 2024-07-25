using UnityEngine;
using FSM;
using Player;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace Enemy.Wolf
{
    public class AttackWolfState : WolfState, IDamageDiller
    {
        private float attackCdTimer;

        public AttackWolfState(StateMachine<WolfState> stateMachine, Wolf wolf)
            : base(stateMachine, wolf) { }

        public override void Enter()
        {
            Debug.Log($"<color=green>Enter in Attack</color>");

            _wolf.AttackAnimationCallback += Attack;
            _wolf.Rigidbody2D.velocity = new Vector2(0, _wolf.Rigidbody2D.velocity.y);

            attackCdTimer = -1f;
        }

        public override void Exit()
        {
            _wolf.Rigidbody2D.velocity = new Vector2(0, _wolf.Rigidbody2D.velocity.y);

            _wolf.AttackAnimationCallback -= Attack;
        }

        public override void Operate()
        {
            attackCdTimer -= Time.deltaTime;

            _wolf.Rigidbody2D.velocity = new Vector2(0, _wolf.Rigidbody2D.velocity.y);

            if (attackCdTimer <= 0f)
            {
                if (_wolf.DistanceTrigger.ClosestPlayer == null)
                {
                    _stateMashine.GoTo<FollowWolfState>();

                    return;
                }
                else
                {
                    if (_wolf.DistanceTrigger.CurrentDistance > _wolf.AttackRadius)
                    {
                        _stateMashine.GoTo<FollowWolfState>();
                    }
                    else
                    {
                        if (_wolf.transform.position.x < _wolf.Target.transform.position.x)
                            _wolf.transform.rotation = Quaternion.Euler(0, 0, 0);

                        else if (_wolf.transform.position.x > _wolf.Target.transform.position.x)
                            _wolf.transform.rotation = Quaternion.Euler(0, 180, 0);

                        _wolf.Animator.Play(_wolf.BiteHash, -1, 0f);

                        attackCdTimer = _wolf.AttackCd;
                    }
                }
            }
        }

        private void Attack()
        {
            var hits = Physics2D.BoxCastAll(_wolf.Rigidbody2D.position + Vector2.Scale(_wolf.AttackAreaOffset, _wolf.transform.right), _wolf.AttackArea, 0, _wolf.transform.right, 0, _wolf.AttackLayers);

            foreach (var player in hits)
            {
                if (player.transform != null)
                    player.transform.GetComponent<PlayerController>().Stats.RemoveHealth(_wolf.Damage);
            }
        }

        public int GetDamageValue() => _wolf.Damage;
    }
}
