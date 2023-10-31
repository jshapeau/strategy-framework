using UnityEngine;

/// <summary>
/// Represents a single style for a GridCellHighlight
/// </summary>
public class GridCellHighlightStyle : IHighlightStyle
{
    public Color Color { get; private set; }
    
    public GridCellHighlightStyle(Color color)
    {
        this.Color = color;
    }
}