using FSM;

namespace Enemy.Bird
{
    public class FollowCrowState : CrowState
    {
        public FollowCrowState(StateMachine<CrowState> stateMachine, Crow crow) 
            : base(stateMachine, crow) { }

        public override void Enter() { }

        public override void Exit() { }

        public override void Operate()
        {
            if (_crow.Target == null)
                _stateMachine.GoTo<PatrolCrowState>();
            else if (_crow._distanceTrigger.CurrentDistance < _crow._attackRadius)
            {
                _crow.TouchingTarget = _crow._distanceTrigger.ClosestPlayer;
                _stateMachine.GoTo<AttackCrowState>();
            }               
            else
            {
                _crow.Move(_crow.Target.transform.position, _crow.FollowSpeed);
                _crow.Rotate(_crow.Target.transform.position);
            }
        }
    }
}
