using System.Collections;
using System.Collections.Generic;

public interface ILevelGrid
{
    public IGridCellGraph Cells {get; set;}
    public GridPosition GetGridDimensions();
    public Vector3 GetGridScale();
    public GridPosition SanitizeGridPosition(GridPosition gridPosition);
    public GridPositionCollection SanitizeGridPosition(GridPositionCollection gridPositions);
    public bool IsValidGridPosition(GridPosition gridPosition);
}
