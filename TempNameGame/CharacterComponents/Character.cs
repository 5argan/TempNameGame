using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TempNameGame.AvatarComponents;
using TempNameGame.TileEngine;

namespace TempNameGame.CharacterComponents
{
    public class Character : ICharacter
    {
        public const float SPEAKING_RADIUS = 40f;

        private static Game1 _game;
        private static readonly Dictionary<AnimationKey, Animation> _characterAnimations = new Dictionary<AnimationKey, Animation>();

        public string Name { get; private set; }
        public AnimatedSprite Sprite { get; private set; }
        public Avatar BattleAvatar { get; }
        public Avatar GiveAvatar { get; }

        public string Conversation { get; private set; }

        private Character()
        {
            
        }

        private static void BuildAnimations()
        {
            throw new NotImplementedException();
        }

        public static Character FromString(Game game, string characterString)
        {
            if (_game == null)
                _game = (Game1)game;

            if (_characterAnimations.Count == 0)
                BuildAnimations();

            var character = new Character();
            var parts = characterString.Split(',');
            character.Name = parts[0];
            var texture = game.Content.Load<Texture2D>(@"CharacterSprites\" + parts[1]);
            character.Sprite = new AnimatedSprite(texture, _game.playerAnimations);
            AnimationKey key;
            if (!Enum.TryParse(parts[2], true, out key))
                key = AnimationKey.WalkDown;
            character.Sprite.CurrentAnimation = key;
            character.Conversation = parts[3];

            return character;
        }
        public void SetConversation(string newConversation)
        {
            Conversation = newConversation;
        }

        public static void Save(string characterName)
        {
            throw new NotImplementedException();
        }

        public void Update(GameTime gameTime)
        {
            Sprite.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Sprite.Draw(gameTime, spriteBatch);
        }
    }
}