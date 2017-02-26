using System;
using InsigneVictoriae.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace InsigneVictoriae.State.GameStates
{
    public interface IIntroScreenState : IGameState
    {
        
    }

    public class IntroScreenState : GameStateBase, IIntroScreenState
    {
        private Texture2D _background;
        private Rectangle _backgroundDestination;
        private SpriteFont _font;
        private TimeSpan _elapsedTime;
        private Vector2 _position;
        private string _message;
        public IntroScreenState(Game game) : base(game)
        {
            game.Services.AddService(typeof(IIntroScreenState), this);
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
            PlayerIndex? index = null;
            _elapsedTime += gameTime.ElapsedGameTime;

            if (InputHandler.CheckKeyReleased(Keys.Space) || InputHandler.CheckKeyReleased(Keys.Enter) || InputHandler.CheckMouseReleased(MouseButton.Left))
            {
                _manager.ChangeState((MainMenuState)_game.MainMenuState, index);
            }
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