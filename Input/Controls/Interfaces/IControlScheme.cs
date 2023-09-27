using System;
using System.Collections.Generic;

public interface IControlScheme
{
    public GameState.GameState State { get; }
    public void OnActivate();
    public void OnDeactivate();

    public void Up();
    public void Down();
    public void Left();
    public void Right();
    public void Hover();
    public void Select();
    public void Cancel();
}
