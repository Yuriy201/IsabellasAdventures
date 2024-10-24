using System.Collections;
using UnityEngine;
using FSM;

namespace Enemy.Bird
{
    public class AttackCrowState : CrowState, IDamageDiller
    {
        private bool attacking = false;
        public AttackCrowState(StateMachine<CrowState> stateMachine, Crow crow)
            : base(stateMachine, crow) { }

        public override void Enter()
        {
            _crow.AttackAnimationCallback += Attack;
        }

        public override void Exit()
        {
            _crow.Animator.Play(_crow.FlyHash);

            _crow.AttackAnimationCallback -= Attack;
        }

        public override void Operate()
        {
            if (attacking)
                return;

            if (_crow._distanceTrigger.CurrentDistance > _crow._attackRadius)
            {
                _crow.TouchingTarget = null;
                _stateMachine.GoTo<FollowCrowState>();
            }
            else
            {
                attacking = true;

                _crow.StartCoroutine(AttackRoutine());
            }

        }

        private IEnumerator AttackRoutine()
        {
            _crow.Animator.Play(_crow.AttackHash, -1, 0);

            yield return new WaitForSeconds(_crow.DamageCooldown);

            attacking = false;
        }

        private void Attack()
        {
            if (_crow._distanceTrigger.CurrentDistance <= _crow._attackRange)
                _crow.TouchingTarget.Stats.RemoveHealth(this);
        }

        public int GetDamageValue() => _crow.Damage;
    }
}
