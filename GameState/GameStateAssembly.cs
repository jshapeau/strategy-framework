using System.Collections.Generic;
using System;
using UnityEngine;

namespace GameState {

/// <summary>
/// Collection of linked GameState and GameStateControlScheme
/// </summary>
public class GameStateAssembly 
{
    public IGameState GameState { get; private set; }
    public Dictionary<ControlType, IControlScheme> ControlSchemes { get; private set; }

    public event Action OnStateEntered
    {
        add
        {
            this.GameState.OnStateEntered += value;
        }
        remove
        {
            this.GameState.OnStateEntered -= value;
        }
    }

    public GameStateAssembly(IGameState gameState, IEnumerable<IControlScheme> controlSchemes, GameStateRepository controller) 
    {
        this.GameState = gameState;
        this.ControlSchemes = new Dictionary<ControlType, IControlScheme>();
        foreach (IControlScheme controlScheme in controlSchemes)
        {
            this.ControlSchemes.Add(controlScheme.controlType, controlScheme);
        }

        controller.RegisterState(this);
    }
}

}