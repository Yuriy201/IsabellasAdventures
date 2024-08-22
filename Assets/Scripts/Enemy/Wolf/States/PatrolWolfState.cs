using UnityEngine;
using Player;
using FSM;

namespace Enemy.Wolf
{
    public class PatrolWolfState : WolfState
    {
        private float walkTimer;
        private bool walking;

        private float stayTimer;
        private bool staying;

        private Vector3 nextMovePoint;

        public PatrolWolfState(StateMachine<WolfState> stateMachine, Wolf wolf)
            : base(stateMachine, wolf) { }

        public override void Enter()
        {
            walkTimer = Random.Range(12f, 14f);

            walking = true;
            staying = false;

            _wolf.Animator.SetFloat("WalkMult", 1);

            Debug.Log($"<color=green>Enter in Patrol</color>");
            //_wolf.Rigidbody2D.velocity = new Vector2(_wolf.Speed, _wolf.Rigidbody2D.velocity.y);
        }

        public override void Exit() { }

        public override void Operate()
        {
            if (_wolf.Target != null)
            {
                _stateMashine.GoTo<FollowWolfState>();
                return;
            }

            if (!staying && walkTimer > 0f)
            {
                if (walking)
                {
                    _wolf.Animator.Play(_wolf.WalkHash);

                    walking = false;
                }

                walkTimer -= Time.deltaTime;

                if (_wolf.transform.position.x < _wolf.LeftExetremPoint.position.x)
                    _wolf.transform.rotation = Quaternion.Euler(0, 0, 0);

                else if (_wolf.transform.position.x > _wolf.RightExetremPoint.position.x)
                    _wolf.transform.rotation = Quaternion.Euler(0, 180, 0);

                _wolf.Rigidbody2D.velocity = new Vector2(_wolf.Speed * _wolf.transform.right.x, _wolf.Rigidbody2D.velocity.y);

                if (walkTimer <= 0f)
                {
                    walking = false;
                    staying = true;

                    stayTimer = Random.Range(1f, 2f);
                }

                return;
            }

            if (!walking)
            {
                if (stayTimer > 0f)
                {
                    if (staying)
                    {
                        _wolf.Animator.Play(_wolf.IdleHash);

                        staying = false;
                    }

                    stayTimer -= Time.deltaTime;

                    _wolf.Rigidbody2D.velocity = new Vector2(0, _wolf.Rigidbody2D.velocity.y);
                }
                else
                {
                    staying = false;
                    walking = true;

                    walkTimer = Random.Range(2f, 4f);
                }
            }
        }
    }
}