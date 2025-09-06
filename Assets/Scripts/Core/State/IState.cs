namespace Player47.Core.State
{
    public interface IState
    {
        void Enter();
        void Execute();
        void Exit();
    }   

}