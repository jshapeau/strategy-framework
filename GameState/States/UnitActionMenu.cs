using System.Collections.Generic;
using UnityEngine;

namespace GameState {

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
    private IUnit unit;
    private List<GridCellCommand> commands = new List<GridCellCommand>();

    public UnitActionMenu(UI.UnitActionMenu unitActionMenu, 
        UnitSelectedVisual unitSelectedVisual,
        IMenuItemSelector menuItemSelector,
        Combat combatState) 
        : base(combatState) 
    {
        this.Menu = unitActionMenu;
        this.unitSelectedVisual = unitSelectedVisual;
        this.MenuItemSelector = menuItemSelector;

    }

    public override void Reactivate() {
        this.Activate(this.unit, this.PreviousState);
    }

    public void Activate(IUnit unit, IGameState previousState = null) 
    {
        if (previousState != this) {
            this.PreviousState = previousState;
        }

        this.unit = unit;
        this.commands = unit.GetCommands();
           
        this.OnStateEntered?.Invoke();

        this.unitSelectedVisual.Select(unit.GridObject);
        this.Menu.Open();
        this.PopulateMenu(unit.GetCommands());
    }

    private void PopulateMenu(List<GridCellCommand> commands) 
    {
        foreach(GridCellCommand command in commands) {
            this.Menu.AddSelectableItem(command.ToString(), 
                delegate() {SwitchToValidatorGameState(command); 
                } );
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