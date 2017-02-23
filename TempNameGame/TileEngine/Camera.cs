using System;
using Microsoft.Xna.Framework;

namespace TempNameGame.TileEngine
{
    public class Camera
    {
        private Vector2 _position;

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }
        private float _speed;

        public float Speed
        {
            get { return _speed; }
            set { _speed = MathHelper.Clamp(value, 1f, 16f); }
        }

        public Matrix Transformation => Matrix.CreateTranslation(new Vector3(-Position, 0f));

        public Camera()
        {
            Speed = 4f;
        }

        public Camera(Vector2 position)
        {
            Speed = 4f;
            Position = position;
        }

        public void LockCamera(TileMap map, Rectangle viewport)
        {
            _position.X = MathHelper.Clamp(Position.X, 0, map.WidthInPixels - viewport.Width);
            _position.Y = MathHelper.Clamp(Position.Y, 0, map.HeightInPixels - viewport.Height);
        }

        public void LockToSprite(TileMap map, AnimatedSprite sprite, Rectangle viewport)
        {
            _position.X = sprite.Position.X + sprite.Width/2 - viewport.Width/2;
            _position.Y = sprite.Position.Y + sprite.Height / 2 - viewport.Height/ 2;
            LockCamera(map, viewport);
        }
    }
}