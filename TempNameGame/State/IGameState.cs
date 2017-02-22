using Microsoft.Xna.Framework;

namespace TempNameGame.State
{
    public interface IGameState
    {
        GameState Tag { get; }
        PlayerIndex? CurrentPlayerIndex { get; set; }
    }
}