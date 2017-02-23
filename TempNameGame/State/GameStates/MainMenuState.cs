using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TempNameGame.Components;

namespace TempNameGame.State.GameStates
{
    public interface IMainMenuState : IGameState
    {
    }

    public class MainMenuState : GameStateBase, IMainMenuState
    {
        private Texture2D _background;
        private SpriteFont _spriteFont;
        private MenuComponent _menuComponent;

        public MainMenuState(Game game) : base(game)
        {
            game.Services.AddService(typeof(IMainMenuState), this);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteFont = Game.Content.Load<SpriteFont>(@"Fonts\InterfaceFont");
            _background = Game.Content.Load<Texture2D>(@"GameScreens\menuscreen");

            var texture = Game.Content.Load<Texture2D>(@"Misc\wooden-button");

            string[] menuItems = {"NEW GAME", "CONTINUE", "OPTIONS", "EXIT"};

            _menuComponent = new MenuComponent(_spriteFont, texture, menuItems);

            var position = new Vector2
            {
                Y = 90,
                X = 1200 - _menuComponent.Width
            };

            _menuComponent.Position = position;

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            _menuComponent.Update(gameTime, null);

            if (InputHandler.CheckKeyReleased(Keys.Space) || InputHandler.CheckKeyReleased(Keys.Enter) ||
                (_menuComponent.MouseOver && InputHandler.CheckMouseReleased(MouseButton.Left)))
            {
                if (_menuComponent.SelectedIndex == 0)
                {
                    InputHandler.FlushInput();
                }
                else if (_menuComponent.SelectedIndex == 1)
                {
                    InputHandler.FlushInput();
                }
                else if (_menuComponent.SelectedIndex == 2)
                {
                    InputHandler.FlushInput();
                }
                else if (_menuComponent.SelectedIndex == 3)
                {
                    Game.Exit();
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _game.SpriteBatch.Begin();
            _game.SpriteBatch.Draw(_background, Vector2.Zero, Color.White);
            _game.SpriteBatch.End();

            base.Draw(gameTime);

            _game.SpriteBatch.Begin();
            _menuComponent.Draw(gameTime, _game.SpriteBatch);
            _game.SpriteBatch.End();
        }
    }
}