using System;
using System.Collections.Generic;
using System.Linq;
using InsigneVictoriae.Characters;
using InsigneVictoriae.TileEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InsigneVictoriae.CombatMap
{
    public class CombatMap
    {
        private readonly TileMap _tileMap;
        private int _width;
        private int _height;
        private CharacterManager _characterManager;
        private readonly List<ICharacter> _playerCharacters;
        private readonly List<ICharacter> _aiCharacters;


        public CombatMap(TileMap tileMap, CharacterManager characterManager) : this(tileMap, new List<ICharacter>(), new List<ICharacter>(), characterManager)
        {
            
        }

        public CombatMap(TileMap tileMap, List<ICharacter> playerCharacters, List<ICharacter> aiCharacters, CharacterManager characterManager)
        {
            _tileMap = tileMap;
            _width = _tileMap.MapWidth;
            _height = _tileMap.MapHeight;
            _playerCharacters = playerCharacters;
            _aiCharacters = aiCharacters;
            _characterManager = characterManager;
        }

        public void Update(GameTime gameTime)
        {
            _tileMap.Update(gameTime);
            _playerCharacters.RemoveAll(c => !c.IsAlive());
            _aiCharacters.RemoveAll(c => !c.IsAlive());
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Camera camera)
        {
            _tileMap.Draw(gameTime, spriteBatch, camera);
            DrawCharacters(gameTime, spriteBatch, camera);
        }

        private void DrawCharacters(GameTime gameTime, SpriteBatch spriteBatch, Camera camera)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, camera.Transform);

            foreach (var character in _playerCharacters.Union(_aiCharacters))
            {
                character.Sprite.Position.X = character.Position.X * Engine.TileWidth;
                character.Sprite.Position.Y = character.Position.Y * Engine.TileWidth;
                character.Sprite.Draw(gameTime, spriteBatch);
            }

            spriteBatch.End();
        }
    }
}