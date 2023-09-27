namespace GameState {

public interface ICombatGameState : IGameState
{
    public IDirectionalSelector Selector { get; }
    public void SwitchToUnitActionContextState(IUnit unit, IGameState parent);
}

}