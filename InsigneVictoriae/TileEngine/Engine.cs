using System;
using InsigneVictoriae.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TempNameGame.TileEngine;

namespace InsigneVictoriae.TileEngine
{
    public class Engine
    {
/*
        private static float scrollSpeed = 500f;
*/
        public static Rectangle ViewPortRectangle { get; set; }

        public static int TileWidth { get; set; } = 32;
        public static int TileHeight { get; set; } = 32;
        public TileMap Map { get; private set; }
        public static Camera Camera { get; private set; }

        public Engine(Rectangle viewPort)
        {
            ViewPortRectangle = viewPort;
            Camera = new Camera();

            TileHeight = 64;
            TileWidth = 64;
        }

        public Engine(Rectangle viewPort, int tileWidth, int tileHeight) : this(viewPort)
        {
            TileWidth = tileWidth;
            TileHeight = tileHeight;
        }

        public static Point VectorToCell(Vector2 position)
            => new Point((int) position.X/TileWidth, (int) position.Y/TileHeight);
        
        public void SetMap(TileMap newMap)
        {
            if (newMap == null)
            {
                throw new ArgumentNullException(nameof(newMap));
            }

            Map = newMap;
        }

        public void Update(GameTime gameTime)
        {
            Map.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Map.Draw(gameTime, spriteBatch, Camera);
        }
    }
}