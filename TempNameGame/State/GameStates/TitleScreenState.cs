using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TempNameGame.State.GameStates
{
    public interface ITitleScreenState : IGameState
    {
        
    }

    public class TitleScreenState : GameStateBase, ITitleScreenState
    {
        private Texture2D _background;
        private Rectangle _backgroundDestination;
        private SpriteFont _font;
        private TimeSpan _elapsedTime;
        private Vector2 _position;
        private string _message;
        public TitleScreenState(Game game) : base(game)
        {
            game.Services.AddService(typeof(ITitleScreenState), this);
        }

        public override void Initialize()
        {
            _backgroundDestination = Game1.ScreenRectangle;
            _elapsedTime = TimeSpan.Zero;
            _message = "PRESS SPACE TO CONTINUE";

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _background = _content.Load<Texture2D>(@"GameScreens\titlescreen");
            _font = _content.Load<SpriteFont>(@"Fonts\InterfaceFont");

            var size = _font.MeasureString(_message);
            _position = new Vector2((Game1.ScreenRectangle.Width - size.X) / 2, Game1.ScreenRectangle.Bottom - 50 - _font.LineSpacing);

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            _elapsedTime += gameTime.ElapsedGameTime;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _game.SpriteBatch.Begin();
            _game.SpriteBatch.Draw(_background, _backgroundDestination, Color.White);

            var color = new Color(1f, 1f, 1f)*(float) Math.Abs(Math.Sin(_elapsedTime.TotalSeconds*2));

            _game.SpriteBatch.DrawString(_font, _message, _position, color);
            _game.SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}