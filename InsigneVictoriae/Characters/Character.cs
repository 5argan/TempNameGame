using System;
using InsigneVictoriae.TileEngine;
using Microsoft.Xna.Framework;
using TempNameGame.TileEngine;

namespace InsigneVictoriae.Characters
{
    public class Character : ICharacter
    {
        private Vector2 _position;

        public AnimatedSprite Sprite { get; }
        public Vector2 Position => _position;
        public int MovementRange { get; }

        public Character(Vector2 position, AnimatedSprite sprite, int movementRange)
        {
            _position = position;
            Sprite = sprite;
            MovementRange = movementRange;
        }

        public bool MoveToTile(int x, int y)
        {
            if (!(Math.Abs(Position.X - x) + Math.Abs(Position.Y - y) <= MovementRange))
                return false;

            _position.X = x;
            _position.Y = y;
            return true;
        }
    }
}