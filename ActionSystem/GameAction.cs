using System;
using UnityEngine;

public abstract class GameAction<T>
{
    public ActionData data = new ActionData();
    public T target;

    public GameAction()
    {
    }

    public void RegisterOutcome(Action outcome)
    {
        Debug.Log("Unimplemented");
    }
    
    public abstract void Execute(T target);
}
