using UnityEngine;

/// <summary>
/// Represents an organized collection of GridCells in space. 
/// </summary>
public class LevelGrid : MonoBehaviour, ILevelGrid
{
    [SerializeField] public IntReference gridWidth;
    [SerializeField] public IntReference gridHeight;
    [SerializeField] public IntReference gridLength;

    [SerializeField] private float xScale;
    [SerializeField] private float yScale;
    [SerializeField] private float zScale;

    public IGridCellGraph Cells { get; set; }

    private void Awake()
    {
        xScale = GridPosition.xScale;
        yScale = GridPosition.yScale;
        zScale = GridPosition.zScale;
    }

    public Vector3 GetGridScale()
    {
        return new Vector3(xScale, yScale, zScale);
    }

    public GridPosition GetGridDimensions()
    {
        return new GridPosition(gridWidth, gridHeight, gridLength);
    }

    /// <summary>
    /// Clamps a grid position to the nearest valid position on the grid.
    /// </summary>
    /// <param name="gridPosition"></param>
    /// <returns></returns>
    public GridPosition SanitizeGridPosition(GridPosition gridPosition)
    {
        if (IsValidGridPosition(gridPosition))
        {
            return gridPosition;
        }
        else
        {
            return new GridPosition(Mathf.Clamp(gridPosition.x, 0, gridWidth - 1),
                                    Mathf.Clamp(gridPosition.y, 0, gridHeight - 1),
                                    Mathf.Clamp(gridPosition.z, 0, gridLength - 1));
        }
    }

    /// <summary>
    /// Clamps a GridPositionCollection to the nearest valid positions on this LevelGrid such that the shape of the GridPositionCollection is preserved.
    /// </summary>
    /// <param name="gridPositions"></param>
    /// <returns></returns>
    public GridPositionCollection SanitizeGridPosition(GridPositionCollection gridPositions)
    {
        bool allValid = true;
        foreach (GridPosition gridPosition in gridPositions)
        {
            if (!IsValidGridPosition(gridPosition))
            {
                allValid = false;
                break;
            }
        }

        if (allValid)
        {
            return gridPositions;
        }

        GridPositionCollection results = new GridPositionCollection();
        GridPositionCollection bounds = gridPositions.GetBounds();

        //Lower Bounds
        int x_offset = bounds[0].x < 0 ? bounds[0].x : 0;
        int y_offset = bounds[0].y < 0 ? bounds[0].y : 0;
        int z_offset = bounds[0].z < 0 ? bounds[0].z : 0;

        //Upper Bounds
        x_offset = bounds[1].x >= gridWidth ? bounds[1].x - gridWidth + 1 : x_offset;
        y_offset = bounds[1].y >= gridHeight ? bounds[1].y - gridHeight + 1 : y_offset;
        z_offset = bounds[1].z >= gridLength ? bounds[1].z - gridLength + 1 : z_offset;

        foreach (GridPosition gridPosition in gridPositions)
        {
            GridPosition result = gridPosition - new GridPosition(x_offset, y_offset, z_offset);
            results.Add(result);
        }

        return results;
    }

    public bool IsValidGridPosition(GridPosition gridPosition)
    {
        return gridPosition.x >= 0 &&
                gridPosition.y >= 0 &&
                gridPosition.z >= 0 &&
                gridPosition.x < gridWidth &&
                gridPosition.y < gridHeight &&
                gridPosition.z < gridLength;
    }
}