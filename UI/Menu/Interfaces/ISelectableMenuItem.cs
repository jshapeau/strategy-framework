using System;
using UnityEngine;

public interface ISelectableMenuItem 
{
    public Action Select { get; }
    public Transform GetTransform();
}
