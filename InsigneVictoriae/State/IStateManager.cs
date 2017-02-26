using System;
using Microsoft.Xna.Framework;
using TempNameGame.State;

namespace InsigneVictoriae.State
{
    public interface IStateManager
    {
        IGameState CurrentState { get; }
        event EventHandler StateChanged;

        void PushState(GameState state, PlayerIndex? index);
        void ChangeState(GameState state, PlayerIndex? index);
        void PopState();
        bool ContainsState(GameState state);
    }
    
}
