using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TempNameGame.AvatarComponents;
using TempNameGame.TileEngine;

namespace TempNameGame.PlayerComponents
{
    public class Player : DrawableGameComponent
    {
        protected Game1 _game;
        protected string _name;
        protected bool _gender;
        protected string _mapName;
        protected Point _tile;
        protected readonly AnimatedSprite _sprite;
        protected Texture2D _texture;
        protected Vector2 _position;

        protected Dictionary<string, Avatar> _avatars = new Dictionary<string, Avatar>();
        private string _currentAvatar;

        public Vector2 Position
        {
            get { return _sprite.Position; }
            set { _sprite.Position = value; }
        }

        public float Speed { get; set; } = 180f;

        public AnimatedSprite Sprite => _sprite;

        public virtual Avatar CurrentAvatar =>
            _avatars[_currentAvatar];

        private Player(Game game) : base(game)
        {
        }

        public Player(Game game, string name, bool gender, Texture2D texture) : base(game)
        {
            _game = (Game1) game;
            _name = name;
            _gender = gender;
            _texture = texture;
            _sprite = new AnimatedSprite(texture, _game.playerAnimations)
            {
                CurrentAnimation = AnimationKey.WalkDown
            };
        }

        public virtual void AddAvatar(string avatarName, Avatar avatar)
        {
            if (!_avatars.ContainsKey(avatarName))
            {
                _avatars.Add(avatarName, avatar);
            }
        }

        public virtual Avatar GetAvatar(string avatarName) =>
            _avatars.ContainsKey(avatarName) ? _avatars[avatarName] : null;

        public virtual void SetAvatar(string avatarName)
        {
            if (_avatars.ContainsKey(avatarName))
                _currentAvatar = avatarName;
            else
                throw new IndexOutOfRangeException();
        }

        public void SavePlayer()
        {
            
        }

        public static Player Load(Game game)
        {
            return new Player(game);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            _sprite.Draw(gameTime, _game.SpriteBatch);
        }
    }
}