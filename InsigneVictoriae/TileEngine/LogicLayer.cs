using Microsoft.Xna.Framework;

namespace TempNameGame.TileEngine
{
    public class LogicLayer
    {
        private string[] _tiles;
        public int Width { get; }
        public int Height { get; }

        public string GetTile(int x, int y) =>
            x < 0 || y < 0 || x >= Width || y >= Height ? "" : _tiles[y*Width + x];
        
        public void SetTile(int x, int y, int tileIndex)
        {
            throw new System.NotImplementedException();
        }
    }
}