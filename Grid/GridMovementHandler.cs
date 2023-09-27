using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Processes movement within a grid.
/// </summary>
public class GridMovementHandler : MonoBehaviour
{

    IGridCellGraph gridCellGraph;

    private void Awake() 
    {
        GridObject.onMovementComplete += onEnterGridCell;
        GridObject.onMovementStart += onExitGridCell;
    }

    private void OnDestroy() 
    {
        GridObject.onMovementComplete -= onEnterGridCell;
        GridObject.onMovementStart -= onExitGridCell;
    }

    [Zenject.Inject]
    public void Initialize(IGridCellGraph gridCellGraph)
    {
        this.gridCellGraph = gridCellGraph;
    }

    void Start()
    {
        
    }

    private void onEnterGridCell(MoveGameAction action)
    {
        foreach (GridPosition gridPosition in action.MovementMap.end)
        {
            gridCellGraph.At(gridPosition).AddGridObject(action);
        }
        
    }

    private void onExitGridCell(MoveGameAction action)
    {
        foreach (GridPosition currentPosition in action.MovementMap.start)
        {
            gridCellGraph.At(currentPosition).RemoveGridObject(action);
        }
        
    }
    
}
