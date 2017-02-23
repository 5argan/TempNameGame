using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TempNameGame.Components
{
    public class MenuComponent
    {
        private SpriteFont _spriteFont;
        private readonly List<string> _menuItems = new List<string>();
        private int _selectedIndex = -1;

        public int Width { get; private set; }
        public int Height { get; private set; }

        public Color NormalColor { get; set; } = Color.White;
        public Color HighlightColor { get; set; } = Color.Red;

        private Texture2D _texture;

        public Vector2 Position { get; set; }

        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set { _selectedIndex = MathHelper.Clamp(value, 0, _menuItems.Count - 1); }
        }

        public bool MouseOver { get; private set; }

        public MenuComponent(SpriteFont spriteFont, Texture2D texture)
        {
            MouseOver = false;
            _spriteFont = spriteFont;
            _texture = texture;
        }

        public MenuComponent(SpriteFont spriteFont, Texture2D texture, string[] menuItems) : this(spriteFont, texture)
        {
            _selectedIndex = 0;

            foreach (var item in menuItems)
            {
                _menuItems.Add(item);
            }

            MeasureMenu();
        }

        public void SetMenuItems(string[] items)
        {
            _menuItems.Clear();
            _menuItems.AddRange(items);
            MeasureMenu();

            SelectedIndex = 0;
        }

        private void MeasureMenu()
        {
            Debug.Assert(_menuItems.Count > 0);

            Width = _texture.Width;
            Height = 0;

            foreach (var item in _menuItems)
            {
                var size = _spriteFont.MeasureString(item);

                if (size.X > Width)
                    Width = (int) size.X;

                Height += _texture.Height + 50;
            }

            Height -= 50;
        }

        public void Update(GameTime gametime, PlayerIndex index)
        {
            var menuPosition = Position;
            var p = InputHandler.MouseState.Position;

            MouseOver = false;

            for (var i = 0; i < _menuItems.Count; i++)
            {
                //This assumes every button has the same height (texture). Could cause issues later.
                var buttonRectangle = new Rectangle((int) menuPosition.X, (int) menuPosition.Y, _texture.Width, _texture.Height);

                if (buttonRectangle.Contains(p))
                {
                    _selectedIndex = i;
                    MouseOver = true;
                }

                menuPosition.Y += _texture.Height + 50;
            }

            if (!MouseOver && InputHandler.CheckKeyReleased(Keys.Up))
            {
                _selectedIndex--;
                if (_selectedIndex < 0)
                    _selectedIndex = _menuItems.Count - 1;
            }
            else if (!MouseOver && InputHandler.CheckKeyReleased(Keys.Down))
            {
                _selectedIndex++;
                if (_selectedIndex > _menuItems.Count - 1)
                    _selectedIndex = 0;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var menuPosition = Position;

            for (var i = 0; i < _menuItems.Count; i++)
            {
                var myColor = i == SelectedIndex ? HighlightColor : NormalColor;
                spriteBatch.Draw(_texture, menuPosition, Color.White);

                var textSize = _spriteFont.MeasureString(_menuItems[i]);
                var textPosition = 
                    menuPosition + new Vector2((int) (_texture.Width - textSize.X)/2, (int) (_texture.Height - textSize.Y)/2);
                spriteBatch.DrawString(_spriteFont, _menuItems[i], textPosition, myColor);

                menuPosition.Y += _texture.Height + 50;
            }
        } 
    }
}