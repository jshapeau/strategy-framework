namespace GameState
{
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

        public void SwitchToUnitActionContextState(
            Commandable commandable,
            IGameState parent = null
        )
        {
            baseState.SwitchToUnitActionContextState(commandable, parent ?? this);
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
