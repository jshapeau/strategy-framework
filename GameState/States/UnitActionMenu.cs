using System.Collections.Generic;

namespace GameState
{
    public class UnitActionMenu : CombatDecorator
    {
        //STATE DEPENDENCIES
        [Zenject.Inject]
        private Validator validatorState;

        public override GameStateType StateType { get; } = GameStateType.UnitActionMenu;
        public override IDirectionalSelector Selector => this.MenuItemSelector;

        public IDirectionalSelector MenuItemSelector { get; private set; }
        public UI.UnitActionMenu Menu { get; private set; }

        private UnitSelectedVisual unitSelectedVisual;
        private Commandable commandable;
        private List<ICommand> commands = new List<ICommand>();

        public UnitActionMenu(
            UI.UnitActionMenu unitActionMenu,
            UnitSelectedVisual unitSelectedVisual,
            IMenuItemSelector menuItemSelector,
            Combat combatState
        )
            : base(combatState)
        {
            this.Menu = unitActionMenu;
            this.unitSelectedVisual = unitSelectedVisual;
            this.MenuItemSelector = menuItemSelector;
        }

        public override void Reactivate()
        {
            this.Activate(this.commandable, this.PreviousState);
        }

        public void Activate(Commandable commandable, IGameState previousState = null)
        {
            if (previousState != this)
            {
                this.PreviousState = previousState;
            }

            this.commandable = commandable;
            this.commands = commandable.Commands;
            this.OnStateEntered?.Invoke();

            this.unitSelectedVisual.Select(commandable.Entity);
            this.Menu.Open();
            this.PopulateMenu(commandable.Commands);
        }

        private void PopulateMenu(List<ICommand> commands)
        {
            foreach (GridCellCommand command in commands)
            {
                this.Menu.AddSelectableItem(
                    command.ToString(),
                    delegate()
                    {
                        SwitchToValidatorGameState(command);
                    }
                );
            }
        }

        private void SwitchToValidatorGameState(ICommand command)
        {
            this.Deactivate();
            this.validatorState.Activate(command, this);
        }

        public override void Deactivate()
        {
            this.Menu.Close();
            this.unitSelectedVisual.Deselect();
            base.Deactivate();
        }
    }
}
