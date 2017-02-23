using Microsoft.Xna.Framework;
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
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public void SetUpNewGame()
        {
            throw new System.NotImplementedException();
        }

        public void LoadExistingGame()
        {
            throw new System.NotImplementedException();
        }

        public void StartGame()
        {
            throw new System.NotImplementedException();
        }
    }
}