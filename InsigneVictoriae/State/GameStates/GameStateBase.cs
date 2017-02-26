using System;
using InsigneVictoriae;
using Microsoft.Xna.Framework;

namespace TempNameGame.State.GameStates
{
    public class GameStateBase : GameState
    {
        protected static Random random = new Random();
        protected Game1 _game;

        public GameStateBase(Game game) : base(game)
        {
            _game = (Game1)game;
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
        }
    }
}