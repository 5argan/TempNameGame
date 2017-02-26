using InsigneVictoriae.TileEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TempNameGame.TileEngine;

namespace InsigneVictoriae.Characters
{
    public interface ICharacter
    {
        AnimatedSprite Sprite { get; }
        Vector2 Position { get; }
        int MovementRange { get; }

        bool MoveToTile(int x, int y);
    }
}