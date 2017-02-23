using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TempNameGame.TileEngine
{
    public enum AnimationKey
    {
        IdleLeft,
        IdleRight,
        IdleDown,
        IdleUp,
        WalkLeft,
        WalkRight,
        WalkDown,
        WalkUp,
        ThrowLeft,
        ThrowRight,
        DuckLeft,
        DuckRight,
        JumpLeft,
        JumpRight,
        Dieing,
    }

    public class AnimatedSprite
    {
        private readonly Dictionary<AnimationKey, Animation> _animations;
        private float _speed = 200.0f;
        private readonly Texture2D _texture;

        public AnimationKey CurrentAnimation { get; set; }
        public bool IsAnimating { get; set; }
        public int Width => _animations[CurrentAnimation].FrameWidth;
        public int Height => _animations[CurrentAnimation].FrameHeight;
        public Vector2 Position;

        public float Speed
        {
            get { return _speed; }
            set { _speed = MathHelper.Clamp(value, 1.0f, 400.0f); }
        }

        public Vector2 Velocity { get; set; }

        public AnimatedSprite(Texture2D sprite, Dictionary<AnimationKey, Animation> animation)
        {
            _texture = sprite;
            _animations = new Dictionary<AnimationKey, Animation>();
            foreach (var key in animation.Keys)
                _animations.Add(key, (Animation)animation[key].Clone());
        }

        public void ResetAnimation()
        {
            _animations[CurrentAnimation].Reset();
        }

        public virtual void Update(GameTime gameTime)
        {
            if (IsAnimating)
                _animations[CurrentAnimation].Update(gameTime);
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, _animations[CurrentAnimation].CurrentFrameRect, Color.White);
        }

        public void LockToMap(Point mapSize)
        {
            Position.X = MathHelper.Clamp(Position.X, 0, mapSize.X - Width);
            Position.Y = MathHelper.Clamp(Position.Y, 0, mapSize.Y - Height);
        }
    }
}