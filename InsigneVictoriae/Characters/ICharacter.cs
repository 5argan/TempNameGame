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
        int Health { get; }
        int CurrentHealth { get; set; }
        int BaseAttack { get; }
        int BaseDefense { get; }
        int AttackRange { get; }

        bool MoveToTile(int x, int y);
        void Attack(ICharacter target);
        bool IsAlive();
    }
}