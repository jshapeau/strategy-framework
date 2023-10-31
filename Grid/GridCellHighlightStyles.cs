using UnityEngine;

/// <summary>
/// Provides styling information for GridCellHighlights
/// </summary>
public class GridCellHighlightStyles
{
    public GridCellHighlightStyle Select = new GridCellHighlightStyle( new Color(0f, 0.7f, 0f, 0.7f) );
    public GridCellHighlightStyle Preview = new GridCellHighlightStyle( new Color(0f, 0.7f, 0.7f, 0.7f) );
    public GridCellHighlightStyle Invalid = new GridCellHighlightStyle( new Color(0.7f, 0f, 0f, 0.7f) );
}