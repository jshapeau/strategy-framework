using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

/// <summary>
/// Represents a collection of linked GridCells, such as a level grid.
/// </summary>
public class GridCellGraph : IGridCellGraph
{
    GridCell[,,] array;
    ILevelGrid parent;
    
    public GridCellGraph(GridCell[,,] array, ILevelGrid parent)
    {
        this.array = array;
        this.parent = parent;
    }
 
    public GridCellCollection GetAllGridCells() {
        return new GridCellCollection(this.array.Cast<GridCell>().ToList());
    }

    public bool IsOpen(List<GridCell> gridCells)
    {
        foreach(GridCell gridCell in gridCells)
        {
            if (!gridCell.IsOpen())
            {
                return false;
            }
        }

        return true;
    }

    public GridPositionCollection GetGridPositions(GridCellCollection gridCells)
    {
        return this.GetAllGridCells().GetGridPositions();
    }

    public bool Exists(GridPositionCollection gridPositions) {
        foreach (GridPosition gridPosition in gridPositions)
        {
            if (!Exists(gridPosition)) {
                return false;
            }
        }

        return true;
    }

    public bool Exists(GridPosition gridPosition)
    {
        return parent.IsValidGridPosition(gridPosition);
    }

    public GridCell At(GridPosition gridPosition)
    {
        return this.array[gridPosition.x, gridPosition.y, gridPosition.z];
    }

    public Dictionary<Neighbor, GridCellCollection> GetAllNeighbors() {
        return this.GetAllGridCells().GetNeighbors();
    }

    public GridCellCollection At(GridPositionCollection gridPositions)
    {
        GridCellCollection gridCells = new GridCellCollection();
        foreach (GridPosition gridPosition in gridPositions)
        {
            gridCells.Add(this.At(gridPosition));
        }
        return gridCells;
    }

}




