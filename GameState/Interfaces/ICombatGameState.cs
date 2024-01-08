namespace GameState
{
    public interface ICombatGameState : IGameState
    {
        public IDirectionalSelector Selector { get; }
        public void SwitchToUnitActionContextState(Commandable commandable, IGameState parent);
    }
}
