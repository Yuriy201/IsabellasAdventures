using System.Collections;
using UnityEngine;
using FSM;

namespace Enemy.Bird
{
    public class PatrolCrowState : CrowState
    {
        private Transform _currentPoint;
        private bool _isStay;
        
        public PatrolCrowState(StateMachine<CrowState> stateMachine, Crow crow) 
            : base(stateMachine, crow) { }

        public override void Enter()
        {
            _crow.Animator.Play(_crow.FlyHash);
            _isStay = false;
            _currentPoint = _crow.GetCurrentPoint();
            _crow.Rotate(_currentPoint.position);
        }

        public override void Exit() { }

        public override void Operate()
        {
            if (_crow.Target != null)
                _stateMachine.GoTo<FollowCrowState>();
            else if (_isStay == false)
            {
                if (_crow.transform.position == _currentPoint.position)
                {
                    _isStay = true;
                    _crow.StartCoroutine(StayTimer());
                }
                else
                    _crow.Move(_currentPoint.position, _crow.Speed);
            }
        }

        private IEnumerator StayTimer()
        {
            yield return new WaitForSeconds(_crow.StayTime);
            _crow.ChangePoint();
            _currentPoint = _crow.GetCurrentPoint();
            _isStay = false;
            _crow.Rotate(_currentPoint.position);
            _crow.Move(_currentPoint.position, _crow.Speed);
        }
    }
}
