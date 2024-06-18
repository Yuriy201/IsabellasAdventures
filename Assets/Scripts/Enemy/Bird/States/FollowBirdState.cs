using UnityEngine;
using FSM;

namespace Enemy.Bird
{
    public class FollowBirdState : BirdState
    {
        public FollowBirdState(StateMachine<BirdState> stateMachine, Bird bird) 
            : base(stateMachine, bird) { }

        public override void Enter()
        {
            //
        }

        public override void Exit()
        {
            //
        }

        public override void Operate()
        {
            if (_bird.Target == null)
            {
                _stateMachine.GoTo<PatrolBirdState>();
                return;
            }
            else if (_bird.TouchingTarget != null)
            {
                _stateMachine.GoTo<AttackBirdState>();
                return;
            }

            _bird.transform.position = Vector2.MoveTowards(_bird.transform.position, 
                _bird.Target.transform.position, _bird.FollowSpeed * Time.deltaTime);

            if (_bird.transform.position.x < _bird.Target.transform.position.x)
                _bird.transform.rotation = Quaternion.Euler(0, 180, 0);
            else
                _bird.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
