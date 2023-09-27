using System.Collections.Generic;
using GameState;
using UnityEngine;

namespace Controls {

/// <summary>
/// Control scheme for Combat game states, or any child state.
/// </summary>
public class Combat : BaseControlScheme
{
    private ICombatGameState state;

    public Combat(ICombatGameState state) 
        : base() 
    {
        this.state = state;
    }

    public override void Cancel()
    {
        state.ReturnToPreviousState();
    }

    public override void Select()
    {
        state.Selector.Select();
    }

    public override void Up() 
    {
        state.Selector.Up();
    }

    public override void Down() 
    {
        state.Selector.Down();
    }

    public override void Left() 
    {
        state.Selector.Left();
    }

    public override void Right() 
    {
        state.Selector.Right();
    }

}

}