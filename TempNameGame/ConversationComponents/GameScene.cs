using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TempNameGame.Components;

namespace TempNameGame.ConversationComponents
{
    public class GameScene
    {
        private Vector2 textPosition;

        protected Game _game;
        protected string _text;
        public int selectedIndex { get; private set; }

        public string Text
        {
            get { return _text;}
            set { _text = value; }
        }

        public static Texture2D Selected { get; private set; }
        public List<SceneOption> Options { get; set; }

        public bool IsMouseOver { get; private set; }

        [ContentSerializerIgnore]
        public Color NormalColor { get; set; }

        [ContentSerializerIgnore]
        public Color HighlightColor { get; set; }

        public Vector2 MenuPosition { get; } = new Vector2(50, 475);

        [ContentSerializerIgnore]
        public SceneAction OptionAction => Options[selectedIndex].OptionAction;

        public string OptionScene => Options[selectedIndex].OptionScene;

        public string OptionText => Options[selectedIndex].OptionText;

        private GameScene()
        {
            NormalColor = Color.Black;
            HighlightColor = Color.Red;
        }

        public GameScene(string text, List<SceneOption> options) : this()
        {
            _text = text;
            Options = options;
            
            textPosition = Vector2.Zero;
        }

        public GameScene(Game game, string text, List<SceneOption> options) : this(text, options)
        {
            _game = game;
        }

        public void SetText(string text, SpriteFont font)
        {
            textPosition = new Vector2(450, 50);

            var sb = new StringBuilder();
            var currentLength = 0f;
            if (font == null)
            {
                Text = text;
                return;
            }

            var parts = text.Split(' ');
            foreach (var part in parts)
            {
                var size = font.MeasureString(part);
                if (currentLength + size.X < 500f)
                {
                    sb.Append(part);
                    sb.Append(" ");
                    currentLength += size.X;
                }
                else
                {
                    sb.Append("\n\r");
                    sb.Append(part);
                    sb.Append(" ");
                    currentLength = 0;
                }
            }

            Text = sb.ToString();
        }

        public void Initialize()
        {
            
        }

        public void Update(GameTime gameTime, PlayerIndex index)
        {
            if (InputHandler.CheckKeyReleased(Keys.Up))
            {
                selectedIndex = selectedIndex < 1 ? Options.Count - 1 : selectedIndex - 1;
            }
            else if (InputHandler.CheckKeyReleased(Keys.Down))
            {
                selectedIndex = selectedIndex > Options.Count - 2 ? 0 : selectedIndex + 1;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Texture2D background, SpriteFont font)
        {
            var selectedPosition = new Vector2();
            if (Selected == null)
                Selected = _game.Content.Load<Texture2D>(@"Misc\selected");

            if (textPosition == Vector2.Zero)
                SetText(Text, font);

            if (background != null)
                spriteBatch.Draw(background, Vector2.Zero, Color.White);

            spriteBatch.DrawString(font, Text, textPosition, Color.White);

            var position = MenuPosition;
            var optionRect = new Rectangle(0, (int) position.Y, 1280, font.LineSpacing);
            IsMouseOver = false;

            for (var i = 0; i < Options.Count; i++)
            {
                if (optionRect.Contains(InputHandler.MouseState.Position))
                {
                    selectedIndex = i;
                    IsMouseOver = true;
                }

                Color myColor;
                if (i == selectedIndex)
                {
                    myColor = HighlightColor;
                    selectedPosition.X = position.X - 35;
                    selectedPosition.Y = position.Y;

                    spriteBatch.Draw(Selected, selectedPosition, Color.White);
                }
                else
                {
                    myColor = NormalColor;
                }

                spriteBatch.DrawString(font, Options[i].OptionText, position, myColor);

                position.Y += font.LineSpacing + 5;
                optionRect.Y += font.LineSpacing + 5;
            }
        }
    }
}