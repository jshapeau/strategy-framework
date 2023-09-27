using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GridDebugObject : MonoBehaviour
{
    [SerializeField] private TextMeshPro text;
    public GridCell gridCell;

    void Update()
    {
        text.text = gridCell.ToString();
    }
}
