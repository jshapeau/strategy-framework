using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Visual component for highlighting selected Unit
/// </summary>
public class UnitSelectedVisual : MonoBehaviour
{
    [SerializeField] private Vector3 heightOffset;
    private IWorldToGridAdapter worldToGridAdapter;
    
    private void Start() 
    {

    }

    [Zenject.Inject]
    public void Initialize(IWorldToGridAdapter worldToGridAdapter)
    {
        this.worldToGridAdapter = worldToGridAdapter;
    }

    public void Select(GridObject gridObject)
    {   
        Vector3 newWorldPosition = worldToGridAdapter.GetAverageWorldPosition(gridObject.Positions);
        transform.position = newWorldPosition + heightOffset;
        gameObject.SetActive(true);
    }

    public void Deselect()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy() 
    {

    }

}
