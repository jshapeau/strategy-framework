using System.Collections.Generic;
using System;

public class GridCell
{
    public GridPosition GridPosition { get; private set; }
    public List<GridObjectEntity> Entities = new List<GridObjectEntity>();
    public Dictionary<Neighbor, GridCell> Neighbors { get; set; } = new Dictionary<Neighbor, GridCell>();

    public Func<GridObjectEntity> onEntityAdded;
    public Func<GridObjectEntity> onEntityRemoved;

    public GridCell(int x, int y, int z, ILevelGrid parent)
    {
        this.GridPosition = new GridPosition(x, y, z);
    }

    public List<T> GetComponentsOfType<T>() where T : IGridObjectComponent
    {
        foreach (GridObjectEntity entity in this.Entities)
        {
            return entity.GetComponentsOfType<T>();
        }
        return default(List<T>);
    }

    public T GetFirstComponentOfType<T>() where T : class, IGridObjectComponent
    {
        foreach (GridObjectEntity entity in this.Entities)
        {
            T result = entity.GetFirstComponentOfType<T>();
            if (result != null)
            {
                return result;
            }
        }
        return default(T);
    }

    public override string ToString() 
    {
        string unitString = "";
        foreach (GridObjectEntity entity in this.Entities)
        {
            unitString += entity.Name + "\n";
        }
        return $"{GridPosition.ToString(true)} {unitString}";
    }
}
