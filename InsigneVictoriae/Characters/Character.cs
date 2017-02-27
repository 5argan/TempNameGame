using System;
using System.Diagnostics;
using InsigneVictoriae.TileEngine;
using Microsoft.Xna.Framework;
using TempNameGame.TileEngine;

namespace InsigneVictoriae.Characters
{
    public class Character : ICharacter
    {
        private Vector2 _position;

        public int PlayerId { get; }
        public AnimatedSprite Sprite { get; }
        public Vector2 Position => _position;
        public int MovementRange { get; }
        public int Health { get; }
        public int CurrentHealth { get; set; }
        public int BaseAttack { get; }
        public int BaseDefense { get; }
        public int AttackRange { get; }

        public Character(Vector2 position, AnimatedSprite sprite, int movementRange, int playerId)
        {
            _position = position;
            Sprite = sprite;
            MovementRange = movementRange;
            PlayerId = playerId;
        }

        public bool MoveToTile(int x, int y)
        {
            if (!(Math.Abs(Position.X - x) + Math.Abs(Position.Y - y) <= MovementRange))
                return false;

            _position.X = x;
            _position.Y = y;
            return true;
        }

        /// <summary>
        /// Deal damage to the target character according to attack and defense values.
        /// </summary>
        /// <param name="target">The character being attacked.</param>
        /// <returns>true if the attack was successful, false otherwise</returns>
        public bool Attack(ICharacter target)
        {
            target.CurrentHealth -= BaseAttack - target.BaseDefense;
            
            return true;
        }

        public bool Support(ICharacter target)
        {
            throw new NotImplementedException();
        }

        public bool IsAlive()
            => CurrentHealth > 0;
    }
}