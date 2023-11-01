using System;

public class GridCellCommand : ICommand
{
    public string Name { get; private set; }
    public Action<ActionData> OnExecute { get; set; }
    public IDirectionalSelector Selector { get => this.selector; }

    private GridObjectEntity source { get; }
    private MovementGameAction GameAction;
    private IGridCellSelector selector;

    public GridCellCommand(
        string name,
        GridObjectEntity source,
        IGridCellSelector selector)
    {
        this.Name = name;
        this.source = source;
        this.selector = selector;
    }

    public void Initialize()
    {
        this.selector.Initialize(this.source.Location);
        this.selector.OnSelect += Execute;
    }

    public void Execute(GridCellCollection cells)
    {
        this.GameAction = new MovementGameAction(this.source);
        GameAction.Execute(cells);
        this.OnExecute.Invoke(GameAction.data);
    }

    public override string ToString()
    {
        return $"Command: {Name}";
    }

}
