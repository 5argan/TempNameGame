using System;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TempNameGame.AvatarComponents;
using TempNameGame.CharacterComponents;
using TempNameGame.Components;
using TempNameGame.ConversationComponents;
using TempNameGame.PlayerComponents;
using TempNameGame.TileEngine;
using Keys = Microsoft.Xna.Framework.Input.Keys;

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
            
        }

        public override void Update(GameTime gameTime)
        {
            if (InputHandler.CheckMouseReleased(MouseButton.Left))
            {
                var temp = Matrix.Invert(_camera.Transformation);
                var temp2 = new Vector2(InputHandler.MouseState.X, InputHandler.MouseState.Y);
                var clickedPosition = Vector2.Transform(temp2, temp);
                MessageBox.Show($"{(int)(clickedPosition.X / Engine.TileWidth)}, {(int)(clickedPosition.Y / Engine.TileHeight)}");
            }

            var motion = Vector2.Zero;
            const int padding = 8;

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
                motion *= _player.Speed*(float) gameTime.ElapsedGameTime.TotalSeconds;

                var pRect = new Rectangle(
                    (int) _player.Sprite.Position.X + (int) motion.X + padding,
                    (int) _player.Sprite.Position.Y + (int) motion.Y + padding,
                    Engine.TileWidth - padding,
                    Engine.TileHeight - padding);

                foreach (var key in _map.Characters.Keys)
                {
                    var r = new Rectangle(
                        _map.Characters[key].X*Engine.TileWidth + padding,
                        _map.Characters[key].Y*Engine.TileHeight + padding,
                        Engine.TileWidth - padding,
                        Engine.TileHeight - padding);

                    if (!pRect.Intersects(r)) continue;
                    motion = Vector2.Zero;
                    break;
                }

                var newPosition = _player.Sprite.Position + motion;

                _player.Sprite.Position = newPosition;
                _player.Sprite.IsAnimating = true;
                _player.Sprite.LockToMap(new Point(_map.WidthInPixels, _map.HeightInPixels));
            }
            else
            {
                _player.Sprite.IsAnimating = false;
            }

            _camera.LockToSprite(_map, _player.Sprite, Game1.ScreenRectangle);
            _player.Sprite.Update(gameTime);

            if (InputHandler.CheckKeyReleased(Keys.Space) || InputHandler.CheckKeyReleased(Keys.Enter))
            {
                foreach (var key in _map.Characters.Keys)
                {
                    var c = CharacterManager.Instance.GetCharacter(key);
                    var distance = Vector2.Distance(_player.Sprite.Center, c.Sprite.Center);

                    if (!(Math.Abs(distance) < 72f)) continue;

                    var conversationState =
                        (IConversationState) _game.Services.GetService(typeof(IConversationState));
                    _manager.PushState((ConversationState) conversationState, _currentPlayerIndex);
                    conversationState.SetConversation(_player, c);
                    conversationState.StartConversation();
                }
            }


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
            var spriteSheet = _content.Load<Texture2D>(@"PlayerSprites\maleplayer");
            _player = new Player(_game, "Wesley", false, spriteSheet);
            _player.AddAvatar("fire", AvatarManager.GetAvatar("fire"));
            _player.SetAvatar("fire");

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

            ConversationManager.CreateConversations(_game);

            var teacherOne = Character.FromString(_game, "Lance,teacherone,WalkDown,teacherone,dark");
            var teacherTwo = PCharacter.FromString(_game, "Marissa,teachertwo,WalkDown,teachertwo,light");

            teacherOne.SetConversation("LanceHello");
            teacherTwo.SetConversation("MarissaHello");

            _game.CharacterManager.AddCharacter("teacherone", teacherOne);
            _game.CharacterManager.AddCharacter("teachertwo", teacherTwo);

            _map.Characters.Add("teacherone", new Point(0,4));
            _map.Characters.Add("teachertwo", new Point(4, 0));

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