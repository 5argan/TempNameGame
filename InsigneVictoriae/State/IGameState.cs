using Microsoft.Xna.Framework;

namespace InsigneVictoriae.State
{
    public interface IGameState
    {
        GameState Tag { get; }
        PlayerIndex? CurrentPlayerIndex { get; set; }
    }
}