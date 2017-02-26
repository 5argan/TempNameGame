using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TempNameGame.AvatarComponents;
using TempNameGame.TileEngine;

namespace TempNameGame.CharacterComponents
{
    public interface ICharacter
    {
        string Name { get; }
        string Conversation { get; }
        AnimatedSprite Sprite { get; }
        Avatar BattleAvatar { get; }
        Avatar GiveAvatar { get; }
        void SetConversation(string newConversation);
        void Update(GameTime gameTime);
        void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}