using System;
using InsigneVictoriae.TileEngine;
using InsigneVictoriae.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TempNameGame.TileEngine;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace InsigneVictoriae.State.GameStates
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
            }

            var motion = Vector2.Zero;

            /*_camera.LockToSprite(_map, _player.Sprite, Game1.ScreenRectangle);
            _player.Sprite.Update(gameTime);*/

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            if (_map != null && _camera != null)
                _map.Draw(gameTime, _game.SpriteBatch, _camera);

            /*_game.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, _camera.Transformation);
            _player.Sprite.Draw(gameTime, _game.SpriteBatch);
            _game.SpriteBatch.End();*/
        }

        public void SetUpNewGame()
        {
            var spriteSheet = _content.Load<Texture2D>(@"PlayerSprites\maleplayer");
            /*_player = new Player(_game, "Wesley", false, spriteSheet);
            _player.AddAvatar("fire", AvatarManager.GetAvatar("fire"));
            _player.SetAvatar("fire");*/

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

            /*ConversationManager.CreateConversations(_game);

            var teacherOne = Character.FromString(_game, "Lance,teacherone,WalkDown,teacherone,dark");
            var teacherTwo = PCharacter.FromString(_game, "Marissa,teachertwo,WalkDown,teachertwo,light");*/

            /*teacherOne.SetConversation("LanceHello");
            teacherTwo.SetConversation("MarissaHello");

            _game.CharacterManager.AddCharacter("teacherone", teacherOne);
            _game.CharacterManager.AddCharacter("teachertwo", teacherTwo);

            _map.Characters.Add("teacherone", new Point(0,4));
            _map.Characters.Add("teachertwo", new Point(4, 0));*/

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