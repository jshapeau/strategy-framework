using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWorldToGridAdapter
{
    public ILevelGrid Grid {get; set;}
    public Vector3 GetAverageWorldPosition(GridPositionCollection gridPositions);
    public GridCell WorldPositionToGridCell(Vector3 position);
    public Vector3 GridPositionToWorldPosition(GridPosition gridPosition);
    public GridPosition WorldPositionToGridPosition(Vector3 position);
    public GridPositionCollection WorldPositionToGridPosition(List<Vector3> position);
}
