using System;
using Microsoft.Xna.Framework;

namespace TempNameGame.TileEngine
{
    public class Camera
    {
        public Vector2 Position { get; set; }
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
            Position.X = MathHelper.Clamp(Position.X, 0, map.WidthInPixels - viewport.Width);
            Position.Y = MathHelper.Clamp(Position.Y, 0, map.HeightInPixels - viewport.Height);
        }
    }
}