using UnityEngine;
using Player;
using FSM;


namespace Enemy.Wolf
{
    public class FollowWolfState : WolfState
    {
        public FollowWolfState(StateMachine<WolfState> stateMachine, Wolf wolf)
            : base(stateMachine, wolf) { }

        public override void Enter()
        {
            Debug.Log($"<color=yellow>Enter in Follow</color>");
            _wolf.GetComponent<SpriteRenderer>().color = Color.red;
        }

        public override void Exit()
        {
            _wolf.GetComponent<SpriteRenderer>().color = Color.white;
        }

        public override void Operate()
        {
            if(_wolf.Target == null)
            {
                _stateMashine.GoTo<PatrolWolfState>();
                return;
            }
            if (_wolf.TouchingTarget != null)
            {
                _stateMashine.GoTo<AttackWolfState>();
                return;
            }

            if (_wolf.transform.position.x < _wolf.Target.transform.position.x)
                _wolf.transform.rotation = Quaternion.Euler(0, 0, 0);

            else if (_wolf.transform.position.x > _wolf.Target.transform.position.x)
                _wolf.transform.rotation = Quaternion.Euler(0, 180, 0);

            _wolf.Rigidbody2D.velocity = new Vector2(_wolf.Speed * _wolf.transform.right.x, _wolf.Rigidbody2D.velocity.y);
        }
    }
}
