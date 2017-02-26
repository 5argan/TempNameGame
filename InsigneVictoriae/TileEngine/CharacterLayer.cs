using InsigneVictoriae.Characters;
using Microsoft.Xna.Framework;

namespace InsigneVictoriae.TileEngine
{
    public class CharacterLayer
    {
        private ICharacter[] _tiles;
        public int Width { get; }
        public int Height { get; }

        public ICharacter GetCharacterAt(int x, int y) =>
            x < 0 || y < 0 || x >= Width || y >= Height ? null : _tiles[y*Width + x];
        
        public void SetCharacterAt(int x, int y, int tileIndex)
        {
            throw new System.NotImplementedException();
        }
    }
}