using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Debugging class for constructing LevelGrids.
/// </summary>
public class LevelGridBuilder : ILevelGridBuilder
{
    private ILevelGrid levelGrid;
    public GridCell[,,] gridCells;
    public Dictionary<Neighbor, GridPosition> neighborDirectionMapping;

    public LevelGridBuilder(ILevelGrid levelGrid)
    {
        this.levelGrid = levelGrid;
        GridPosition gridDimensions = levelGrid.GetGridDimensions();
        gridCells = new GridCell[gridDimensions.x, gridDimensions.y, gridDimensions.z];

        neighborDirectionMapping = new Dictionary<Neighbor, GridPosition>();

        this.BuildGridCells();
        this.BuildNeighborMappings();
        this.BuildNeighborCaches();
    }

    public ILevelGrid GetLevelGrid()
    {
        return this.levelGrid;
    }

    public void BuildNeighborMappings()
    {
        neighborDirectionMapping.Add(Neighbor.UpperX, new GridPosition(1,0,0));
        neighborDirectionMapping.Add(Neighbor.LowerX, new GridPosition(-1,0,0));
        neighborDirectionMapping.Add(Neighbor.UpperZ, new GridPosition(0,0,1));
        neighborDirectionMapping.Add(Neighbor.LowerZ, new GridPosition(0,0,-1));
        neighborDirectionMapping.Add(Neighbor.UpperY, new GridPosition(0,1,0));
        neighborDirectionMapping.Add(Neighbor.LowerY, new GridPosition(0,-1,0));
    }

    public void BuildGridCells()
    {
        GridPosition gridDimensions = levelGrid.GetGridDimensions();

        for (int x = 0; x < gridDimensions.x; x++)
        {
            for (int y = 0; y < gridDimensions.y; y++)
            {
                for (int z = 0; z < gridDimensions.z; z++)
                {
                    GridCell gridCell = new GridCell(x, y, z, levelGrid);
                    gridCells[x,y,z] = gridCell;
                }
            }
        }

        levelGrid.Cells = new GridCellGraph(gridCells, levelGrid);
    }

    public void GenerateGridDebugObjects(Transform debugPrefab)
    {
        GridPosition gridDimensions = levelGrid.GetGridDimensions();
        for (int x = 0; x < gridDimensions.x; x++)
        {
            for (int y = 0; y < gridDimensions.y; y++)
            {
                for (int z = 0; z < gridDimensions.z; z++)
                {
                    
                    GenerateGridDebugObjects(debugPrefab, 
                                            (new GridPosition(x, y, z)).ToWorldPosition(),
                                            levelGrid.Cells.At(new GridPosition(x, y, z)));
                }
            }
        }
    }

    public void BuildNeighborCaches()
    {
        GridPosition gridDimensions = levelGrid.GetGridDimensions();

        for (int x = 0; x < gridDimensions.x; x++)
        {
            for (int y = 0; y < gridDimensions.y; y++)
            {
                for (int z = 0; z < gridDimensions.z; z++)
                {
                    CacheGridCellNeighbors(levelGrid.Cells.At(new GridPosition(x,y,z)));
                }
            }
        }
    }

    private void CacheGridCellNeighbors(GridCell gridCell)
    {
        foreach (KeyValuePair<Neighbor, GridPosition> item in neighborDirectionMapping)
        {
            GridPosition potentialNeighborPosition = gridCell.gridPosition + item.Value;
            if (levelGrid.IsValidGridPosition(potentialNeighborPosition))
            {   
                gridCell.Neighbors[item.Key] = levelGrid.Cells.At(potentialNeighborPosition);
            }            
        } 
    }

    private void GenerateGridDebugObjects(Transform debugPrefab, Vector3 position, GridCell gridCell)
    {
        Transform debugObject = GameObject.Instantiate(debugPrefab, position, Quaternion.identity);
        debugObject.GetComponent<GridDebugObject>().gridCell = gridCell;
    }

}
