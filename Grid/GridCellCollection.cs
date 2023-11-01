using UnityEngine;
using System.Collections.Generic;
using System.Collections.ObjectModel;

public class GridCellCollection : Collection<GridCell>
{
    private IGridCellGraph parent;   
    
    public void SetParent(IGridCellGraph parent)
    {
        Debug.Log("Test");
        this.parent = parent;
    }

    public GridCellCollection() {}

    public void AddEntity(GridObjectEntity entity)
    {
        foreach (GridCell gridCell in this.Items)
        {
            gridCell.Entities.Add(entity);
        }
    }

    public void RemoveEntity(GridObjectEntity entity)
    {
        foreach (GridCell gridCell in this.Items)
        {
            gridCell.Entities.Remove(entity);
        }
    }

    public GridCellCollection(List<GridCell> gridCells)
        : base(gridCells)
    {
    }

    public GridCellCollection(GridCell gridCell)
        : base(new List<GridCell>() {gridCell})
    {
        
    }

    public GridCellCollection(GridPositionCollection gridPositions)
        : base()
    {
        foreach (GridPosition gridPosition in gridPositions)
        {
            if (this.parent.At(gridPosition) != null)
            {
                base.Add(this.parent.At(gridPosition));
            }
        }        
    }

    public GridPositionCollection GetGridPositions()
    {
        GridPositionCollection gridPositions = new GridPositionCollection();

        foreach (GridCell gridCell in this.Items)
        {
            gridPositions.Add(gridCell.gridPosition);
        }

        return gridPositions;
    }

    #nullable enable
    public GridCellCollection? GetAllNeighborsOrNull(Neighbor neighbor) {
        GridCellCollection neighbors = new GridCellCollection();
        
        foreach (GridCell gridCell in this.Items) {
            if (!gridCell.Neighbors.ContainsKey(neighbor)) {
                return null;
            } 

            neighbors.Add(gridCell.Neighbors[neighbor]);
        }

        return neighbors;
    }
    #nullable disable

    public Dictionary<Neighbor, GridCellCollection> GetNeighbors()
    {
        Dictionary<Neighbor, GridCellCollection> neighborsDictionary = new Dictionary<Neighbor, GridCellCollection>();

        foreach (Neighbor neighbor in System.Enum.GetValues(typeof(Neighbor)))
        {
            GridCellCollection tempNeighbors = new GridCellCollection();
            
            foreach (GridCell gridCell in this.Items)
            {
                if(gridCell.Neighbors.ContainsKey(neighbor))
                {
                    tempNeighbors.Add(gridCell.Neighbors[neighbor]);
                }
            }
    
            if (tempNeighbors.Count > 0)
            {
                neighborsDictionary[neighbor] = tempNeighbors;
            }
            
        }

        return neighborsDictionary;
    }

}