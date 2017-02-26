using Microsoft.Xna.Framework;
using InsigneVictoriae.TileEngine;
using TempNameGame.TileEngine;

namespace InsigneVictoriae.Characters
{
    public class AiCharacter : Character
    {
        public AiCharacter(Vector2 position, AnimatedSprite sprite, int movementRange) : base(position, sprite, movementRange)
        {
        }
    }
}