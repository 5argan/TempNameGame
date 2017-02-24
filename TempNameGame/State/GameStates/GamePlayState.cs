using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TempNameGame.Components;
using TempNameGame.TileEngine;

namespace TempNameGame.State.GameStates
{
    public interface IGamePlayState
    {
        void SetUpNewGame();
        void LoadExistingGame();
        void StartGame();
    }
    public class GamePlayState : GameStateBase, IGamePlayState
    {
        Engine _engine = new Engine(Game1.ScreenRectangle, 64, 64);
        private TileMap _map;
        private Camera _camera;
        private Player _player;
        public GamePlayState(Game game) : base(game)
        {
            game.Services.AddService(typeof(IGamePlayState), this);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            var spriteSheet = _content.Load<Texture2D>(@"PlayerSprites\maleplayer");
            _player = new Player(_game, "Wesley", false, spriteSheet);
        }

        public override void Update(GameTime gameTime)
        {
            var motion = Vector2.Zero;

            if (InputHandler.KeyboardState.IsKeyDown(Keys.A))
            {
                motion.X -= 1;
                _player.Sprite.CurrentAnimation = AnimationKey.WalkLeft;
            }
            if (InputHandler.KeyboardState.IsKeyDown(Keys.D))
            {
                motion.X += 1;
                _player.Sprite.CurrentAnimation = AnimationKey.WalkRight;
            }
            if (InputHandler.KeyboardState.IsKeyDown(Keys.W))
            {
                motion.Y -= 1;
                _player.Sprite.CurrentAnimation = AnimationKey.WalkUp;
            }
            if (InputHandler.KeyboardState.IsKeyDown(Keys.S))
            {
                motion.Y += 1;
                _player.Sprite.CurrentAnimation = AnimationKey.WalkDown;
            }


            if (motion != Vector2.Zero)
            {
                motion.Normalize();
                motion *= (_player.Speed*(float) gameTime.ElapsedGameTime.TotalSeconds);
                var newPosition = _player.Sprite.Position + motion;

                _player.Sprite.Position = newPosition;
                _player.Sprite.IsAnimating = true;
                _player.Sprite.LockToMap(new Point(_map.WidthInPixels, _map.HeightInPixels));
            }

            _camera.LockToSprite(_map, _player.Sprite, Game1.ScreenRectangle);
            _player.Sprite.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            if (_map != null && _camera != null)
                _map.Draw(gameTime, _game.SpriteBatch, _camera);

            _game.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, _camera.Transformation);
            _player.Sprite.Draw(gameTime, _game.SpriteBatch);
            _game.SpriteBatch.End();
        }

        public void SetUpNewGame()
        {
            var tiles = _game.Content.Load<Texture2D>(@"Tiles\tileset1");
            var set = new TileSet(8, 8, 32, 32) {Texture = tiles};

            var background = new TileLayer(200, 200);
            var edge = new TileLayer(200, 200);
            var building = new TileLayer(200, 200);
            var decor = new TileLayer(200, 200);

            _map = new TileMap(set, background, edge, building, decor, "test-map");
            _map.FillEdges();
            _map.FillBuilding();
            _map.FillDecoration();

            _camera = new Camera();
        }

        public void LoadExistingGame()
        {
            throw new System.NotImplementedException();
        }

        public void StartGame()
        {
        }
    }
}