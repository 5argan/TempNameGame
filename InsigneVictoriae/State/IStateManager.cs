using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TempNameGame.State
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
