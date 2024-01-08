using System.Collections.Generic;

public class MovementGameAction : GameAction<GridCellCollection>
{
    private MoveActionData moveData = new MoveActionData();

    public MovementGameAction(
        GridObjectEntity source,
        ActionData parent = null,
        GridObjectEntity actionTarget = null
    )
    {
        this.data.moveActionData = moveData;
        this.data.source = source;

        if (actionTarget != null)
        {
            this.data.targets.Add(actionTarget);
        }
        else
        {
            this.data.targets.Add(source);
        }
    }

    //Currently, multi-target will not have multiple destinations. Needs fix.
    public override void Execute(GridCellCollection destination, ActionData parent = null)
    {
        this.data.moveActionData.destination = destination;
        this.data.parent = parent;

        foreach (GridObjectEntity entity in this.data.targets)
        {
            List<GridObject> targets = entity.GetComponentsOfType<GridObject>();
            foreach (GridObject target in targets)
            {
                target.Move(this);
            }
        }

        Debug.Log("Execute Unimplemented");
    }
}
