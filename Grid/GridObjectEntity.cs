using System.Collections.Generic;

public class GridObjectEntity
{
    private List<IGridObjectComponent> Components;

    public GridObjectEntity(List<IGridObjectComponent> initialComponents)
    {
        this.Add(initialComponents);
    }
    public GridObjectEntity(IGridObjectComponent initialComponent)
    {
        this.Add(initialComponent);
    }

    public void Add(List<IGridObjectComponent> components)
    {
        this.Components.AddRange(components);
        foreach (IGridObjectComponent component in components)
        {
            component.Entity = this;
        }
    }

    public void Add(IGridObjectComponent component)
    {
        this.Components.Add(component);
        component.Entity = this;
    }

    public void Remove(IGridObjectComponent component)
    {
        this.Components.Remove(component);
        component.Entity = null;
    }

    public List<T> GetObjectsOfType<T>() where T : IGridObjectComponent
    {
        List<T> result = new List<T>();
        foreach (IGridObjectComponent entity in this.Components)
        {
            if (entity is T)
            {
                result.Add((T)entity);
            }
        }
        return result;
    }
}
