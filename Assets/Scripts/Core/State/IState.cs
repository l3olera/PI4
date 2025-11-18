namespace Player47.Core.State
{
    /// <summary>
    /// Interface para estados da máquina de estadoss
    /// </summary>
    public interface IState
    {
        void Enter();
        void Update();
        void Exit();
    }   

}