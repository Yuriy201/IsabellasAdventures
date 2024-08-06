using FSM;

namespace Enemy.Bird
{
    public abstract class CrowState : IState
    {
        protected StateMachine<CrowState> _stateMachine;
        protected Crow _crow;

        public CrowState(StateMachine<CrowState> stateMachine, Crow crow)
        {
            _stateMachine = stateMachine;
            _crow = crow;
        }

        public abstract void Enter();

        public abstract void Exit();

        public abstract void Operate();
    }
}