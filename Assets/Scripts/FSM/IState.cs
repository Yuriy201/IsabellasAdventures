namespace FSM
{
    public interface IState
    {
        void Enter();

        void Operate();

        void Exit();
    }
}