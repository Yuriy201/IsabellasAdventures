using UnityEngine;
using FSM;

namespace Enemy.Bird
{
    public class PatrolBirdState : BirdState
    {
        public PatrolBirdState(StateMachine<BirdState> stateMachine, Bird bird) 
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
            if (_bird.Target != null)
            {
                _stateMachine.GoTo<FollowBirdState>();
                return;
            }

            _bird.transform.position = Vector2.MoveTowards(_bird.transform.position, 
                _bird.CurrentPoint.position, _bird.Speed * Time.deltaTime);

            if (Vector2.Distance(_bird.transform.position, _bird.CurrentPoint.position) < 0.5f)
                _bird.IncreasePointIndex();

            if (_bird.transform.position.x < _bird.CurrentPoint.position.x)
                _bird.transform.rotation = Quaternion.Euler(0, 180, 0);
            else
                _bird.transform.rotation = Quaternion.Euler(0, 0, 0);           
        }        
    }
}
