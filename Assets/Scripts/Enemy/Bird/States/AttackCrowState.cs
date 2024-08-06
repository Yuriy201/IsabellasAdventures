using System.Collections;
using UnityEngine;
using FSM;

namespace Enemy.Bird
{
    public class AttackCrowState : CrowState, IDamageDiller
    {
        public AttackCrowState(StateMachine<CrowState> stateMachine, Crow crow) 
            : base(stateMachine, crow) { }

        public override void Enter()
        {
            _crow.StartCoroutine(AttackRoutine());
        }

        public override void Exit()
        {
            _crow.Animator.Play(_crow.FlyHash);
        }

        public override void Operate()
        {
            if (_crow.TouchingTarget == null)
                _stateMachine.GoTo<FollowCrowState>();
        }

        private IEnumerator AttackRoutine()
        {
            while (_crow.TouchingTarget != null)
            {
                _crow.Animator.Play(_crow.AttackHash);
                _crow.TouchingTarget.Stats.RemoveHealth(this);
                yield return new WaitForSeconds(_crow.DamageCooldown);
            }
        }

        public int GetDamageValue() => _crow.Damage;
    }
}
