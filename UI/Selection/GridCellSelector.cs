using System;
using System.Collections.Generic;

/// <summary>
/// Selects GridCells using neighbors based on an initial selection.
/// </summary>
public class GridCellSelector : IGridCellSelector
{
    ///PUBLIC//
    public GridCellCollection Selection {get; private set;}
    public event Action<GridCellCollection> OnSelect;
    
    //PRIVATE//
    private IValidator<GridCellCollection> validator { get; }
    private IGridCellSelectionVisual selectionVisual;
    private GridCellHighlightStyles styles;

    public GridCellSelector(IGridCellSelectionVisual selectionVisual, 
        IValidator<GridCellCollection> validator, 
        GridCellHighlightStyles styles)
    {
        this.selectionVisual = selectionVisual;
        this.validator = validator;
        this.styles = styles;
    }

    public void Select() 
    {
        this.OnSelect?.Invoke(this.Selection);
    }

    public void Initialize(GridCellCollection initialSelection)
    {
        selectionVisual.Deselect();
        this.Selection = initialSelection;
        selectionVisual.Select(this.Selection.GetGridPositions(), selectionVisual.Styles.Select);
    }

    public void Activate()
    {   
        selectionVisual.Activate();        
    }

    public void Deactivate() 
    {
        selectionVisual.Deactivate(); 
    }

    public void Up() 
    {
        MoveSelection(Neighbor.UpperZ);
    }

    public void Down() 
    {
        MoveSelection(Neighbor.LowerZ);
    }

    public void Left() 
    {
        MoveSelection(Neighbor.LowerX);
    }

    public void Right() 
    {
        MoveSelection(Neighbor.UpperX);
    }

    public List<GridObject> GetGridObjectsInSelection() 
    {
        List<GridObject> gridObjectsInSelection = new List<GridObject>();

        foreach(GridCell gridCell in this.Selection) {
            gridObjectsInSelection.AddRange(gridCell.GetGridObjectList());
        }

        return gridObjectsInSelection;
    }

    public void ShowValidSelections() 
    {
        GridCellCollection validItems = this.validator.GetValidItems();
        this.selectionVisual.Select(validItems.GetGridPositions(), selectionVisual.Styles.Preview);
    }

    private void UpdateSelection(GridCellCollection newSelection, GridCellHighlightStyle style) 
    {
        if (this.validator.Validate(newSelection)) {
            this.Selection = newSelection;
            this.selectionVisual.Select(this.Selection.GetGridPositions(), style);
        }
    }

    public void HideValidSelections()
    {
        Debug.Log("Unimplemented");
    }

    private void MoveSelection(Neighbor neighborName)
    {
        GridCellCollection neighbors = this.Selection.GetAllNeighborsOrNull(neighborName);
        if (neighbors != null) {
            UpdateSelection(neighbors, selectionVisual.Styles.Select);
        }
    }

}