using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TempNameGame.AvatarComponents;
using TempNameGame.TileEngine;

namespace TempNameGame.CharacterComponents
{
    public class PCharacter : ICharacter
    {
        public const float SPEAKING_RADIUS = 40f;
        public const int AVATAR_LIMIT = 6;

        private static Game1 _game;
        private static readonly Dictionary<AnimationKey, Animation> _characterAnimations = new Dictionary<AnimationKey, Animation>();

        private readonly Avatar[] _avatars = new Avatar[AVATAR_LIMIT];
        private int _currentAvatar;

        public string Name { get; private set; }
        public AnimatedSprite Sprite { get; private set; }
        public Avatar BattleAvatar => _avatars[_currentAvatar];
        public Avatar GiveAvatar { get; }

        public string Conversation { get; private set; }

        private PCharacter()
        {

        }

        private static void BuildAnimations()
        {
            throw new NotImplementedException();
        }

        public static PCharacter FromString(Game game, string characterString)
        {
            if (_game == null)
                _game = (Game1)game;

            if (_characterAnimations.Count == 0)
                BuildAnimations();

            var character = new PCharacter();
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

        public void ChangeAvatar(int index)
        {
            if (index < AVATAR_LIMIT && index >= 0)
                _currentAvatar = index;
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