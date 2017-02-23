using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TempNameGame.TileEngine;

namespace TempNameGame
{
    public class Player : DrawableGameComponent
    {
        private Game1 _game;
        private string _name;
        private bool _gender;
        private string _mapName;
        private Point _tile;
        private readonly AnimatedSprite _sprite;
        private Texture2D _texture;
        private Vector2 _position;

        public Vector2 Position
        {
            get { return _sprite.Position; }
            set { _sprite.Position = value; }
        }

        public float Speed { get; set; } = 180f;

        public AnimatedSprite Sprite => _sprite;

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