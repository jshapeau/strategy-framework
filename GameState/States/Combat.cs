using System.Collections.Generic;

namespace GameState {

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
    
    public Combat(IGridCellSelector selector) : base()
    {
        this.PreviousState = null; 
        this.selector = selector;
        this.selector.OnSelect += this.OnSelect;
    }
    
    //Needs Refactor
    private void OnSelect(GridCellCollection gridCells)
    {
        List<GridObject> gridObjects = this.GetGridObjectsInSelection(gridCells);
        foreach(GridObject o in gridObjects)
        {
            if (o.Unit != null)
            {
                this.SwitchToUnitActionContextState(o.Unit);
            }
            break;
        }
    }

    //Needs Refactor
    private List<GridObject> GetGridObjectsInSelection(GridCellCollection gridCells) {
        List<GridObject> gridObjectsInSelection = new List<GridObject>();

        foreach(GridCell gridCell in gridCells) {
            gridObjectsInSelection.AddRange(gridCell.GetGridObjectList());
        }

        return gridObjectsInSelection;
    }
    
    public override void Activate()
    {
        this.selector.Initialize(this.defaultSelection);
        base.Activate();
    }

    public override void Deactivate()
    {
        this.selector.Deactivate();
        base.Deactivate();
    }

    public override void Reactivate() 
    {
        this.Activate();
    }

    public virtual void SwitchToUnitActionContextState(IUnit unit, IGameState parent = null) 
    {   
        if(unit != null) {
            this.unitActionMenuState.Activate(unit, parent ?? this);
        }
    }
}

}
