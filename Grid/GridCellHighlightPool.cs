using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Provides a performant, dynamic support for bulk operations on GridCellHighlights.
/// </summary>
public class GridCellHighlightPool : IGridCellSelectionVisual
{
    private int poolSize = 4;
    private Stack<GridCellHighlight> objectPool;
    public Dictionary<GridPosition, GridCellHighlight> ActiveObjectPool {get; private set;}
    private GridCellHighlight gridCellHighlightPrefab;
    private Dictionary<IHighlightStyle, List<GridCellHighlight>> ActiveObjects;
    public GridCellHighlightStyles Styles { get; private set; }

    public GridCellHighlightPool(GridCellHighlight gridCellHighlightPrefab, 
        GridCellHighlightStyles styles)
    {
        this.gridCellHighlightPrefab = gridCellHighlightPrefab;
        this.Styles = styles;
        objectPool = new Stack<GridCellHighlight>();
        ActiveObjectPool = new Dictionary<GridPosition, GridCellHighlight>();
        this.ActiveObjects = new Dictionary<IHighlightStyle, List<GridCellHighlight>>();
        
        this.Initialize(); //Needs refactoring.
    }

    private void OnSelect() {

    }

    private void OnDeselect() {
        
    }

    public void Activate()
    {
        foreach (List<GridCellHighlight> highlightList in ActiveObjects.Values)
        {
            foreach (GridCellHighlight highlight in highlightList)
            {
                highlight.Select();
            }
        }
    }

    public void Deactivate()
    {
        foreach (List<GridCellHighlight> highlightList in ActiveObjects.Values)
        {
            foreach (GridCellHighlight highlight in highlightList)
            {
                highlight.Deselect();
            }
        }
    }

    public void Initialize()
    {
        Debug.Log("Initializing GridCellHighlightPool");
        for (int n = 0; n < poolSize; n++)
        {
            Transform gridCellHighlight = GameObject.Instantiate(gridCellHighlightPrefab.transform, new Vector3(0,0,0), Quaternion.identity);
            gridCellHighlight.gameObject.SetActive(false);
            
            objectPool.Push(gridCellHighlight.GetComponent<GridCellHighlight>());
        }
    }
    
    public void Deselect()
    {
        this.Deactivate();
        ActiveObjects.Clear();
    }

    public void Deselect(IHighlightStyle style)
    {
        if (!ActiveObjects.ContainsKey(style))
        {
            return;
        }

        foreach (GridCellHighlight highlight in ActiveObjects[style])
        {
            highlight.Deselect();
            objectPool.Push(highlight);
        }

        ActiveObjects.Remove(style);
    }

    public void DeselectAll()
    {
        foreach (GridCellHighlightStyle style in ActiveObjects.Keys)
        {
            this.Deselect(style);
        }
    }

    public void DeselectAt(GridPositionCollection gridPositions)
    {
        Debug.Log("DeselectAt Unimplemented");
    }

    private void ExpandPool(int amount)
    {
        for (int n = 0; n < amount; n++)
        {
            Transform gridCellHighlight = GameObject.Instantiate(gridCellHighlightPrefab.transform, new Vector3(0,0,0), Quaternion.identity);
            // gridCellHighlight.gameObject.SetActive(false);
            objectPool.Push(gridCellHighlight.GetComponent<GridCellHighlight>());
        }
    }

    public void Select(GridPositionCollection positionList, IHighlightStyle style)
    {
        this.Deselect(style);

        int poolSizeShortageAmount = positionList.Count - objectPool.Count;
        if (poolSizeShortageAmount > 0)
        {
            ExpandPool(poolSizeShortageAmount);
        }
        
        List<GridCellHighlight> highlights = new List<GridCellHighlight>();
        
        for (int n=0; n < positionList.Count; n++)
        {   
            GridCellHighlight highlight = objectPool.Pop();
            highlight.transform.position = positionList[n].ToWorldPosition();
            highlight.gameObject.SetActive(true);
            highlights.Add(highlight);
        }

        ActiveObjects[style] = highlights;
    }

    public void Select(HashSet<GridPosition> positionList)
    {
        GridPositionCollection gridPositionCollection = new GridPositionCollection(positionList.ToList<GridPosition>());
        Select(gridPositionCollection, Styles.Select);
    }

}
