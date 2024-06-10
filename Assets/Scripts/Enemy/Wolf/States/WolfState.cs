using FSM;

namespace Enemy.Wolf
{
    public abstract class WolfState : IState
    {
        protected StateMachine<WolfState> _stateMashine;
        protected Wolf _wolf;

        public WolfState(StateMachine<WolfState> stateMachine, Wolf wolf)
        {
            _stateMashine = stateMachine;
            _wolf = wolf;
        }

        public abstract void Enter();

        public abstract void Exit();

        public abstract void Operate();
    }
}