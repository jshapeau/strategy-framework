using System;
using System.Collections.Generic;

public interface IGridCellSelector : ITypedDirectionalSelector<GridCellCollection>
{
    public List<GridObject> GetGridObjectsInSelection();    
}