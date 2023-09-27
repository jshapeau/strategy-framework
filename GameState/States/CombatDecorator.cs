namespace GameState {

/// <summary>
/// Allows states to inherit dependencies directly from Combat game states.
/// </summary>
public abstract class CombatDecorator : GameState, ICombatGameState
{
    public abstract IDirectionalSelector Selector { get; }
    private ICombatGameState baseState;

    public CombatDecorator(ICombatGameState baseState)
    {
        this.baseState = baseState;
    }

    public void SwitchToUnitActionContextState(IUnit unit, IGameState parent = null) 
    {
        baseState.SwitchToUnitActionContextState(unit, parent ?? this);
    }

    public override void Deactivate()
    {
        baseState.Deactivate();
    }

    public override void Reactivate()
    {
        baseState.Reactivate();
    }
}

}