using System;

public class GridCellCommand : ICommand
{
    public string Name { get; private set; }
    public Action<IGameAction> OnExecute { get; set; }
    public IDirectionalSelector Selector { get => this.selector; }


    private IUnit Unit { get; } //ToDo: Should be provided directly to the action by factory method.
    private Func<GridCellCollection> getGridCells;
    private IGameAction GameAction;
    private IGridCellSelector selector;

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
    }

    public void Initialize()
    {
        this.selector.Initialize(this.getGridCells());
        this.selector.OnSelect += Execute;
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
