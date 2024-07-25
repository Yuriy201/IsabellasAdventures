using UnityEngine;
using Player;
using FSM;


namespace Enemy.Wolf
{
    public class FollowWolfState : WolfState
    {
        private float speedMult = 1.2f;
        private float distanceCheckCdTimer = 0.5f;

        public FollowWolfState(StateMachine<WolfState> stateMachine, Wolf wolf)
            : base(stateMachine, wolf) { }

        public override void Enter()
        {
            _wolf.Animator.Play(_wolf.WalkHash);
            Debug.Log($"<color=yellow>Enter in Follow</color>");
        }

        public override void Exit()
        {
        }

        public override void Operate()
        {
            if (_wolf.Target == null)
            {
                _stateMashine.GoTo<PatrolWolfState>();
                return;
            }

            if (_wolf.DistanceTrigger.ClosestPlayer != null && _wolf.DistanceTrigger.CurrentDistance <= _wolf.AttackRadius)
            {
                _stateMashine.GoTo<AttackWolfState>();
                return;
            }

            distanceCheckCdTimer -= Time.deltaTime;

            if (distanceCheckCdTimer <= 0f && _wolf.DistanceTrigger.ClosestPlayer == null)
            {
                distanceCheckCdTimer = 0.5f;

                float distance = (_wolf.Target.transform.position - _wolf.transform.position).sqrMagnitude;

                if (distance >= _wolf.DistanceToUnAgro)
                {
                    _wolf.Target = null;
                }
            }
          
            if (_wolf.transform.position.x < _wolf.Target.transform.position.x)
                _wolf.transform.rotation = Quaternion.Euler(0, 0, 0);

            else if (_wolf.transform.position.x > _wolf.Target.transform.position.x)
                _wolf.transform.rotation = Quaternion.Euler(0, 180, 0);

            _wolf.Rigidbody2D.velocity = new Vector2(_wolf.Speed * _wolf.transform.right.x * speedMult, _wolf.Rigidbody2D.velocity.y);
        }
    }
}
