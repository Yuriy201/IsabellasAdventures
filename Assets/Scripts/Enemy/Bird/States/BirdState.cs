using FSM;

namespace Enemy.Bird
{
    public abstract class BirdState : IState
    {
        protected StateMachine<BirdState> _stateMachine;
        protected Bird _bird;

        public BirdState(StateMachine<BirdState> stateMachine, Bird bird)
        {
            _stateMachine = stateMachine;
            _bird = bird;
        }

        public abstract void Enter();

        public abstract void Exit();

        public abstract void Operate();
    }
}