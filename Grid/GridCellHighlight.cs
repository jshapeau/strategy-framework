using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCellHighlight : MonoBehaviour
{
    private void OnEnable() {
        
    }

    //NEEDS FIX.
    public void Select(GameObject obj) {
        this.gameObject.SetActive(true);
    }

    public void Select()
    {
        this.gameObject.SetActive(true);
    }

    public void Deselect()
    {
        this.gameObject.SetActive(false);
    }

}
