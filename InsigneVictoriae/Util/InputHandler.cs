using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace InsigneVictoriae.Util
{
    public enum MouseButton
    {
        Left,
        Right,
        Center
    }
    public class InputHandler : GameComponent
    {
        public static MouseState MouseState { get; private set; } = Mouse.GetState();

        public static MouseState PreviousMouseState { get; private set; } = Mouse.GetState();

        public static KeyboardState KeyboardState { get; private set; } = Keyboard.GetState();

        public static KeyboardState PreviousKeyboardState { get; private set; } = Keyboard.GetState();

        public InputHandler(Game game) : base(game)
        {
        }

        public override void Update(GameTime gameTime)
        {
            PreviousKeyboardState = KeyboardState;
            KeyboardState = Keyboard.GetState();

            PreviousMouseState = MouseState;
            MouseState = Mouse.GetState();

            base.Update(gameTime);
        }

        public static void FlushInput()
        {
            MouseState = PreviousMouseState;
            KeyboardState = PreviousKeyboardState;
        }

        public static bool CheckKeyReleased(Keys key)
        {
            return KeyboardState.IsKeyUp(key) && PreviousKeyboardState.IsKeyDown(key);
        }

        public static bool CheckMouseReleased(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.Left:
                    return (MouseState.LeftButton == ButtonState.Released) &&
                           (PreviousMouseState.LeftButton == ButtonState.Pressed);
                case MouseButton.Right:
                    return (MouseState.RightButton == ButtonState.Released) &&
                           (PreviousMouseState.RightButton == ButtonState.Pressed);
                case MouseButton.Center:
                    return (MouseState.MiddleButton == ButtonState.Released) &&
                           (PreviousMouseState.MiddleButton == ButtonState.Pressed);
                default:
                    throw new ArgumentOutOfRangeException(nameof(button), button, null);
            }
        }
    }
}