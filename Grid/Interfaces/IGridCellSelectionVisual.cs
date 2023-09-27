public interface IGridCellSelectionVisual : ISelectionVisual<GridPositionCollection>
{
    public GridCellHighlightStyles Styles { get; }
    public void DeselectAt(GridPositionCollection selection);
}