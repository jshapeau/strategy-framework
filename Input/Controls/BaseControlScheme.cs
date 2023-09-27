using UnityEngine;
using System;
using System.Collections.Generic;

namespace Controls {

/// <summary>
/// Provides default implementation for control schemes.
/// </summary>
public abstract class BaseControlScheme : IControlScheme
{
    public abstract GameState.ControlType controlType { get; }
    public GameState.GameState State { 
            get
            {
                Debug.Log("ERROR: BaseControlScheme is for debugging only, and cannot have a state.");
                return null;
            } 
            private set {}
        }

    public BaseControlScheme() {}

    public virtual void OnActivate() {}

    public virtual void OnDeactivate() {}

    public void Activate() {}

    public virtual void Up()
    {
        Debug.Log("Up: Unimplemented");
    }

    public virtual void Down()
    {
        Debug.Log("Down: Unimplemented");
    }

    public virtual void Right()
    {
        Debug.Log("Right: Unimplemented");
    }

    public virtual void Left()
    {
        Debug.Log("Left: Unimplemented");
    }

    public virtual void Hover()
    {
        Debug.Log("Hover: Unimplemented");
    }

    public virtual void Select()
    {
        Debug.Log("Select: Unimplemented");
    }

    public virtual void Cancel()
    {
        Debug.Log("Cancel: Unimplemented");
    }
}

}
