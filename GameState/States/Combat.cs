using System.Collections.Generic;

namespace GameState
{
    public class Combat : GameState, ICombatGameState
    {
        //STATE DEPENDENCIES
        [Zenject.Inject]
        private UnitActionMenu unitActionMenuState;

        [Zenject.Inject]
        private GridCellCollection defaultSelection;

        public override GameStateType StateType { get; } = GameStateType.Combat;
        public IDirectionalSelector Selector => selector;

        private IGridCellSelector selector;

        public Combat(IGridCellSelector selector)
            : base()
        {
            this.PreviousState = null;
            this.selector = selector;
        }

        //Needs better handling of multiple commandable results.
        private void OnSelect(GridCellCollection gridCells)
        {
            Commandable commandable = gridCells.GetFirstComponentOfType<Commandable>();
            Debug.Log(commandable);
            this.SwitchToUnitActionContextState(commandable);
        }

        public override void Activate()
        {
            this.selector.Initialize(this.defaultSelection);
            this.selector.OnSelect += this.OnSelect;
            base.Activate();
        }

        public override void Deactivate()
        {
            this.selector.Deactivate();
            this.selector.OnSelect -= this.OnSelect;
            base.Deactivate();
        }

        public override void Reactivate()
        {
            this.Activate();
        }

        public virtual void SwitchToUnitActionContextState(
            Commandable commandable,
            IGameState parent = null
        )
        {
            if (commandable != null)
            {
                this.unitActionMenuState.Activate(commandable, parent ?? this);
            }
        }
    }
}
