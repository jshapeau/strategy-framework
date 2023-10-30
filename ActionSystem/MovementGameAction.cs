using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementGameAction : GameAction<GridCellCollection>
{
    private MoveActionData moveData = new MoveActionData();

    public MovementGameAction(
        GridObjectEntity source,
        GridCellCollection destination,
        ActionData parent = null,
        GridObjectEntity actionTarget = null
    )
    {
        this.data.moveActionData = moveData;
        data.moveActionData.destination = destination;
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

    public override void Execute(GridCellCollection target)
    {
        Debug.Log("Execute Unimplemented");
    }
}
