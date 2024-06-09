using UnityEngine;
using Player;
using FSM;

namespace Enemy.Wolf
{
    public class PatrolWolfState : WolfState
    {
        public PatrolWolfState(StateMachine<WolfState> stateMachine, Wolf wolf)
            : base(stateMachine, wolf) { }

        public override void Enter()
        {
            Debug.Log($"<color=green>Enter in Patrol</color>");
            _wolf.Rigidbody2D.velocity = new Vector2(_wolf.Speed, _wolf.Rigidbody2D.velocity.y);
        }

        public override void Exit() { }

        public override void Operate()
        {
            if (_wolf.Target != null)
            {
                _stateMashine.GoTo<FollowWolfState>();
                return;
            }

            if (_wolf.transform.position.x < _wolf.LeftExetremPoint.position.x)
                _wolf.transform.rotation = Quaternion.Euler(0, 0, 0); 

            else if (_wolf.transform.position.x > _wolf.RightExetremPoint.position.x)
                _wolf.transform.rotation = Quaternion.Euler(0, 180, 0);

            _wolf.Rigidbody2D.velocity = new Vector2(_wolf.Speed * _wolf.transform.right.x, _wolf.Rigidbody2D.velocity.y);
        }
    }
}