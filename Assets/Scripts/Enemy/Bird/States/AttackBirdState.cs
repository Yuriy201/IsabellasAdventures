using System.Collections;
using UnityEngine;
using FSM;

namespace Enemy.Bird
{
    public class AttackBirdState : BirdState, IDamageDiller
    {
        public AttackBirdState(StateMachine<BirdState> stateMachine, Bird bird) 
            : base(stateMachine, bird) { }

        public override void Enter()
        {
            _bird.StartCoroutine(AttackRoutine());
        }

        public override void Exit()
        {
            
        }

        public override void Operate()
        {
            if (_bird.TouchingTarget == null)
            {
                _stateMachine.GoTo<FollowBirdState>();
                return;
            }
        }

        private IEnumerator AttackRoutine()
        {
            while (_bird.TouchingTarget != null) 
            {
                _bird.TouchingTarget.Stats.RemoveHealth(this);
                yield return null;

                var sprR = _bird.GetComponent<SpriteRenderer>();
                for (int i = 0; i < 4; i++)
                {
                    sprR.color = Color.white;
                    yield return new WaitForSeconds(0.2f);

                    sprR.color = Color.red;
                    yield return new WaitForSeconds(0.2f);
                }
            }
        }

        public int GetDamageValue() => _bird.Damage;
    }
}
