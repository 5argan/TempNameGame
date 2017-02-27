using System.Collections.Generic;
using System.Linq;
using InsigneVictoriae.Characters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TempNameGame.TileEngine;

namespace InsigneVictoriae.TileEngine
{
    public class TileMap
    {
        //private readonly Engine _engine = new Engine(Camera.vi1);
        [ContentSerializer] private readonly int _mapWidth;
        [ContentSerializer] private readonly int _mapHeight;
        
        [ContentSerializer]
        public string MapName { get; private set; }

        [ContentSerializer]
        public TileSet TileSet { get; set; }

        [ContentSerializer]
        public TileLayer GroundLayer { get; set; }

        [ContentSerializer]
        public TileLayer EdgeLayer { get; set; }

        [ContentSerializer]
        public TileLayer BuildingLayer { get; set; }

        [ContentSerializer]
        public TileLayer DecorationLayer { get; set; }
        
        public int MapWidth => _mapWidth;
        public int MapHeight => _mapHeight;
        
        public List<ICharacter> Characters { get; }
        public int WidthInPixels => MapWidth * Engine.TileWidth;
        public int HeightInPixels => MapHeight * Engine.TileHeight;

        private TileMap()
        {
        }

        private TileMap(TileSet tileSet, string mapName) : this()
        {
            Characters = new List<ICharacter>();
            TileSet = tileSet;
            MapName = mapName;
        }

        public TileMap(TileSet tileSet, TileLayer groundLayer, TileLayer edgeLayer, TileLayer buildingLayer,
            TileLayer decorationLayer, string mapName) : this(tileSet, mapName)
        {
            GroundLayer = groundLayer;
            EdgeLayer = edgeLayer;
            BuildingLayer = buildingLayer;
            DecorationLayer = decorationLayer;

            _mapWidth = groundLayer.Width;
            _mapHeight = groundLayer.Height;
        }

        public void SetGroundTile(int x, int y, int index)
        {
            GroundLayer.SetTile(x, y, index);
        }

        public int GetGroundTile(int x, int y) => GroundLayer.GetTile(x, y);

        public void SetBuildingTile(int x, int y, int index)
        {
            BuildingLayer.SetTile(x, y, index);
        }

        public int GetBuildingTile(int x, int y) => BuildingLayer.GetTile(x, y);

        public void SetEdgeTile(int x, int y, int index)
        {
            EdgeLayer.SetTile(x, y, index);
        }

        public int GetEdgeTile(int x, int y) => EdgeLayer.GetTile(x, y);

        public void SetDecorationTile(int x, int y, int index)
        {
            DecorationLayer.SetTile(x, y, index);
        }

        public int GetDecorationTile(int x, int y) => DecorationLayer.GetTile(x, y);

        public void FillEdges()
        {
            for (var y = 0; y < MapHeight; y++)
            {
                for (var x = 0; x < MapWidth; x++)
                {
                    EdgeLayer.SetTile(x, y, -1);
                }
            }
        }

        public void FillBuilding()
        {
            for (var y = 0; y < MapHeight; y++)
            {
                for (var x = 0; x < MapWidth; x++)
                {
                    BuildingLayer.SetTile(x, y, -1);
                }
            }
        }

        public void FillDecoration()
        {
            for (var y = 0; y < MapHeight; y++)
            {
                for (var x = 0; x < MapWidth; x++)
                {
                    DecorationLayer.SetTile(x, y, -1);
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            GroundLayer?.Update(gameTime);
            EdgeLayer?.Update(gameTime);
            BuildingLayer?.Update(gameTime);
            DecorationLayer?.Update(gameTime);

            Characters.RemoveAll(c => !c.IsAlive());
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Camera camera)
        {
            GroundLayer?.Draw(gameTime, spriteBatch, TileSet, camera);
            EdgeLayer?.Draw(gameTime, spriteBatch, TileSet, camera);
            BuildingLayer?.Draw(gameTime, spriteBatch, TileSet, camera);
            DecorationLayer?.Draw(gameTime, spriteBatch, TileSet, camera);

            DrawCharacters(gameTime, spriteBatch, camera);
        }

        public void DrawCharacters(GameTime gameTime, SpriteBatch spriteBatch, Camera camera)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, camera.Transform);
            
            foreach (var character in Characters)
            {
                character.Sprite.Position.X = character.Position.X*Engine.TileWidth;
                character.Sprite.Position.Y = character.Position.Y * Engine.TileWidth;
                character.Sprite.Draw(gameTime, spriteBatch);
            }

            spriteBatch.End();
        }

        public ICharacter GetCharacterAtCell(int x, int y) =>
            Characters.FirstOrDefault(c => (int)c.Position.X == x && (int)c.Position.Y == y);
    }
}