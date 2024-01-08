using System;

public interface IGameState
{
    public string Name { get; }
    public IGameState PreviousState { get; }
    public GameState.GameStateType StateType { get; }
    public Action OnStateEntered { get; set; }
    public void ReturnToPreviousState();
    public void Reactivate();
    public void Deactivate();
}
