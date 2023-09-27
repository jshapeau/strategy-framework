using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGridCellGraph
{
    public bool Exists(GridPosition gridPosition);
    public bool Exists(GridPositionCollection gridPositions);
    public bool IsOpen(List<GridCell> gridCells);
    public GridCell At(GridPosition gridPosition);
    public GridCellCollection At(GridPositionCollection gridPositions);
    
    public GridCellCollection GetAllGridCells();
    public GridPositionCollection GetGridPositions(GridCellCollection gridCells);
}
