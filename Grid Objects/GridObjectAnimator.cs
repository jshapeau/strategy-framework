using System.Collections.Generic;
using UnityEngine;

public class GridObjectAnimator : MonoBehaviour, IGridObjectAnimator
{
    public IWorldToGridAdapter WorldToGridAdapter {get; private set;}
    private List<Vector3> targetWorldPositions = new List<Vector3>();  
    
    private float movementSpeed = 8f;
    private float rotationSpeed = 10f;
    private float stoppingDistance = 0.1f;

    public void Initialize(IWorldToGridAdapter worldToGridAdapter)
    {
        this.WorldToGridAdapter = worldToGridAdapter;
        this.targetWorldPositions = new List<Vector3>();
    }

    public void AnimateMovement(GridPosition targetGridPosition)
    {
        this.AnimateMovement(new GridPositionCollection{targetGridPosition});
    }

    public void AnimateMovement(GridPositionCollection targetGridPositions)
    {
        Vector3 averageTargetPosition = WorldToGridAdapter.GetAverageWorldPosition(targetGridPositions);

        this.targetWorldPositions.Add(averageTargetPosition);
        Debug.Log("Animation Complete");
    }

    private void Update() 
    {
        if (targetWorldPositions.Count == 0)
        {   
            return;
        }

        if (Vector3.Distance(transform.position, targetWorldPositions[0]) > stoppingDistance)
        {
            Vector3 moveDirection = (targetWorldPositions[0] - transform.position).normalized;
            transform.position += moveDirection * movementSpeed * Time.deltaTime;   
            transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);
        } else {
            targetWorldPositions.RemoveAt(0);
        }
    }
}

