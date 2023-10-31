using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public override void Execute(GridCellCollection destination, ActionData parent = null)
    {
        this.data.moveActionData.destination = destination;
        this.data.parent = parent;

        Debug.Log("Execute Unimplemented");
    }
}
