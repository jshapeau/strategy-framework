using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



// public interface ICommand<T> : ICommand
// {
//     ITypedDirectionalSelector<T> Selector { get; }

// }



//Depends on GridCells, IAction, and IGridCellValidator
//Use an event to avoid dealing with control changes
public class GridCellCommand : ICommand
{
    public string Name {get; private set;}
    public Action<IGameAction> OnExecute { get; set; }
    public IDirectionalSelector Selector 
    {
        get
        {
            this.selector.Initialize(this.getGridCells());
            return this.selector;
        }
    }
    
    private IUnit Unit { get; } //ToDo: Should be provided directly to the action by factory method.
    private Func<GridCellCollection> getGridCells;
    private IGameAction GameAction;
    private IGridCellSelector  selector;
    
    // ToDo: getGridCells should be part of Selector, and supplied vie a factory method.
    public GridCellCommand(
        string name, 
        IUnit unit, 
        IGridCellSelector selector, 
        Func<GridCellCollection> getGridCells)
    {
        this.Name = name;
        this.Unit = unit;
        this.selector = selector;
        this.getGridCells = getGridCells;

        Initialize();
    }

    public void Initialize()
    {
        selector.OnSelect += Execute;
    }

    public void Execute(GridCellCollection cells)
    {
        GridPositionCollection positions = cells.GetGridPositions();
        
        //ToDo: GameAction Should be provided by a factory method once GameAction is fully implemented.
        this.GameAction = new MoveGameAction(positions, Unit.GridObject); 
        OnExecute?.Invoke(GameAction);
    }

    public override string ToString()
    {
        return $"Command: {Name}";
    }
    
} 
