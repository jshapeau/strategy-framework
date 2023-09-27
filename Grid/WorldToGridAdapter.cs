using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Strategy for converting from world coordinates to GridPosition coordinates. Allows world and grid positions to vary independently.
/// </summary>
public class WorldToGridAdapter : IWorldToGridAdapter
{
    public ILevelGrid Grid {get; set;}

    public WorldToGridAdapter(ILevelGrid grid)
    {
        this.Grid = grid;
    }

    public Vector3 GetAverageWorldPosition(GridPositionCollection gridPositions)
    {
        int count = gridPositions.Count;
        float x = 0f;
        float y = 0f;
        float z = 0f;
        foreach (GridPosition gridPosition in gridPositions)
        {
            Vector3 worldPosition = gridPosition.ToWorldPosition();
            x += worldPosition.x;
            y += worldPosition.y;
            z += worldPosition.z;
        }

        return new Vector3(x / count, y / count, z / count);
    }

    public GridCell WorldPositionToGridCell(Vector3 position){
        GridPosition gridPosition = WorldPositionToGridPosition(position);

        if (Grid.IsValidGridPosition(gridPosition))
        {
            return Grid.Cells.At(gridPosition);
        }
        return null;
    }

    public GridPosition WorldPositionToGridPosition(Vector3 position)
    {
        Vector3 gridScale = Grid.GetGridScale();
        
        GridPosition gridPosition = new GridPosition(
            Mathf.RoundToInt(position.x / gridScale.x),
            Mathf.RoundToInt(position.y / gridScale.y),
            Mathf.RoundToInt(position.z / gridScale.z)
        );

        return Grid.SanitizeGridPosition(gridPosition);
    }

    public GridPositionCollection WorldPositionToGridPosition(List<Vector3> positions)
    {
        GridPositionCollection gridPositions = new GridPositionCollection();
        Vector3 gridScale = Grid.GetGridScale();

        foreach (Vector3 position in positions)
        {
            gridPositions.Add( new GridPosition(
                Mathf.RoundToInt(position.x / gridScale.x),
                Mathf.RoundToInt(position.y / gridScale.y),
                Mathf.RoundToInt(position.z / gridScale.z))
            );
        }

        return Grid.SanitizeGridPosition(gridPositions);
    }

    public Vector3 GridPositionToWorldPosition(GridPosition gridPosition)
    {
        return gridPosition.ToWorldPosition();
    }

    private GridPosition SanitizeGridPosition(GridPosition gridPosition)
    {
        if(Grid.IsValidGridPosition(gridPosition))
        {
            return gridPosition;
        } 
        else
        {
            GridPosition gridDimensions = Grid.GetGridDimensions();
            return new GridPosition(Mathf.Clamp(gridPosition.x, 0, gridDimensions.x-1),
                                    Mathf.Clamp(gridPosition.y, 0, gridDimensions.y-1),
                                    Mathf.Clamp(gridPosition.z, 0, gridDimensions.z-1));
        }
    }

}
