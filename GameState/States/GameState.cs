using System;

namespace GameState
{
    /// <summary>
    /// Contains the set of possible interactions and associated business logic available at any point in time.
    /// </summary>
    public abstract class GameState : IGameState
    {
        public abstract GameStateType StateType { get; }
        public string Name { get; private set; }
        public static Action<IGameState> OnStateActivated;
        public Action OnStateEntered { get; set; }
        public IGameState PreviousState { get; protected set; }

        public GameState()
        {
            this.Name = this.GetType().Name;
        }

        public override string ToString()
        {
            return this.Name;
        }

        /// <summary>
        /// Invokes OnStateActivated to activate a game state. Child classes may wish to overload this function to provide state-specific context.
        /// </summary>
        public virtual void Activate()
        {
            // GameState.OnStateActivated += this.OnStateChange;
            OnStateEntered?.Invoke();
        }

        public virtual void OnStateChange(IGameState gameState) { }

        public virtual void ReturnToPreviousState()
        {
            if (this.PreviousState != null)
            {
                this.Deactivate();
                this.PreviousState.Reactivate();
            }
        }

        /// <summary>
        /// Invokes OnStateActivated to activate a game state; accepts no context, and should be used to activate a GameState that already has a valid context.
        /// </summary>
        public virtual void Reactivate()
        {
            Debug.Log("Reactivate Unimplemented");
        }

        public virtual void Deactivate()
        {
            Debug.Log("Deactivate Unimplemented");
        }
    }
}
