using System.Collections.Generic;
using System;

namespace GameState {



public class ServiceLocator {
    Dictionary<Type, GameStateAssembly> gameStateAssemblies;

    public ServiceLocator() 
    {
        this.gameStateAssemblies = new Dictionary<Type, GameStateAssembly>();
    }

    public void Register(GameStateAssembly instance) 
    {
        gameStateAssemblies.Add(instance.GameState.GetType(), instance);
    }

    

    public T GetGameState<T>() where T : IGameState 
    {
        return (T)this.gameStateAssemblies[typeof(T)].GameState;
    }

    public GameStateAssembly GetGameStateAssembly<T>() where T : IGameState 
    {
        return this.gameStateAssemblies[typeof(T)];
    }
}

}