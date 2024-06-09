using UnityEngine;
using FSM;

namespace Enemy.Wolf
{
    public class AttackWolfState : WolfState, IDamageDiller
    {
        public AttackWolfState(StateMachine<WolfState> stateMachine, Wolf wolf) 
            : base (stateMachine, wolf) { }

        public override void Enter()
        {
            Debug.Log($"<color=green>Enter in Attack</color>");

            _wolf.AttackAnimationCallback += Attack;
            _wolf.Rigidbody2D.velocity = new Vector2(0, _wolf.Rigidbody2D.velocity.y);
            _wolf.Animator.SetBool("Attacking", true);
            
        }

        public override void Exit()
        {
            _wolf.Rigidbody2D.velocity = new Vector2(0, _wolf.Rigidbody2D.velocity.y);
            _wolf.Animator.SetBool("Attacking", false);

            _wolf.AttackAnimationCallback -= Attack;
        }

        public override void Operate()
        {
            if (_wolf.TouchingTarget == null)
                _stateMashine.GoTo<FollowWolfState>();
        }

        private void Attack()
        {
            if (_wolf.TouchingTarget != null)
                _wolf.Target.Stats.RemoveHealth(this);
        }

        public int GetDamageValue() => _wolf.Damage;
    }
}
