using System;
using System.Resources;
using Microsoft.Xna.Framework;

namespace TempNameGame.TileEngine
{
    public class Animation : ICloneable
    {
        private int _framesPerSecond;
        private int _currentFrame;
        private TimeSpan _frameLength;
        private TimeSpan _frameTimer;
        private readonly Rectangle[] _frames;

        public int FramesPerSecond
        {
            get { return _framesPerSecond; }
            set
            {
                if (value < 1)
                    _framesPerSecond = 1;
                else if (value > 60)
                    _framesPerSecond = 60;
                else
                    _framesPerSecond = value;
                _frameLength = TimeSpan.FromSeconds(1/(double) _framesPerSecond);
            }
        }

        public Rectangle CurrentFrameRect => _frames[_currentFrame];

        public int CurrentFrame
        {
            get { return _currentFrame; }
            set { _currentFrame = MathHelper.Clamp(value, 0, _frames.Length - 1); }
        }

        public int FrameWidth { get; private set; }
        public int FrameHeight { get; private set; }

        public Animation(int frameCount, int frameWidth, int frameHeight, int xOffset, int yOffset)
        {
            _frames = new Rectangle[frameCount];
            FrameWidth = frameWidth;
            FrameHeight = frameHeight;

            for (var i = 0; i < frameCount; i++)
            {
                _frames[i] = new Rectangle(xOffset + frameWidth*i, yOffset, frameWidth, frameHeight);
            }
            FramesPerSecond = 5;
            Reset();
        }

        private Animation(Animation animation)
        {
            _frames = animation._frames;
            FramesPerSecond = 5;
        }

        public void Reset()
        {
            CurrentFrame = 0;
            _frameTimer = TimeSpan.Zero;
        }

        public void Update(GameTime gameTime)
        {
            _frameTimer += gameTime.ElapsedGameTime;
            if (_frameTimer >= _frameLength)
            {
                _frameTimer = TimeSpan.Zero;
                CurrentFrame = (CurrentFrame + 1)%_frames.Length;
            }
        }

        public object Clone()
        {
            var animationClone = new Animation(this)
            {
                FrameWidth = this.FrameWidth,
                FrameHeight = this.FrameHeight
            };
            animationClone.Reset();

            return animationClone;
        }
    }
}