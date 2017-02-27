using System.Collections.Generic;
using InsigneVictoriae.Characters;
using InsigneVictoriae.TileEngine;

namespace InsigneVictoriae.CombatMap
{
    public class CombatMap
    {
        private TileMap _tileMap;
        private int _width;
        private int _height;
        private List<ICharacter> _playerCharacters;
        private List<ICharacter> _aiCharacters;

        public CombatMap(TileMap tileMap) : this(tileMap, new List<ICharacter>(), new List<ICharacter>())
        {
            
        }

        public CombatMap(TileMap tileMap, List<ICharacter> playerCharacters, List<ICharacter> aiCharacters)
        {
            _tileMap = tileMap;
            _width = _tileMap.MapWidth;
            _height = _tileMap.MapHeight;
            _playerCharacters = playerCharacters;
            _aiCharacters = aiCharacters;
        }
    }
}